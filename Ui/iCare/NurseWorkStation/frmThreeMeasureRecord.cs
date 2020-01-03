using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using weCare.Core.Entity;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using HRP;
using System.Xml; 
//using iCare.ICU.Espial;

namespace iCare
{
	public class frmThreeMeasureRecord : frmHRPBaseForm,PublicFunction
    {
        #region sysdefine

        private System.Windows.Forms.Label lblWeightUnit;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtInputValue;
		private System.Windows.Forms.GroupBox m_grbOther;
        private System.Windows.Forms.Label lblRecordTime;
		private System.Windows.Forms.Label lblOutStreamUnit;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtOutStreamValue;
        private System.Windows.Forms.Label lblPeeUnit;
		private PinkieControls.ButtonXP m_cmdSave;
		private com.digitalwave.Utility.Controls.ctlTimePicker m_dtpRecordDateTime;
		private System.Windows.Forms.GroupBox m_grbSkinTest;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboSkinTestMedicine;
		private System.Windows.Forms.RadioButton m_rdbSkinTestGood;
		private System.Windows.Forms.RadioButton m_rdbSkinTestBad;
        private System.Windows.Forms.Label lblSkinTestMedicine;
        private com.digitalwave.Utility.Controls.ctlComboBox m_cboEventType;
		private System.Windows.Forms.Label lblDejectaWeightUnit;
        private System.Windows.Forms.CheckBox m_chkNeedWeight;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtDejectaWeightValue;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtPressureDiastolicValue;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtPressureSystolicValue;
		private System.Windows.Forms.CheckBox m_chkPulseIsHalf;
        private PinkieControls.ButtonXP m_cmdDelete;
        private System.Windows.Forms.CheckBox m_chkTemperatureIsHalf;
		private System.Windows.Forms.Label lblOtherItem;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboOtherItem;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboOtherUnit;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtOtherValue;
		private System.Windows.Forms.Label label1;
		private System.Drawing.Printing.PrintDocument m_pdcRecord;
        private System.Windows.Forms.PrintPreviewDialog m_ppdRecord;
		private System.Windows.Forms.Label lblEventHour;
		private System.Windows.Forms.NumericUpDown m_nmuEventHour;
		private System.Windows.Forms.NumericUpDown m_nmuEventMinute;
        private System.Windows.Forms.Label lblEventMinute;
		private System.Windows.Forms.NumericUpDown m_nmuSkinBadCount;
        private PinkieControls.ButtonXP m_cmdGetDovueData;
		private System.Windows.Forms.ToolTip m_tipPrompt;
        private System.Windows.Forms.ImageList m_imgZoom;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboTimeFlag;
		private System.Windows.Forms.Label label3;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboTemperature;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboPulse;
		private System.Windows.Forms.Label label9;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboTemperatureType;
		private System.Windows.Forms.Label label4;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboPulseType;
		private System.Windows.Forms.Label label2;
        private com.digitalwave.Utility.Controls.ctlComboBox m_cboBreathValue;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboDejectaBeforeTimes;
		private System.Windows.Forms.Label label12;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboPeeValue;
		private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboWeightValue;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label11;
		private com.digitalwave.Utility.Controls.ctlComboBox m_cboSkinBadCount;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lblDejecta;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox m_grpOperation;
		private PinkieControls.ButtonXP m_cmdGeneralNurse;
		private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private com.digitalwave.Utility.Controls.ctlTimePicker m_dtpGetDataTime;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox cboCreateDate;

		private System.ComponentModel.IContainer components = null;

        #endregion sysdefine

        #region user define
        private bool blnComboxEvent=false;
        private clsPatient objTempPatient;
        private Panel pnlRecordContain;
        private ctlThreeMeasureRecord m_ctlRecord;
        private Panel pnlFocus;
        protected ctlBorderTextBox m_txtWhichWeek;
        private Button m_cmdNextWeek;
        private Button m_cmdFinalWeek;
        private Button m_cmdPreWeek;
        private Button m_cmdFirstWeek;
        private PictureBox m_picZoom;
        private GroupBox groupBox1;
        private Button m_cmdClear;
        private GroupBox grbRecordType;
        private RadioButton m_rdbSpecialDateType;
        private RadioButton m_rdbPeeType;
        private RadioButton m_rdbDejectaType;
        private RadioButton m_rdbEventType;
        private RadioButton m_rdbPulseType;
        private RadioButton m_rdbBreathType;
        private RadioButton m_rdbTemperatureType;
        private RadioButton m_rdbInputType;
        private RadioButton m_rdbOtherType;
        private RadioButton m_rdbWeightType;
        private RadioButton m_rdbSkinTestType;
        private RadioButton m_rdbOutStreamType;
        private RadioButton m_rdbDownTemperatureType;
        private RadioButton m_rdbPressureType;
        private GroupBox m_grbSpecialDate;
        private CheckBox m_chkNewSpecialDate;
        private GroupBox m_grbTemperature;
        private Panel panel4;
        private RadioButton m_rdbAnusTemperature;
        private RadioButton m_rdbMouthTemperature;
        private RadioButton m_rdbArmpitTemperature;
        private ctlComboBox m_cboTemperatureTimeFlag;
        private Label lblTemperatureUnit;
        private CheckBox m_chkTemperatureNotLineToPre;
        private ctlBorderTextBox m_txtTemperatureValue;
        private GroupBox m_grbOutStream;
        private GroupBox m_grbInput;
        private GroupBox m_grbDejecta;
        private NumericUpDown m_nmuDejectaBeforeTimes;
        private CheckBox m_chkCannotDejecta;
        private Label lblDejectaBeforeTimes;
        private Label lblDejectaClysisTimes;
        private NumericUpDown m_nmuDejectaClysisTimes;
        private Label lblDejectaAfterTimes;
        private NumericUpDown m_nmuDejectaAfterTimes;
        private CheckBox m_chkDejectaAfterMoreTimes;
        private GroupBox m_grbEvent;
        private Label lblEventType;
        private ctlTimePicker m_dtpEventTime_;
        private Label lblEventTime;
        private GroupBox m_grbDownTemperature;
        private ctlComboBox m_cboDownBaseTime;
        private CheckBox m_chkDownBaseTimeIsHalf;
        private Label m_lblDownTemperatureUnit;
        private Label lblBaseTime;
        private ctlBorderTextBox m_txtDownTemperatureValue;
        private GroupBox m_grbPulse;
        private Panel panel2;
        private RadioButton m_rdbPulse;
        private RadioButton m_rdbHeartRate;
        private ctlComboBox m_cboPulseTimeFlag;
        private Label lblPulseUnit;
        private CheckBox m_chkPulseNotLineToPre;
        private ctlBorderTextBox m_txtPulseValue;
        private GroupBox m_grbPressure;
        private Label lblPressureDiastolicUnit;
        private Label lblPressureDiastolic;
        private Label lblPressureSystolic;
        private GroupBox m_grbPee;
        private CheckBox m_chkIsIrretention;
        private ctlBorderTextBox m_txtPeeValue;
        private GroupBox m_grbBreath;
        private Panel panel1;
        private RadioButton m_rdbBreathStopAssistant;
        private RadioButton m_rdbBreathStartAssistant;
        private RadioButton m_rdbBreathNormal;
        private ctlComboBox m_cboBreathTime;
        private Label lblBreathUnit;
        private ctlBorderTextBox m_txtBreathValue;
        private Label lblBreathTime;
        private GroupBox m_grbWeight;
        private Panel panel3;
        private RadioButton m_rdbWeightNormal;
        private RadioButton m_rdbWeightCar;
        private RadioButton m_rdbWeightBed;
        private ctlBorderTextBox m_txtWeightValue;
        private GroupBox groupBox3;

		/// <summary>
		/// ����������
		/// </summary>
		//private clsICUGESimulateGetData m_objICUGESimulateGetData;
        #endregion 

        public frmThreeMeasureRecord()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_rdbSpecialDateType.Tag = m_grbSpecialDate;
			m_rdbEventType.Tag = m_grbEvent;
			m_rdbPulseType.Tag = m_grbPulse;
			m_rdbBreathType.Tag = m_grbBreath;
			m_rdbTemperatureType.Tag = m_grbTemperature;
			m_rdbDownTemperatureType.Tag = m_grbDownTemperature;
			m_rdbInputType.Tag = m_grbInput;
			m_rdbDejectaType.Tag = m_grbDejecta;
			m_rdbPeeType.Tag = m_grbPee;
			m_rdbOutStreamType.Tag = m_grbOutStream;
			m_rdbPressureType.Tag = m_grbPressure;
			m_rdbWeightType.Tag = m_grbWeight;
			m_rdbSkinTestType.Tag = m_grbSkinTest;
			m_rdbOtherType.Tag = m_grbOther;

			m_objRecordDomain = new clsThreeMeasureRecordDomain();
			m_objEventManagerDomain = new clsThreeMeasureEventManagerDomain();

			//			m_cboEventType.AddItem("");//ѡ����൱��ɾ�����¼�
			m_cboEventType.AddItem(enmThreeMeasureEventType.��Ժ);
			m_cboEventType.AddItem(enmThreeMeasureEventType.��Ժ);
			m_cboEventType.AddItem(enmThreeMeasureEventType.����);
            m_cboEventType.AddItem(enmThreeMeasureEventType.����);
			m_cboEventType.AddItem(enmThreeMeasureEventType.����);
			m_cboEventType.AddItem(enmThreeMeasureEventType.����);
			m_cboEventType.AddItem(enmThreeMeasureEventType.���);
            m_cboEventType.AddItem(enmThreeMeasureEventType.���);
            m_cboEventType.AddItem(enmThreeMeasureEventType.ת��);
            m_cboEventType.AddItem(enmThreeMeasureEventType.ת��);
            m_cboEventType.AddItem(enmThreeMeasureEventType.����);
            m_cboEventType.AddItem(enmThreeMeasureEventType.ͣ����);
            m_cboEventType.AddItem(enmThreeMeasureEventType.����ձ);
            m_cboEventType.AddItem(enmThreeMeasureEventType.ͣ����ձ);
            m_cboEventType.AddItem(enmThreeMeasureEventType.�ƾ���ԡ);
            m_cboEventType.AddItem(enmThreeMeasureEventType.��ˮ��ԡ);
            m_cboEventType.AddItem(enmThreeMeasureEventType.�Ϻ�����);
            m_cboEventType.AddItem(enmThreeMeasureEventType.ͣ������);
            m_cboEventType.AddItem(enmThreeMeasureEventType.�ܲ�);
			//			m_cboEventType.SelectedIndex = 0;

			#region old
			clsThreeMeasureEventInfo [] objEvents = m_objEventManagerDomain.m_objGetThreeMeasureEventInfoArr();

			for(int i=0;i<objEvents.Length;i++)
			{
				switch(objEvents[i].m_strThreeMeasureEventFlag)
				{
					case "0":
						m_cboSkinTestMedicine.AddItem(objEvents[i]);
						break;
					case "1":
						m_cboOtherItem.AddItem(objEvents[i]);
						break;
				}				
			}
			m_cboOtherUnit.AddItem("cm");
			#endregion		

			m_cboPulseTimeFlag.AddItem("4 am");
			m_cboPulseTimeFlag.AddItem("8 am");
			m_cboPulseTimeFlag.AddItem("12 am");
			m_cboPulseTimeFlag.AddItem("4 pm");
			m_cboPulseTimeFlag.AddItem("8 pm");
			m_cboPulseTimeFlag.AddItem("12 pm");
			m_cboPulseTimeFlag.SelectedIndex = 0;

			m_cboTemperatureTimeFlag.AddItem("4 am");
			m_cboTemperatureTimeFlag.AddItem("8 am");
			m_cboTemperatureTimeFlag.AddItem("12 am");
			m_cboTemperatureTimeFlag.AddItem("4 pm");
			m_cboTemperatureTimeFlag.AddItem("8 pm");
			m_cboTemperatureTimeFlag.AddItem("12 pm");
			m_cboTemperatureTimeFlag.SelectedIndex = 0;

			m_cboDownBaseTime.AddItem("4 am");
			m_cboDownBaseTime.AddItem("8 am");
			m_cboDownBaseTime.AddItem("12 am");
			m_cboDownBaseTime.AddItem("4 pm");
			m_cboDownBaseTime.AddItem("8 pm");
			m_cboDownBaseTime.AddItem("12 pm");
			m_cboDownBaseTime.SelectedIndex = 0;

			m_cboBreathTime.AddItem("4 am");
			m_cboBreathTime.AddItem("8 am");
			m_cboBreathTime.AddItem("12 am");
			m_cboBreathTime.AddItem("4 pm");
			m_cboBreathTime.AddItem("8 pm");
			m_cboBreathTime.AddItem("12 pm");
			m_cboBreathTime.SelectedIndex = 0;

            //m_objBorderTool = new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlsArrayBorder(
            //    new Control[]{
            //                     m_nmuDejectaAfterTimes,
            //                     m_nmuDejectaBeforeTimes,
            //                     m_nmuDejectaClysisTimes,
            //                     m_nmuEventHour,
            //                     m_nmuEventMinute,
            //                     m_nmuSkinBadCount
            //                 });

			m_objBaseTemperature = null;

			m_lblForTitle.Text = "��  ��  ��";

			m_arlSaveTemp = new ArrayList();

			m_hasLastModifyDateInRecord = new Hashtable();
			m_hasOpenDateInRecord = new Hashtable();

			m_strUserID = m_objCurrentContext.m_ObjEmployee.m_StrEmployeeID;

			m_ctlRecord.m_StrUserID = m_strUserID;
			m_ctlRecord.m_StrUserName = m_objCurrentContext.m_ObjEmployee.m_StrLastName;

			m_objPublic = new clsPublicDomain();			

			

			//			m_arlSetFirstInPatientID = new ArrayList();
			//			m_arlSetFirstInPatientDate = new ArrayList();
			//			m_arlSetFirstOpenDate = new ArrayList();

			m_objTrendDomain=new clsTrendDomain();

			m_ctlRecord.m_BlnIsShort = true;

			m_mthInit();

			//m_objICUGESimulateGetData=new clsICUGESimulateGetData(this);
		}

		private void m_mthInit()
		{
			m_cboTimeFlag.AddItem("4 am");
			m_cboTimeFlag.AddItem("8 am");
			m_cboTimeFlag.AddItem("12 am");
			m_cboTimeFlag.AddItem("4 pm");
			m_cboTimeFlag.AddItem("8 pm");
			m_cboTimeFlag.AddItem("12 pm");
			m_cboTimeFlag.SelectedIndex = m_intGetLastestTime(DateTime.Now);

			m_cboTemperatureType.AddRangeItems(new string[]{"�ڱ�:","�ر�:","Ҹ��:","����:"});
			m_cboTemperatureType.SelectedIndex = 2;

//			m_cboTemperature.AddRangeItems(new string[]{"���²���","�ܲ�","���","���"});
//			m_cboTemperature.AddRangeItems(new string[]{"������"});

			m_cboPulseType.AddRangeItems(new string[]{"����:","����:"});
			m_cboPulseType.SelectedIndex = 0;
//			m_cboPulse.AddRangeItems(new string[]{"�ܲ�","���","���"});
//			m_cboPulse.AddRangeItems(new string[]{"������"});

			m_cboBreathValue.AddRangeItems(new string[]{"��������","ͣ��������"});

			m_cboDejectaBeforeTimes.AddRangeItems(new string[]{"*","0/E","1/E","2/E","*/E","1 1/E","1 2/E","2 1/E","2 2/E"});

			m_cboPeeValue.AddItem("*");

			m_cboWeightValue.AddRangeItems(new string[]{"����","�Դ�"});

			m_cboSkinBadCount.AddRangeItems(new string[]{"-","+","++","+++"});
			m_cboSkinBadCount.SelectedIndex = 0;
		}

		private clsTrendDomain m_objTrendDomain;
		private string m_strUserID;
		private string m_strInPatientDate;
		private clsPublicDomain m_objPublic;

		/// <summary>
		/// ��������޸�����
		/// </summary>
		private Hashtable m_hasLastModifyDateInRecord;
		/// <summary>
		/// �����¼��ʹ������
		/// </summary>
		private Hashtable m_hasOpenDateInRecord;

        //private clsBorderTool m_objBorderTool;

		private clsThreeMeasureTemperatureValue m_objBaseTemperature;

		private clsThreeMeasureRecordDomain m_objRecordDomain;
		private clsThreeMeasureEventManagerDomain m_objEventManagerDomain;

		/// <summary>
		/// �Ƿ�������
		/// </summary>
		private bool m_blnIsControl = false;

		private object m_objSaveValue;

		/// <summary>
		/// ��������޸�ǰ����Ϣ���Ա��ڱ������ݿ�ʧ��ʱ�Ľ���ָ�
		/// </summary>
		private clsThreeMeasureXmlValue m_objOleXml;

		private ArrayList m_arlSaveTemp;

		//		private bool m_blnCanTextChanged = true;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThreeMeasureRecord));
            this.lblWeightUnit = new System.Windows.Forms.Label();
            this.m_txtInputValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_grbOther = new System.Windows.Forms.GroupBox();
            this.lblOtherItem = new System.Windows.Forms.Label();
            this.m_cboOtherUnit = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cboOtherItem = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_txtOtherValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblRecordTime = new System.Windows.Forms.Label();
            this.lblOutStreamUnit = new System.Windows.Forms.Label();
            this.m_txtOutStreamValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblPeeUnit = new System.Windows.Forms.Label();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_dtpRecordDateTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.m_grbSkinTest = new System.Windows.Forms.GroupBox();
            this.m_nmuSkinBadCount = new System.Windows.Forms.NumericUpDown();
            this.m_rdbSkinTestGood = new System.Windows.Forms.RadioButton();
            this.m_rdbSkinTestBad = new System.Windows.Forms.RadioButton();
            this.lblSkinTestMedicine = new System.Windows.Forms.Label();
            this.m_cboSkinTestMedicine = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblEventHour = new System.Windows.Forms.Label();
            this.m_nmuEventHour = new System.Windows.Forms.NumericUpDown();
            this.m_nmuEventMinute = new System.Windows.Forms.NumericUpDown();
            this.lblEventMinute = new System.Windows.Forms.Label();
            this.m_cboEventType = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_chkNeedWeight = new System.Windows.Forms.CheckBox();
            this.lblDejectaWeightUnit = new System.Windows.Forms.Label();
            this.m_txtDejectaWeightValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtPressureDiastolicValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtPressureSystolicValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdGetDovueData = new PinkieControls.ButtonXP();
            this.m_chkPulseIsHalf = new System.Windows.Forms.CheckBox();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_chkTemperatureIsHalf = new System.Windows.Forms.CheckBox();
            this.m_pdcRecord = new System.Drawing.Printing.PrintDocument();
            this.m_ppdRecord = new System.Windows.Forms.PrintPreviewDialog();
            this.m_tipPrompt = new System.Windows.Forms.ToolTip(this.components);
            this.m_cboTemperature = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboPulse = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_txtWhichWeek = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cmdNextWeek = new System.Windows.Forms.Button();
            this.m_cmdFinalWeek = new System.Windows.Forms.Button();
            this.m_cmdPreWeek = new System.Windows.Forms.Button();
            this.m_cmdFirstWeek = new System.Windows.Forms.Button();
            this.m_picZoom = new System.Windows.Forms.PictureBox();
            this.m_imgZoom = new System.Windows.Forms.ImageList(this.components);
            this.m_grpOperation = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.m_cboDejectaBeforeTimes = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblDejecta = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_cboPeeValue = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.m_dtpGetDataTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.m_cboSkinBadCount = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.m_cboWeightValue = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_cboBreathValue = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cboPulseType = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cboTemperatureType = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cboTimeFlag = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.m_cmdGeneralNurse = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cboCreateDate = new System.Windows.Forms.ComboBox();
            this.pnlRecordContain = new System.Windows.Forms.Panel();
            this.m_ctlRecord = new com.digitalwave.Utility.Controls.ctlThreeMeasureRecord();
            this.pnlFocus = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cmdClear = new System.Windows.Forms.Button();
            this.grbRecordType = new System.Windows.Forms.GroupBox();
            this.m_rdbSpecialDateType = new System.Windows.Forms.RadioButton();
            this.m_rdbPeeType = new System.Windows.Forms.RadioButton();
            this.m_rdbDejectaType = new System.Windows.Forms.RadioButton();
            this.m_rdbEventType = new System.Windows.Forms.RadioButton();
            this.m_rdbPulseType = new System.Windows.Forms.RadioButton();
            this.m_rdbBreathType = new System.Windows.Forms.RadioButton();
            this.m_rdbTemperatureType = new System.Windows.Forms.RadioButton();
            this.m_rdbInputType = new System.Windows.Forms.RadioButton();
            this.m_rdbOtherType = new System.Windows.Forms.RadioButton();
            this.m_rdbWeightType = new System.Windows.Forms.RadioButton();
            this.m_rdbSkinTestType = new System.Windows.Forms.RadioButton();
            this.m_rdbOutStreamType = new System.Windows.Forms.RadioButton();
            this.m_rdbDownTemperatureType = new System.Windows.Forms.RadioButton();
            this.m_rdbPressureType = new System.Windows.Forms.RadioButton();
            this.m_grbSpecialDate = new System.Windows.Forms.GroupBox();
            this.m_chkNewSpecialDate = new System.Windows.Forms.CheckBox();
            this.m_grbTemperature = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_rdbAnusTemperature = new System.Windows.Forms.RadioButton();
            this.m_rdbMouthTemperature = new System.Windows.Forms.RadioButton();
            this.m_rdbArmpitTemperature = new System.Windows.Forms.RadioButton();
            this.m_cboTemperatureTimeFlag = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblTemperatureUnit = new System.Windows.Forms.Label();
            this.m_chkTemperatureNotLineToPre = new System.Windows.Forms.CheckBox();
            this.m_txtTemperatureValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_grbOutStream = new System.Windows.Forms.GroupBox();
            this.m_grbInput = new System.Windows.Forms.GroupBox();
            this.m_grbDejecta = new System.Windows.Forms.GroupBox();
            this.m_nmuDejectaBeforeTimes = new System.Windows.Forms.NumericUpDown();
            this.m_chkCannotDejecta = new System.Windows.Forms.CheckBox();
            this.lblDejectaBeforeTimes = new System.Windows.Forms.Label();
            this.lblDejectaClysisTimes = new System.Windows.Forms.Label();
            this.m_nmuDejectaClysisTimes = new System.Windows.Forms.NumericUpDown();
            this.lblDejectaAfterTimes = new System.Windows.Forms.Label();
            this.m_nmuDejectaAfterTimes = new System.Windows.Forms.NumericUpDown();
            this.m_chkDejectaAfterMoreTimes = new System.Windows.Forms.CheckBox();
            this.m_grbEvent = new System.Windows.Forms.GroupBox();
            this.lblEventType = new System.Windows.Forms.Label();
            this.m_dtpEventTime_ = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblEventTime = new System.Windows.Forms.Label();
            this.m_grbDownTemperature = new System.Windows.Forms.GroupBox();
            this.m_cboDownBaseTime = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_chkDownBaseTimeIsHalf = new System.Windows.Forms.CheckBox();
            this.m_lblDownTemperatureUnit = new System.Windows.Forms.Label();
            this.lblBaseTime = new System.Windows.Forms.Label();
            this.m_txtDownTemperatureValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_grbPulse = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_rdbPulse = new System.Windows.Forms.RadioButton();
            this.m_rdbHeartRate = new System.Windows.Forms.RadioButton();
            this.m_cboPulseTimeFlag = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblPulseUnit = new System.Windows.Forms.Label();
            this.m_chkPulseNotLineToPre = new System.Windows.Forms.CheckBox();
            this.m_txtPulseValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_grbPressure = new System.Windows.Forms.GroupBox();
            this.lblPressureDiastolicUnit = new System.Windows.Forms.Label();
            this.lblPressureDiastolic = new System.Windows.Forms.Label();
            this.lblPressureSystolic = new System.Windows.Forms.Label();
            this.m_grbPee = new System.Windows.Forms.GroupBox();
            this.m_chkIsIrretention = new System.Windows.Forms.CheckBox();
            this.m_txtPeeValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_grbBreath = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rdbBreathStopAssistant = new System.Windows.Forms.RadioButton();
            this.m_rdbBreathStartAssistant = new System.Windows.Forms.RadioButton();
            this.m_rdbBreathNormal = new System.Windows.Forms.RadioButton();
            this.m_cboBreathTime = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblBreathUnit = new System.Windows.Forms.Label();
            this.m_txtBreathValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.lblBreathTime = new System.Windows.Forms.Label();
            this.m_grbWeight = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_rdbWeightNormal = new System.Windows.Forms.RadioButton();
            this.m_rdbWeightCar = new System.Windows.Forms.RadioButton();
            this.m_rdbWeightBed = new System.Windows.Forms.RadioButton();
            this.m_txtWeightValue = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_pnlNewBase.SuspendLayout();
            this.m_grbOther.SuspendLayout();
            this.m_grbSkinTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuSkinBadCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuEventHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuEventMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picZoom)).BeginInit();
            this.m_grpOperation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlRecordContain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctlRecord)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grbRecordType.SuspendLayout();
            this.m_grbSpecialDate.SuspendLayout();
            this.m_grbTemperature.SuspendLayout();
            this.panel4.SuspendLayout();
            this.m_grbDejecta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaBeforeTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaClysisTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaAfterTimes)).BeginInit();
            this.m_grbEvent.SuspendLayout();
            this.m_grbDownTemperature.SuspendLayout();
            this.m_grbPulse.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_grbPressure.SuspendLayout();
            this.m_grbPee.SuspendLayout();
            this.m_grbBreath.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_grbWeight.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(575, 74);
            this.lblSex.Size = new System.Drawing.Size(32, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(649, 74);
            this.lblAge.Size = new System.Drawing.Size(28, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(535, 12);
            this.lblBedNoTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(519, 42);
            this.lblInHospitalNoTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(353, 74);
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(519, 74);
            this.lblSexTitle.Size = new System.Drawing.Size(56, 14);
            this.lblSexTitle.Text = "��  ��:";
            this.lblSexTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(607, 74);
            this.lblAgeTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(353, 42);
            this.lblAreaTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(542, 64);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(92, 104);
            this.m_lsvInPatientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(581, 40);
            this.txtInPatientID.Size = new System.Drawing.Size(92, 23);
            this.txtInPatientID.TabStop = false;
            this.txtInPatientID.Visible = false;
            this.txtInPatientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(401, 70);
            this.m_txtPatientName.Size = new System.Drawing.Size(112, 23);
            this.m_txtPatientName.TabStop = false;
            this.m_txtPatientName.Visible = false;
            this.m_txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(581, 10);
            this.m_txtBedNO.Size = new System.Drawing.Size(78, 23);
            this.m_txtBedNO.Visible = false;
            this.m_txtBedNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_cboArea
            // 
            this.m_cboArea.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboArea.ListForeColor = System.Drawing.Color.Black;
            this.m_cboArea.Location = new System.Drawing.Point(401, 38);
            this.m_cboArea.Size = new System.Drawing.Size(114, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(362, 98);
            this.m_lsvPatientName.Size = new System.Drawing.Size(112, 104);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(542, 30);
            this.m_lsvBedNO.Size = new System.Drawing.Size(86, 104);
            // 
            // m_cboDept
            // 
            this.m_cboDept.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboDept.ListForeColor = System.Drawing.Color.Black;
            this.m_cboDept.Location = new System.Drawing.Point(401, 8);
            this.m_cboDept.Size = new System.Drawing.Size(114, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(353, 12);
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(580, -32);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNext.Location = new System.Drawing.Point(655, 10);
            this.m_cmdNext.Size = new System.Drawing.Size(18, 21);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdPre.Location = new System.Drawing.Point(298, 40);
            this.m_cmdPre.Size = new System.Drawing.Size(3, 21);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(330, 42);
            this.m_lblForTitle.Size = new System.Drawing.Size(20, 14);
            this.m_lblForTitle.Text = "��  ��  ��";
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(900, 1);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 27);
            this.m_tipMain.SetToolTip(this.m_cmdModifyPatientInfo, "����鿴���޸Ļ�����ϸ��Ϣ(��ݼ�Alt+P)");
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(2, 0);
            this.m_pnlNewBase.Size = new System.Drawing.Size(969, 58);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(967, 27);
            // 
            // lblWeightUnit
            // 
            this.lblWeightUnit.AutoSize = true;
            this.lblWeightUnit.BackColor = System.Drawing.SystemColors.Control;
            this.lblWeightUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWeightUnit.ForeColor = System.Drawing.Color.Black;
            this.lblWeightUnit.Location = new System.Drawing.Point(243, 329);
            this.lblWeightUnit.Name = "lblWeightUnit";
            this.lblWeightUnit.Size = new System.Drawing.Size(21, 14);
            this.lblWeightUnit.TabIndex = 465;
            this.lblWeightUnit.Text = "Kg";
            this.lblWeightUnit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_txtInputValue
            // 
            this.m_txtInputValue.AccessibleName = "";
            this.m_txtInputValue.BackColor = System.Drawing.Color.White;
            this.m_txtInputValue.BorderColor = System.Drawing.Color.White;
            this.m_txtInputValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtInputValue.Location = new System.Drawing.Point(82, 194);
            this.m_txtInputValue.Name = "m_txtInputValue";
            this.m_txtInputValue.Size = new System.Drawing.Size(150, 23);
            this.m_txtInputValue.TabIndex = 1800;
            this.m_txtInputValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_grbOther
            // 
            this.m_grbOther.Controls.Add(this.lblOtherItem);
            this.m_grbOther.Controls.Add(this.m_cboOtherUnit);
            this.m_grbOther.Controls.Add(this.label1);
            this.m_grbOther.ForeColor = System.Drawing.Color.Black;
            this.m_grbOther.Location = new System.Drawing.Point(-112, 16);
            this.m_grbOther.Name = "m_grbOther";
            this.m_grbOther.Size = new System.Drawing.Size(260, 132);
            this.m_grbOther.TabIndex = 1400;
            this.m_grbOther.TabStop = false;
            this.m_grbOther.Text = "����";
            // 
            // lblOtherItem
            // 
            this.lblOtherItem.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOtherItem.ForeColor = System.Drawing.Color.Black;
            this.lblOtherItem.Location = new System.Drawing.Point(16, 39);
            this.lblOtherItem.Name = "lblOtherItem";
            this.lblOtherItem.Size = new System.Drawing.Size(56, 20);
            this.lblOtherItem.TabIndex = 465;
            this.lblOtherItem.Text = "��Ŀ��";
            // 
            // m_cboOtherUnit
            // 
            this.m_cboOtherUnit.BackColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.BorderColor = System.Drawing.Color.Black;
            this.m_cboOtherUnit.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboOtherUnit.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboOtherUnit.DropButtonForeColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboOtherUnit.flatFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOtherUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOtherUnit.ForeColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboOtherUnit.ListForeColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboOtherUnit.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.Location = new System.Drawing.Point(148, 76);
            this.m_cboOtherUnit.m_BlnEnableItemEventMenu = true;
            this.m_cboOtherUnit.Name = "m_cboOtherUnit";
            this.m_cboOtherUnit.SelectedIndex = -1;
            this.m_cboOtherUnit.SelectedItem = null;
            this.m_cboOtherUnit.SelectionStart = 0;
            this.m_cboOtherUnit.Size = new System.Drawing.Size(80, 26);
            this.m_cboOtherUnit.TabIndex = 1403;
            this.m_cboOtherUnit.TextBackColor = System.Drawing.Color.White;
            this.m_cboOtherUnit.TextForeColor = System.Drawing.Color.Black;
            this.m_cboOtherUnit.Visible = false;
            this.m_cboOtherUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            this.m_cboOtherUnit.DropDown += new System.EventHandler(this.m_cboOtherUnit_DropDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 465;
            this.label1.Text = "��ֵ��";
            // 
            // m_cboOtherItem
            // 
            this.m_cboOtherItem.BackColor = System.Drawing.Color.White;
            this.m_cboOtherItem.BorderColor = System.Drawing.Color.Black;
            this.m_cboOtherItem.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboOtherItem.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboOtherItem.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboOtherItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboOtherItem.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOtherItem.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboOtherItem.ForeColor = System.Drawing.Color.Black;
            this.m_cboOtherItem.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboOtherItem.ListForeColor = System.Drawing.Color.Black;
            this.m_cboOtherItem.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboOtherItem.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboOtherItem.Location = new System.Drawing.Point(72, 384);
            this.m_cboOtherItem.m_BlnEnableItemEventMenu = true;
            this.m_cboOtherItem.Name = "m_cboOtherItem";
            this.m_cboOtherItem.SelectedIndex = -1;
            this.m_cboOtherItem.SelectedItem = null;
            this.m_cboOtherItem.SelectionStart = 0;
            this.m_cboOtherItem.Size = new System.Drawing.Size(128, 23);
            this.m_cboOtherItem.TabIndex = 2200;
            this.m_cboOtherItem.TextBackColor = System.Drawing.Color.White;
            this.m_cboOtherItem.TextForeColor = System.Drawing.Color.Black;
            this.m_cboOtherItem.evtDelItem += new System.EventHandler(this.m_cboOtherItem_evtDelItem);
            this.m_cboOtherItem.evtAddItem += new System.EventHandler(this.m_cboOtherItem_evtAddItem);
            this.m_cboOtherItem.evtModifyItem += new System.EventHandler(this.m_cboOtherItem_evtModifyItem);
            this.m_cboOtherItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            this.m_cboOtherItem.DropDown += new System.EventHandler(this.m_cboOtherItem_DropDown);
            // 
            // m_txtOtherValue
            // 
            this.m_txtOtherValue.AccessibleName = "";
            this.m_txtOtherValue.BackColor = System.Drawing.Color.White;
            this.m_txtOtherValue.BorderColor = System.Drawing.Color.White;
            this.m_txtOtherValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtOtherValue.Location = new System.Drawing.Point(243, 384);
            this.m_txtOtherValue.Name = "m_txtOtherValue";
            this.m_txtOtherValue.Size = new System.Drawing.Size(71, 23);
            this.m_txtOtherValue.TabIndex = 2300;
            this.m_txtOtherValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblRecordTime
            // 
            this.lblRecordTime.AutoSize = true;
            this.lblRecordTime.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecordTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecordTime.ForeColor = System.Drawing.Color.Black;
            this.lblRecordTime.Location = new System.Drawing.Point(11, 23);
            this.lblRecordTime.Name = "lblRecordTime";
            this.lblRecordTime.Size = new System.Drawing.Size(70, 14);
            this.lblRecordTime.TabIndex = 503;
            this.lblRecordTime.Text = "��¼����:";
            this.lblRecordTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblOutStreamUnit
            // 
            this.lblOutStreamUnit.AutoSize = true;
            this.lblOutStreamUnit.BackColor = System.Drawing.SystemColors.Control;
            this.lblOutStreamUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutStreamUnit.ForeColor = System.Drawing.Color.Black;
            this.lblOutStreamUnit.Location = new System.Drawing.Point(238, 75);
            this.lblOutStreamUnit.Name = "lblOutStreamUnit";
            this.lblOutStreamUnit.Size = new System.Drawing.Size(21, 14);
            this.lblOutStreamUnit.TabIndex = 465;
            this.lblOutStreamUnit.Text = "ml";
            this.lblOutStreamUnit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_txtOutStreamValue
            // 
            this.m_txtOutStreamValue.AccessibleName = "";
            this.m_txtOutStreamValue.BackColor = System.Drawing.Color.White;
            this.m_txtOutStreamValue.BorderColor = System.Drawing.Color.White;
            this.m_txtOutStreamValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtOutStreamValue.Location = new System.Drawing.Point(67, 71);
            this.m_txtOutStreamValue.Name = "m_txtOutStreamValue";
            this.m_txtOutStreamValue.Size = new System.Drawing.Size(160, 23);
            this.m_txtOutStreamValue.TabIndex = 1700;
            this.m_txtOutStreamValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblPeeUnit
            // 
            this.lblPeeUnit.AutoSize = true;
            this.lblPeeUnit.BackColor = System.Drawing.SystemColors.Control;
            this.lblPeeUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPeeUnit.ForeColor = System.Drawing.Color.Black;
            this.lblPeeUnit.Location = new System.Drawing.Point(238, 48);
            this.lblPeeUnit.Name = "lblPeeUnit";
            this.lblPeeUnit.Size = new System.Drawing.Size(21, 14);
            this.lblPeeUnit.TabIndex = 465;
            this.lblPeeUnit.Text = "ml";
            this.lblPeeUnit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdSave.ForeColor = System.Drawing.Color.Black;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(38, 533);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(100, 30);
            this.m_cmdSave.TabIndex = 2400;
            this.m_cmdSave.TabStop = false;
            this.m_cmdSave.Text = "����";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            this.m_cmdSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_dtpRecordDateTime
            // 
            this.m_dtpRecordDateTime.BackColor = System.Drawing.Color.White;
            this.m_dtpRecordDateTime.BorderColor = System.Drawing.Color.Black;
            this.m_dtpRecordDateTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpRecordDateTime.DropButtonBackColor = System.Drawing.Color.White;
            this.m_dtpRecordDateTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpRecordDateTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpRecordDateTime.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpRecordDateTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpRecordDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpRecordDateTime.Location = new System.Drawing.Point(95, 20);
            this.m_dtpRecordDateTime.m_BlnOnlyTime = false;
            this.m_dtpRecordDateTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpRecordDateTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpRecordDateTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpRecordDateTime.Name = "m_dtpRecordDateTime";
            this.m_dtpRecordDateTime.ReadOnly = false;
            this.m_dtpRecordDateTime.Size = new System.Drawing.Size(137, 22);
            this.m_dtpRecordDateTime.TabIndex = 35;
            this.m_dtpRecordDateTime.TabStop = false;
            this.m_dtpRecordDateTime.TextBackColor = System.Drawing.Color.White;
            this.m_dtpRecordDateTime.TextForeColor = System.Drawing.Color.Black;
            this.m_dtpRecordDateTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_grbSkinTest
            // 
            this.m_grbSkinTest.BackColor = System.Drawing.SystemColors.Control;
            this.m_grbSkinTest.Controls.Add(this.m_nmuSkinBadCount);
            this.m_grbSkinTest.Controls.Add(this.m_rdbSkinTestGood);
            this.m_grbSkinTest.Controls.Add(this.m_rdbSkinTestBad);
            this.m_grbSkinTest.Controls.Add(this.lblSkinTestMedicine);
            this.m_grbSkinTest.ForeColor = System.Drawing.Color.Black;
            this.m_grbSkinTest.Location = new System.Drawing.Point(-140, 16);
            this.m_grbSkinTest.Name = "m_grbSkinTest";
            this.m_grbSkinTest.Size = new System.Drawing.Size(260, 132);
            this.m_grbSkinTest.TabIndex = 1300;
            this.m_grbSkinTest.TabStop = false;
            this.m_grbSkinTest.Text = "Ƥ��";
            // 
            // m_nmuSkinBadCount
            // 
            this.m_nmuSkinBadCount.AccessibleName = "save";
            this.m_nmuSkinBadCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_nmuSkinBadCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_nmuSkinBadCount.ForeColor = System.Drawing.Color.White;
            this.m_nmuSkinBadCount.Location = new System.Drawing.Point(156, 95);
            this.m_nmuSkinBadCount.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.m_nmuSkinBadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_nmuSkinBadCount.Name = "m_nmuSkinBadCount";
            this.m_nmuSkinBadCount.Size = new System.Drawing.Size(36, 19);
            this.m_nmuSkinBadCount.TabIndex = 1304;
            this.m_nmuSkinBadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_nmuSkinBadCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_rdbSkinTestGood
            // 
            this.m_rdbSkinTestGood.Checked = true;
            this.m_rdbSkinTestGood.Location = new System.Drawing.Point(24, 92);
            this.m_rdbSkinTestGood.Name = "m_rdbSkinTestGood";
            this.m_rdbSkinTestGood.Size = new System.Drawing.Size(60, 24);
            this.m_rdbSkinTestGood.TabIndex = 1302;
            this.m_rdbSkinTestGood.TabStop = true;
            this.m_rdbSkinTestGood.Text = "����";
            this.m_rdbSkinTestGood.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_rdbSkinTestBad
            // 
            this.m_rdbSkinTestBad.Location = new System.Drawing.Point(96, 92);
            this.m_rdbSkinTestBad.Name = "m_rdbSkinTestBad";
            this.m_rdbSkinTestBad.Size = new System.Drawing.Size(56, 24);
            this.m_rdbSkinTestBad.TabIndex = 1303;
            this.m_rdbSkinTestBad.Text = "����";
            this.m_rdbSkinTestBad.CheckedChanged += new System.EventHandler(this.m_rdbSkinTestBad_CheckedChanged);
            this.m_rdbSkinTestBad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblSkinTestMedicine
            // 
            this.lblSkinTestMedicine.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSkinTestMedicine.ForeColor = System.Drawing.Color.Black;
            this.lblSkinTestMedicine.Location = new System.Drawing.Point(16, 44);
            this.lblSkinTestMedicine.Name = "lblSkinTestMedicine";
            this.lblSkinTestMedicine.Size = new System.Drawing.Size(56, 20);
            this.lblSkinTestMedicine.TabIndex = 465;
            this.lblSkinTestMedicine.Text = "ҩ�";
            // 
            // m_cboSkinTestMedicine
            // 
            this.m_cboSkinTestMedicine.BackColor = System.Drawing.Color.White;
            this.m_cboSkinTestMedicine.BorderColor = System.Drawing.Color.Black;
            this.m_cboSkinTestMedicine.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSkinTestMedicine.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSkinTestMedicine.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSkinTestMedicine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSkinTestMedicine.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSkinTestMedicine.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSkinTestMedicine.ForeColor = System.Drawing.Color.Black;
            this.m_cboSkinTestMedicine.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboSkinTestMedicine.ListForeColor = System.Drawing.Color.Black;
            this.m_cboSkinTestMedicine.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboSkinTestMedicine.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboSkinTestMedicine.Location = new System.Drawing.Point(72, 355);
            this.m_cboSkinTestMedicine.m_BlnEnableItemEventMenu = true;
            this.m_cboSkinTestMedicine.Name = "m_cboSkinTestMedicine";
            this.m_cboSkinTestMedicine.SelectedIndex = -1;
            this.m_cboSkinTestMedicine.SelectedItem = null;
            this.m_cboSkinTestMedicine.SelectionStart = 0;
            this.m_cboSkinTestMedicine.Size = new System.Drawing.Size(128, 23);
            this.m_cboSkinTestMedicine.TabIndex = 2000;
            this.m_cboSkinTestMedicine.TextBackColor = System.Drawing.Color.White;
            this.m_cboSkinTestMedicine.TextForeColor = System.Drawing.Color.Black;
            this.m_cboSkinTestMedicine.evtDelItem += new System.EventHandler(this.m_cboSkinTestMedicine_evtDelItem);
            this.m_cboSkinTestMedicine.evtAddItem += new System.EventHandler(this.m_cboSkinTestMedicine_evtAddItem);
            this.m_cboSkinTestMedicine.evtModifyItem += new System.EventHandler(this.m_cboSkinTestMedicine_evtModifyItem);
            this.m_cboSkinTestMedicine.evtTextChanged += new System.EventHandler(this.m_cboSkinTestMedicine_evtTextChanged);
            this.m_cboSkinTestMedicine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            this.m_cboSkinTestMedicine.DropDown += new System.EventHandler(this.m_cboSkinTestMedicine_DropDown);
            // 
            // lblEventHour
            // 
            this.lblEventHour.AutoSize = true;
            this.lblEventHour.Location = new System.Drawing.Point(219, 53);
            this.lblEventHour.Name = "lblEventHour";
            this.lblEventHour.Size = new System.Drawing.Size(21, 14);
            this.lblEventHour.TabIndex = 808;
            this.lblEventHour.Text = "ʱ";
            this.lblEventHour.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_nmuEventHour
            // 
            this.m_nmuEventHour.BackColor = System.Drawing.Color.White;
            this.m_nmuEventHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_nmuEventHour.ForeColor = System.Drawing.Color.Black;
            this.m_nmuEventHour.Location = new System.Drawing.Point(179, 49);
            this.m_nmuEventHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.m_nmuEventHour.Name = "m_nmuEventHour";
            this.m_nmuEventHour.Size = new System.Drawing.Size(36, 23);
            this.m_nmuEventHour.TabIndex = 150;
            this.m_nmuEventHour.TabStop = false;
            this.m_nmuEventHour.TextChanged += new System.EventHandler(this.m_nmuEventHour_ValueChanged);
            this.m_nmuEventHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_nmuEventMinute
            // 
            this.m_nmuEventMinute.AccessibleName = "save";
            this.m_nmuEventMinute.BackColor = System.Drawing.Color.White;
            this.m_nmuEventMinute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_nmuEventMinute.ForeColor = System.Drawing.Color.Black;
            this.m_nmuEventMinute.Location = new System.Drawing.Point(243, 49);
            this.m_nmuEventMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.m_nmuEventMinute.Name = "m_nmuEventMinute";
            this.m_nmuEventMinute.Size = new System.Drawing.Size(36, 23);
            this.m_nmuEventMinute.TabIndex = 200;
            this.m_nmuEventMinute.TabStop = false;
            this.m_nmuEventMinute.TextChanged += new System.EventHandler(this.m_nmuEventMinute_ValueChanged);
            this.m_nmuEventMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblEventMinute
            // 
            this.lblEventMinute.AutoSize = true;
            this.lblEventMinute.Location = new System.Drawing.Point(287, 53);
            this.lblEventMinute.Name = "lblEventMinute";
            this.lblEventMinute.Size = new System.Drawing.Size(21, 14);
            this.lblEventMinute.TabIndex = 807;
            this.lblEventMinute.Text = "��";
            // 
            // m_cboEventType
            // 
            this.m_cboEventType.AccessibleName = "";
            this.m_cboEventType.BackColor = System.Drawing.Color.White;
            this.m_cboEventType.BorderColor = System.Drawing.Color.Black;
            this.m_cboEventType.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboEventType.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboEventType.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboEventType.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboEventType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboEventType.ForeColor = System.Drawing.Color.Black;
            this.m_cboEventType.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboEventType.ListForeColor = System.Drawing.Color.Black;
            this.m_cboEventType.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboEventType.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboEventType.Location = new System.Drawing.Point(95, 49);
            this.m_cboEventType.m_BlnEnableItemEventMenu = false;
            this.m_cboEventType.Name = "m_cboEventType";
            this.m_cboEventType.SelectedIndex = -1;
            this.m_cboEventType.SelectedItem = null;
            this.m_cboEventType.SelectionStart = 0;
            this.m_cboEventType.Size = new System.Drawing.Size(76, 23);
            this.m_cboEventType.TabIndex = 200;
            this.m_cboEventType.TextBackColor = System.Drawing.Color.White;
            this.m_cboEventType.TextForeColor = System.Drawing.Color.Black;
            this.m_cboEventType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_chkNeedWeight
            // 
            this.m_chkNeedWeight.Location = new System.Drawing.Point(173, 16);
            this.m_chkNeedWeight.Name = "m_chkNeedWeight";
            this.m_chkNeedWeight.Size = new System.Drawing.Size(62, 22);
            this.m_chkNeedWeight.TabIndex = 1300;
            this.m_chkNeedWeight.TabStop = false;
            this.m_chkNeedWeight.Text = "�����";
            this.m_chkNeedWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_chkNeedWeight.CheckedChanged += new System.EventHandler(this.m_chkNeedWeight_CheckedChanged);
            this.m_chkNeedWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblDejectaWeightUnit
            // 
            this.lblDejectaWeightUnit.AutoSize = true;
            this.lblDejectaWeightUnit.BackColor = System.Drawing.SystemColors.Control;
            this.lblDejectaWeightUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDejectaWeightUnit.ForeColor = System.Drawing.Color.Black;
            this.lblDejectaWeightUnit.Location = new System.Drawing.Point(281, 20);
            this.lblDejectaWeightUnit.Name = "lblDejectaWeightUnit";
            this.lblDejectaWeightUnit.Size = new System.Drawing.Size(21, 14);
            this.lblDejectaWeightUnit.TabIndex = 465;
            this.lblDejectaWeightUnit.Text = "��";
            this.lblDejectaWeightUnit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_txtDejectaWeightValue
            // 
            this.m_txtDejectaWeightValue.BackColor = System.Drawing.Color.White;
            this.m_txtDejectaWeightValue.BorderColor = System.Drawing.Color.White;
            this.m_txtDejectaWeightValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtDejectaWeightValue.Location = new System.Drawing.Point(238, 16);
            this.m_txtDejectaWeightValue.Name = "m_txtDejectaWeightValue";
            this.m_txtDejectaWeightValue.Size = new System.Drawing.Size(34, 23);
            this.m_txtDejectaWeightValue.TabIndex = 1400;
            this.m_txtDejectaWeightValue.TabStop = false;
            this.m_txtDejectaWeightValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_txtPressureDiastolicValue
            // 
            this.m_txtPressureDiastolicValue.AccessibleName = "";
            this.m_txtPressureDiastolicValue.BackColor = System.Drawing.Color.White;
            this.m_txtPressureDiastolicValue.BorderColor = System.Drawing.Color.White;
            this.m_txtPressureDiastolicValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtPressureDiastolicValue.Location = new System.Drawing.Point(160, 164);
            this.m_txtPressureDiastolicValue.Name = "m_txtPressureDiastolicValue";
            this.m_txtPressureDiastolicValue.Size = new System.Drawing.Size(72, 23);
            this.m_txtPressureDiastolicValue.TabIndex = 1100;
            this.m_txtPressureDiastolicValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_txtPressureSystolicValue
            // 
            this.m_txtPressureSystolicValue.BackColor = System.Drawing.Color.White;
            this.m_txtPressureSystolicValue.BorderColor = System.Drawing.Color.White;
            this.m_txtPressureSystolicValue.ForeColor = System.Drawing.Color.Black;
            this.m_txtPressureSystolicValue.Location = new System.Drawing.Point(72, 164);
            this.m_txtPressureSystolicValue.Name = "m_txtPressureSystolicValue";
            this.m_txtPressureSystolicValue.Size = new System.Drawing.Size(68, 23);
            this.m_txtPressureSystolicValue.TabIndex = 1000;
            this.m_txtPressureSystolicValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_cmdGetDovueData
            // 
            this.m_cmdGetDovueData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGetDovueData.DefaultScheme = true;
            this.m_cmdGetDovueData.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetDovueData.Hint = "";
            this.m_cmdGetDovueData.Location = new System.Drawing.Point(37, 570);
            this.m_cmdGetDovueData.Name = "m_cmdGetDovueData";
            this.m_cmdGetDovueData.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetDovueData.Size = new System.Drawing.Size(100, 30);
            this.m_cmdGetDovueData.TabIndex = 2600;
            this.m_cmdGetDovueData.TabStop = false;
            this.m_cmdGetDovueData.Text = "�໤������";
            this.m_cmdGetDovueData.Click += new System.EventHandler(this.m_cmdGetDovueData_Click);
            // 
            // m_chkPulseIsHalf
            // 
            this.m_chkPulseIsHalf.Location = new System.Drawing.Point(243, 107);
            this.m_chkPulseIsHalf.Name = "m_chkPulseIsHalf";
            this.m_chkPulseIsHalf.Size = new System.Drawing.Size(71, 22);
            this.m_chkPulseIsHalf.TabIndex = 800;
            this.m_chkPulseIsHalf.TabStop = false;
            this.m_chkPulseIsHalf.Text = "��Сʱ";
            this.m_chkPulseIsHalf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdDelete.ForeColor = System.Drawing.Color.Black;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(157, 533);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(100, 30);
            this.m_cmdDelete.TabIndex = 2500;
            this.m_cmdDelete.TabStop = false;
            this.m_cmdDelete.Text = "ɾ��";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            this.m_cmdDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_chkTemperatureIsHalf
            // 
            this.m_chkTemperatureIsHalf.Location = new System.Drawing.Point(243, 82);
            this.m_chkTemperatureIsHalf.Name = "m_chkTemperatureIsHalf";
            this.m_chkTemperatureIsHalf.Size = new System.Drawing.Size(71, 19);
            this.m_chkTemperatureIsHalf.TabIndex = 400;
            this.m_chkTemperatureIsHalf.TabStop = false;
            this.m_chkTemperatureIsHalf.Text = "��Сʱ";
            this.m_chkTemperatureIsHalf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_pdcRecord
            // 
            this.m_pdcRecord.DocumentName = "�����";
            // 
            // m_ppdRecord
            // 
            this.m_ppdRecord.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.m_ppdRecord.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.m_ppdRecord.ClientSize = new System.Drawing.Size(400, 300);
            this.m_ppdRecord.Document = this.m_pdcRecord;
            this.m_ppdRecord.Enabled = true;
            this.m_ppdRecord.Icon = ((System.Drawing.Icon)(resources.GetObject("m_ppdRecord.Icon")));
            this.m_ppdRecord.Name = "m_ppdRecord";
            this.m_ppdRecord.Visible = false;
            // 
            // m_cboTemperature
            // 
            this.m_cboTemperature.BackColor = System.Drawing.Color.White;
            this.m_cboTemperature.BorderColor = System.Drawing.Color.Black;
            this.m_cboTemperature.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboTemperature.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboTemperature.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboTemperature.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperature.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperature.ForeColor = System.Drawing.Color.Black;
            this.m_cboTemperature.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboTemperature.ListForeColor = System.Drawing.Color.Black;
            this.m_cboTemperature.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboTemperature.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboTemperature.Location = new System.Drawing.Point(95, 77);
            this.m_cboTemperature.m_BlnEnableItemEventMenu = false;
            this.m_cboTemperature.Name = "m_cboTemperature";
            this.m_cboTemperature.SelectedIndex = -1;
            this.m_cboTemperature.SelectedItem = null;
            this.m_cboTemperature.SelectionStart = 0;
            this.m_cboTemperature.Size = new System.Drawing.Size(76, 23);
            this.m_cboTemperature.TabIndex = 300;
            this.m_cboTemperature.TextBackColor = System.Drawing.Color.White;
            this.m_cboTemperature.TextForeColor = System.Drawing.Color.Black;
            this.m_tipPrompt.SetToolTip(this.m_cboTemperature, "��������Ч����");
            this.m_cboTemperature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_cboPulse
            // 
            this.m_cboPulse.BackColor = System.Drawing.Color.White;
            this.m_cboPulse.BorderColor = System.Drawing.Color.Black;
            this.m_cboPulse.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboPulse.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboPulse.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboPulse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboPulse.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulse.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulse.ForeColor = System.Drawing.Color.Black;
            this.m_cboPulse.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboPulse.ListForeColor = System.Drawing.Color.Black;
            this.m_cboPulse.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboPulse.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboPulse.Location = new System.Drawing.Point(95, 108);
            this.m_cboPulse.m_BlnEnableItemEventMenu = false;
            this.m_cboPulse.Name = "m_cboPulse";
            this.m_cboPulse.SelectedIndex = -1;
            this.m_cboPulse.SelectedItem = null;
            this.m_cboPulse.SelectionStart = 0;
            this.m_cboPulse.Size = new System.Drawing.Size(76, 23);
            this.m_cboPulse.TabIndex = 700;
            this.m_cboPulse.TextBackColor = System.Drawing.Color.White;
            this.m_cboPulse.TextForeColor = System.Drawing.Color.Black;
            this.m_tipPrompt.SetToolTip(this.m_cboPulse, "��������Ч����");
            this.m_cboPulse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // m_txtWhichWeek
            // 
            this.m_txtWhichWeek.AccessibleName = "NoDefault";
            this.m_txtWhichWeek.BackColor = System.Drawing.Color.White;
            this.m_txtWhichWeek.BorderColor = System.Drawing.Color.White;
            this.m_txtWhichWeek.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtWhichWeek.ForeColor = System.Drawing.Color.Black;
            this.m_txtWhichWeek.Location = new System.Drawing.Point(536, 0);
            this.m_txtWhichWeek.Name = "m_txtWhichWeek";
            this.m_txtWhichWeek.Size = new System.Drawing.Size(28, 23);
            this.m_txtWhichWeek.TabIndex = 10000008;
            this.m_txtWhichWeek.TabStop = false;
            this.m_txtWhichWeek.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_tipPrompt.SetToolTip(this.m_txtWhichWeek, "�ڼ���");
            this.m_txtWhichWeek.Click += new System.EventHandler(this.m_txtWhichWeek_TextChanged);
            // 
            // m_cmdNextWeek
            // 
            this.m_cmdNextWeek.ForeColor = System.Drawing.Color.Black;
            this.m_cmdNextWeek.Location = new System.Drawing.Point(563, 0);
            this.m_cmdNextWeek.Name = "m_cmdNextWeek";
            this.m_cmdNextWeek.Size = new System.Drawing.Size(32, 20);
            this.m_cmdNextWeek.TabIndex = 10000012;
            this.m_cmdNextWeek.TabStop = false;
            this.m_cmdNextWeek.Text = ">";
            this.m_tipPrompt.SetToolTip(this.m_cmdNextWeek, "��һ��");
            this.m_cmdNextWeek.Click += new System.EventHandler(this.m_cmdNextWeek_Click);
            // 
            // m_cmdFinalWeek
            // 
            this.m_cmdFinalWeek.ForeColor = System.Drawing.Color.Black;
            this.m_cmdFinalWeek.Location = new System.Drawing.Point(591, 0);
            this.m_cmdFinalWeek.Name = "m_cmdFinalWeek";
            this.m_cmdFinalWeek.Size = new System.Drawing.Size(32, 20);
            this.m_cmdFinalWeek.TabIndex = 10000011;
            this.m_cmdFinalWeek.TabStop = false;
            this.m_cmdFinalWeek.Text = ">|";
            this.m_tipPrompt.SetToolTip(this.m_cmdFinalWeek, "���һ��");
            this.m_cmdFinalWeek.Click += new System.EventHandler(this.m_cmdFinalWeek_Click);
            // 
            // m_cmdPreWeek
            // 
            this.m_cmdPreWeek.ForeColor = System.Drawing.Color.Black;
            this.m_cmdPreWeek.Location = new System.Drawing.Point(508, 0);
            this.m_cmdPreWeek.Name = "m_cmdPreWeek";
            this.m_cmdPreWeek.Size = new System.Drawing.Size(32, 20);
            this.m_cmdPreWeek.TabIndex = 10000009;
            this.m_cmdPreWeek.TabStop = false;
            this.m_cmdPreWeek.Text = "<";
            this.m_tipPrompt.SetToolTip(this.m_cmdPreWeek, "��һ��");
            this.m_cmdPreWeek.Click += new System.EventHandler(this.m_cmdPreWeek_Click);
            // 
            // m_cmdFirstWeek
            // 
            this.m_cmdFirstWeek.ForeColor = System.Drawing.Color.Black;
            this.m_cmdFirstWeek.Location = new System.Drawing.Point(480, 0);
            this.m_cmdFirstWeek.Name = "m_cmdFirstWeek";
            this.m_cmdFirstWeek.Size = new System.Drawing.Size(32, 20);
            this.m_cmdFirstWeek.TabIndex = 10000010;
            this.m_cmdFirstWeek.TabStop = false;
            this.m_cmdFirstWeek.Text = "|<";
            this.m_tipPrompt.SetToolTip(this.m_cmdFirstWeek, "��һ��");
            this.m_cmdFirstWeek.Click += new System.EventHandler(this.m_cmdFirstWeek_Click);
            // 
            // m_picZoom
            // 
            this.m_picZoom.Image = ((System.Drawing.Image)(resources.GetObject("m_picZoom.Image")));
            this.m_picZoom.Location = new System.Drawing.Point(4, 2);
            this.m_picZoom.Name = "m_picZoom";
            this.m_picZoom.Size = new System.Drawing.Size(16, 16);
            this.m_picZoom.TabIndex = 10000003;
            this.m_picZoom.TabStop = false;
            this.m_picZoom.Tag = "1";
            this.m_tipPrompt.SetToolTip(this.m_picZoom, "�Ŵ�");
            this.m_picZoom.Click += new System.EventHandler(this.m_picZoom_Click);
            // 
            // m_imgZoom
            // 
            this.m_imgZoom.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgZoom.ImageStream")));
            this.m_imgZoom.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgZoom.Images.SetKeyName(0, "");
            this.m_imgZoom.Images.SetKeyName(1, "");
            // 
            // m_grpOperation
            // 
            this.m_grpOperation.BackColor = System.Drawing.SystemColors.Control;
            this.m_grpOperation.Controls.Add(this.groupBox3);
            this.m_grpOperation.Controls.Add(this.label22);
            this.m_grpOperation.Controls.Add(this.m_dtpGetDataTime);
            this.m_grpOperation.Controls.Add(this.m_txtInputValue);
            this.m_grpOperation.Controls.Add(this.label23);
            this.m_grpOperation.Controls.Add(this.label24);
            this.m_grpOperation.Controls.Add(this.label18);
            this.m_grpOperation.Controls.Add(this.m_cboSkinBadCount);
            this.m_grpOperation.Controls.Add(this.label11);
            this.m_grpOperation.Controls.Add(this.label16);
            this.m_grpOperation.Controls.Add(this.m_cboWeightValue);
            this.m_grpOperation.Controls.Add(this.label20);
            this.m_grpOperation.Controls.Add(this.label6);
            this.m_grpOperation.Controls.Add(this.label7);
            this.m_grpOperation.Controls.Add(this.m_cboBreathValue);
            this.m_grpOperation.Controls.Add(this.label2);
            this.m_grpOperation.Controls.Add(this.m_cboPulseType);
            this.m_grpOperation.Controls.Add(this.label4);
            this.m_grpOperation.Controls.Add(this.m_cboTemperatureType);
            this.m_grpOperation.Controls.Add(this.label9);
            this.m_grpOperation.Controls.Add(this.m_cboTemperature);
            this.m_grpOperation.Controls.Add(this.m_cboPulse);
            this.m_grpOperation.Controls.Add(this.label3);
            this.m_grpOperation.Controls.Add(this.m_cboTimeFlag);
            this.m_grpOperation.Controls.Add(this.lblRecordTime);
            this.m_grpOperation.Controls.Add(this.m_dtpRecordDateTime);
            this.m_grpOperation.Controls.Add(this.m_chkTemperatureIsHalf);
            this.m_grpOperation.Controls.Add(this.m_txtPressureSystolicValue);
            this.m_grpOperation.Controls.Add(this.m_txtPressureDiastolicValue);
            this.m_grpOperation.Controls.Add(this.lblWeightUnit);
            this.m_grpOperation.Controls.Add(this.m_cboSkinTestMedicine);
            this.m_grpOperation.Controls.Add(this.m_cboOtherItem);
            this.m_grpOperation.Controls.Add(this.m_txtOtherValue);
            this.m_grpOperation.Controls.Add(this.m_chkPulseIsHalf);
            this.m_grpOperation.Controls.Add(this.m_cboEventType);
            this.m_grpOperation.Controls.Add(this.m_nmuEventHour);
            this.m_grpOperation.Controls.Add(this.lblEventMinute);
            this.m_grpOperation.Controls.Add(this.m_nmuEventMinute);
            this.m_grpOperation.Controls.Add(this.lblEventHour);
            this.m_grpOperation.Controls.Add(this.panel5);
            this.m_grpOperation.Controls.Add(this.label13);
            this.m_grpOperation.Controls.Add(this.checkBox1);
            this.m_grpOperation.Controls.Add(this.label19);
            this.m_grpOperation.Controls.Add(this.label21);
            this.m_grpOperation.ForeColor = System.Drawing.Color.Black;
            this.m_grpOperation.Location = new System.Drawing.Point(2, 65);
            this.m_grpOperation.Name = "m_grpOperation";
            this.m_grpOperation.Size = new System.Drawing.Size(322, 457);
            this.m_grpOperation.TabIndex = 10000003;
            this.m_grpOperation.TabStop = false;
            this.m_grpOperation.Text = "�������";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.lblDejectaWeightUnit);
            this.groupBox3.Controls.Add(this.m_txtDejectaWeightValue);
            this.groupBox3.Controls.Add(this.m_chkNeedWeight);
            this.groupBox3.Controls.Add(this.m_txtOutStreamValue);
            this.groupBox3.Controls.Add(this.lblOutStreamUnit);
            this.groupBox3.Controls.Add(this.lblPeeUnit);
            this.groupBox3.Controls.Add(this.m_cboDejectaBeforeTimes);
            this.groupBox3.Controls.Add(this.lblDejecta);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.m_cboPeeValue);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(6, 221);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 98);
            this.groupBox3.TabIndex = 2302;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "�ų���";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 14);
            this.label10.TabIndex = 1403;
            this.label10.Text = "��㣺";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(239, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 14);
            this.label17.TabIndex = 465;
            this.label17.Text = "ml";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboDejectaBeforeTimes
            // 
            this.m_cboDejectaBeforeTimes.BackColor = System.Drawing.Color.White;
            this.m_cboDejectaBeforeTimes.BorderColor = System.Drawing.Color.Black;
            this.m_cboDejectaBeforeTimes.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboDejectaBeforeTimes.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboDejectaBeforeTimes.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboDejectaBeforeTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboDejectaBeforeTimes.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDejectaBeforeTimes.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDejectaBeforeTimes.ForeColor = System.Drawing.Color.Black;
            this.m_cboDejectaBeforeTimes.ListBackColor = System.Drawing.Color.White;
            this.m_cboDejectaBeforeTimes.ListForeColor = System.Drawing.Color.Black;
            this.m_cboDejectaBeforeTimes.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboDejectaBeforeTimes.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboDejectaBeforeTimes.Location = new System.Drawing.Point(67, 16);
            this.m_cboDejectaBeforeTimes.m_BlnEnableItemEventMenu = false;
            this.m_cboDejectaBeforeTimes.Name = "m_cboDejectaBeforeTimes";
            this.m_cboDejectaBeforeTimes.SelectedIndex = -1;
            this.m_cboDejectaBeforeTimes.SelectedItem = null;
            this.m_cboDejectaBeforeTimes.SelectionStart = 0;
            this.m_cboDejectaBeforeTimes.Size = new System.Drawing.Size(76, 23);
            this.m_cboDejectaBeforeTimes.TabIndex = 1200;
            this.m_cboDejectaBeforeTimes.TextBackColor = System.Drawing.Color.White;
            this.m_cboDejectaBeforeTimes.TextForeColor = System.Drawing.Color.Black;
            this.m_cboDejectaBeforeTimes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // lblDejecta
            // 
            this.lblDejecta.AutoSize = true;
            this.lblDejecta.BackColor = System.Drawing.SystemColors.Control;
            this.lblDejecta.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDejecta.ForeColor = System.Drawing.Color.Black;
            this.lblDejecta.Location = new System.Drawing.Point(149, 20);
            this.lblDejecta.Name = "lblDejecta";
            this.lblDejecta.Size = new System.Drawing.Size(21, 14);
            this.lblDejecta.TabIndex = 1106;
            this.lblDejecta.Text = "��";
            this.lblDejecta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 1108;
            this.label12.Text = "������";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboPeeValue
            // 
            this.m_cboPeeValue.BackColor = System.Drawing.Color.White;
            this.m_cboPeeValue.BorderColor = System.Drawing.Color.Black;
            this.m_cboPeeValue.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboPeeValue.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboPeeValue.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboPeeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboPeeValue.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPeeValue.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPeeValue.ForeColor = System.Drawing.Color.Black;
            this.m_cboPeeValue.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboPeeValue.ListForeColor = System.Drawing.Color.Black;
            this.m_cboPeeValue.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboPeeValue.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboPeeValue.Location = new System.Drawing.Point(67, 44);
            this.m_cboPeeValue.m_BlnEnableItemEventMenu = false;
            this.m_cboPeeValue.Name = "m_cboPeeValue";
            this.m_cboPeeValue.SelectedIndex = -1;
            this.m_cboPeeValue.SelectedItem = null;
            this.m_cboPeeValue.SelectionStart = 0;
            this.m_cboPeeValue.Size = new System.Drawing.Size(160, 23);
            this.m_cboPeeValue.TabIndex = 1600;
            this.m_cboPeeValue.TextBackColor = System.Drawing.Color.White;
            this.m_cboPeeValue.TextForeColor = System.Drawing.Color.Black;
            this.m_cboPeeValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 14);
            this.label15.TabIndex = 1110;
            this.label15.Text = "������";
            this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 420);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 14);
            this.label22.TabIndex = 0;
            this.label22.Text = "�ɼ�����ʱ��";
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.BackColor = System.Drawing.Color.White;
            this.m_dtpGetDataTime.BorderColor = System.Drawing.Color.Black;
            this.m_dtpGetDataTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpGetDataTime.DropButtonBackColor = System.Drawing.SystemColors.ControlLight;
            this.m_dtpGetDataTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpGetDataTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpGetDataTime.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpGetDataTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpGetDataTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(102, 417);
            this.m_dtpGetDataTime.m_BlnOnlyTime = false;
            this.m_dtpGetDataTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpGetDataTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpGetDataTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpGetDataTime.Name = "m_dtpGetDataTime";
            this.m_dtpGetDataTime.ReadOnly = false;
            this.m_dtpGetDataTime.Size = new System.Drawing.Size(212, 22);
            this.m_dtpGetDataTime.TabIndex = 35;
            this.m_dtpGetDataTime.TabStop = false;
            this.m_dtpGetDataTime.TextBackColor = System.Drawing.Color.White;
            this.m_dtpGetDataTime.TextForeColor = System.Drawing.Color.Black;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(201, 359);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(42, 14);
            this.label23.TabIndex = 1115;
            this.label23.Text = "���:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(201, 388);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(42, 14);
            this.label24.TabIndex = 1115;
            this.label24.Text = "���:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 388);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 14);
            this.label18.TabIndex = 1303;
            this.label18.Text = "������";
            this.label18.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboSkinBadCount
            // 
            this.m_cboSkinBadCount.BackColor = System.Drawing.Color.White;
            this.m_cboSkinBadCount.BorderColor = System.Drawing.Color.Black;
            this.m_cboSkinBadCount.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboSkinBadCount.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboSkinBadCount.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboSkinBadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboSkinBadCount.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSkinBadCount.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboSkinBadCount.ForeColor = System.Drawing.Color.Black;
            this.m_cboSkinBadCount.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboSkinBadCount.ListForeColor = System.Drawing.Color.Black;
            this.m_cboSkinBadCount.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboSkinBadCount.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboSkinBadCount.Location = new System.Drawing.Point(243, 355);
            this.m_cboSkinBadCount.m_BlnEnableItemEventMenu = false;
            this.m_cboSkinBadCount.Name = "m_cboSkinBadCount";
            this.m_cboSkinBadCount.SelectedIndex = -1;
            this.m_cboSkinBadCount.SelectedItem = null;
            this.m_cboSkinBadCount.SelectionStart = 0;
            this.m_cboSkinBadCount.Size = new System.Drawing.Size(71, 23);
            this.m_cboSkinBadCount.TabIndex = 2100;
            this.m_cboSkinBadCount.TextBackColor = System.Drawing.Color.White;
            this.m_cboSkinBadCount.TextForeColor = System.Drawing.Color.Black;
            this.m_cboSkinBadCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 359);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 1115;
            this.label11.Text = "Ƥ�ԣ�";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 329);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 14);
            this.label16.TabIndex = 1114;
            this.label16.Text = "���أ�";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboWeightValue
            // 
            this.m_cboWeightValue.BackColor = System.Drawing.Color.White;
            this.m_cboWeightValue.BorderColor = System.Drawing.Color.Black;
            this.m_cboWeightValue.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboWeightValue.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboWeightValue.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboWeightValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboWeightValue.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboWeightValue.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboWeightValue.ForeColor = System.Drawing.Color.Black;
            this.m_cboWeightValue.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboWeightValue.ListForeColor = System.Drawing.Color.Black;
            this.m_cboWeightValue.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboWeightValue.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboWeightValue.Location = new System.Drawing.Point(72, 325);
            this.m_cboWeightValue.m_BlnEnableItemEventMenu = false;
            this.m_cboWeightValue.Name = "m_cboWeightValue";
            this.m_cboWeightValue.SelectedIndex = -1;
            this.m_cboWeightValue.SelectedItem = null;
            this.m_cboWeightValue.SelectionStart = 0;
            this.m_cboWeightValue.Size = new System.Drawing.Size(160, 23);
            this.m_cboWeightValue.TabIndex = 1900;
            this.m_cboWeightValue.TextBackColor = System.Drawing.Color.White;
            this.m_cboWeightValue.TextForeColor = System.Drawing.Color.Black;
            this.m_cboWeightValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(11, 198);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 14);
            this.label20.TabIndex = 1111;
            this.label20.Text = "����Һ����";
            this.label20.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(144, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 1103;
            this.label6.Text = "/";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 518;
            this.label7.Text = "Ѫѹ��";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboBreathValue
            // 
            this.m_cboBreathValue.BackColor = System.Drawing.Color.White;
            this.m_cboBreathValue.BorderColor = System.Drawing.Color.Black;
            this.m_cboBreathValue.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboBreathValue.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboBreathValue.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboBreathValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboBreathValue.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBreathValue.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBreathValue.ForeColor = System.Drawing.Color.Black;
            this.m_cboBreathValue.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboBreathValue.ListForeColor = System.Drawing.Color.Black;
            this.m_cboBreathValue.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboBreathValue.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboBreathValue.Location = new System.Drawing.Point(72, 135);
            this.m_cboBreathValue.m_BlnEnableItemEventMenu = false;
            this.m_cboBreathValue.Name = "m_cboBreathValue";
            this.m_cboBreathValue.SelectedIndex = -1;
            this.m_cboBreathValue.SelectedItem = null;
            this.m_cboBreathValue.SelectionStart = 0;
            this.m_cboBreathValue.Size = new System.Drawing.Size(160, 23);
            this.m_cboBreathValue.TabIndex = 900;
            this.m_cboBreathValue.TextBackColor = System.Drawing.Color.White;
            this.m_cboBreathValue.TextForeColor = System.Drawing.Color.Black;
            this.m_cboBreathValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 515;
            this.label2.Text = "������";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboPulseType
            // 
            this.m_cboPulseType.BackColor = System.Drawing.Color.White;
            this.m_cboPulseType.BorderColor = System.Drawing.Color.Black;
            this.m_cboPulseType.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboPulseType.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboPulseType.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboPulseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboPulseType.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulseType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulseType.ForeColor = System.Drawing.Color.Black;
            this.m_cboPulseType.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboPulseType.ListForeColor = System.Drawing.Color.Black;
            this.m_cboPulseType.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboPulseType.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboPulseType.Location = new System.Drawing.Point(11, 108);
            this.m_cboPulseType.m_BlnEnableItemEventMenu = false;
            this.m_cboPulseType.Name = "m_cboPulseType";
            this.m_cboPulseType.SelectedIndex = -1;
            this.m_cboPulseType.SelectedItem = null;
            this.m_cboPulseType.SelectionStart = 0;
            this.m_cboPulseType.Size = new System.Drawing.Size(77, 23);
            this.m_cboPulseType.TabIndex = 514;
            this.m_cboPulseType.TabStop = false;
            this.m_cboPulseType.TextBackColor = System.Drawing.Color.White;
            this.m_cboPulseType.TextForeColor = System.Drawing.Color.Black;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 513;
            this.label4.Text = "��";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboTemperatureType
            // 
            this.m_cboTemperatureType.BackColor = System.Drawing.Color.White;
            this.m_cboTemperatureType.BorderColor = System.Drawing.Color.Black;
            this.m_cboTemperatureType.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboTemperatureType.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboTemperatureType.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboTemperatureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboTemperatureType.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperatureType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperatureType.ForeColor = System.Drawing.Color.Black;
            this.m_cboTemperatureType.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboTemperatureType.ListForeColor = System.Drawing.Color.Black;
            this.m_cboTemperatureType.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboTemperatureType.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboTemperatureType.Location = new System.Drawing.Point(11, 77);
            this.m_cboTemperatureType.m_BlnEnableItemEventMenu = false;
            this.m_cboTemperatureType.Name = "m_cboTemperatureType";
            this.m_cboTemperatureType.SelectedIndex = -1;
            this.m_cboTemperatureType.SelectedItem = null;
            this.m_cboTemperatureType.SelectionStart = 0;
            this.m_cboTemperatureType.Size = new System.Drawing.Size(77, 23);
            this.m_cboTemperatureType.TabIndex = 512;
            this.m_cboTemperatureType.TabStop = false;
            this.m_cboTemperatureType.TextBackColor = System.Drawing.Color.White;
            this.m_cboTemperatureType.TextForeColor = System.Drawing.Color.Black;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 509;
            this.label9.Text = "�¼���";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(179, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 506;
            this.label3.Text = "��/��";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cboTimeFlag
            // 
            this.m_cboTimeFlag.BackColor = System.Drawing.Color.White;
            this.m_cboTimeFlag.BorderColor = System.Drawing.Color.Black;
            this.m_cboTimeFlag.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_cboTimeFlag.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboTimeFlag.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboTimeFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTimeFlag.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTimeFlag.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTimeFlag.ForeColor = System.Drawing.Color.Black;
            this.m_cboTimeFlag.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(229)))), ((int)(((byte)(232)))));
            this.m_cboTimeFlag.ListForeColor = System.Drawing.Color.Black;
            this.m_cboTimeFlag.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboTimeFlag.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboTimeFlag.Location = new System.Drawing.Point(243, 19);
            this.m_cboTimeFlag.m_BlnEnableItemEventMenu = false;
            this.m_cboTimeFlag.Name = "m_cboTimeFlag";
            this.m_cboTimeFlag.SelectedIndex = -1;
            this.m_cboTimeFlag.SelectedItem = null;
            this.m_cboTimeFlag.SelectionStart = 0;
            this.m_cboTimeFlag.Size = new System.Drawing.Size(67, 23);
            this.m_cboTimeFlag.TabIndex = 100;
            this.m_cboTimeFlag.TabStop = false;
            this.m_cboTimeFlag.TextBackColor = System.Drawing.Color.White;
            this.m_cboTimeFlag.TextForeColor = System.Drawing.Color.Black;
            this.m_cboTimeFlag.SelectedIndexChanged += new System.EventHandler(this.m_cboTimeFlag_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Location = new System.Drawing.Point(327, 22);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 10);
            this.panel5.TabIndex = 2301;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(243, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 14);
            this.label13.TabIndex = 517;
            this.label13.Text = "��/��";
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(244, 109);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 27);
            this.checkBox1.TabIndex = 800;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "��Сʱ";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(234, 168);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 14);
            this.label19.TabIndex = 465;
            this.label19.Text = "mmHg";
            this.label19.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(238, 198);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(21, 14);
            this.label21.TabIndex = 1112;
            this.label21.Text = "ml";
            this.label21.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // m_cmdGeneralNurse
            // 
            this.m_cmdGeneralNurse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdGeneralNurse.DefaultScheme = true;
            this.m_cmdGeneralNurse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGeneralNurse.Hint = "";
            this.m_cmdGeneralNurse.Location = new System.Drawing.Point(157, 570);
            this.m_cmdGeneralNurse.Name = "m_cmdGeneralNurse";
            this.m_cmdGeneralNurse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGeneralNurse.Size = new System.Drawing.Size(100, 30);
            this.m_cmdGeneralNurse.TabIndex = 10000005;
            this.m_cmdGeneralNurse.TabStop = false;
            this.m_cmdGeneralNurse.Text = "һ�㻤���¼";
            this.m_cmdGeneralNurse.Click += new System.EventHandler(this.m_cmdGeneralNurse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_grbSkinTest);
            this.groupBox2.Controls.Add(this.m_grbOther);
            this.groupBox2.Location = new System.Drawing.Point(374, 404);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1, 1);
            this.groupBox2.TabIndex = 1305;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "no use";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.SystemColors.Control;
            this.label25.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(353, 104);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(70, 14);
            this.label25.TabIndex = 10000006;
            this.label25.Text = "��Ժ����:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label25.Visible = false;
            // 
            // cboCreateDate
            // 
            this.cboCreateDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCreateDate.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboCreateDate.Location = new System.Drawing.Point(425, 102);
            this.cboCreateDate.Name = "cboCreateDate";
            this.cboCreateDate.Size = new System.Drawing.Size(195, 22);
            this.cboCreateDate.TabIndex = 10000008;
            this.cboCreateDate.Visible = false;
            this.cboCreateDate.SelectedIndexChanged += new System.EventHandler(this.cboCreateDate_SelectedIndexChanged_1);
            // 
            // pnlRecordContain
            // 
            this.pnlRecordContain.AutoScroll = true;
            this.pnlRecordContain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRecordContain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRecordContain.Controls.Add(this.m_ctlRecord);
            this.pnlRecordContain.Controls.Add(this.pnlFocus);
            this.pnlRecordContain.Controls.Add(this.m_txtWhichWeek);
            this.pnlRecordContain.Controls.Add(this.m_cmdNextWeek);
            this.pnlRecordContain.Controls.Add(this.m_cmdFinalWeek);
            this.pnlRecordContain.Controls.Add(this.m_cmdPreWeek);
            this.pnlRecordContain.Controls.Add(this.m_cmdFirstWeek);
            this.pnlRecordContain.Controls.Add(this.m_picZoom);
            this.pnlRecordContain.Controls.Add(this.groupBox1);
            this.pnlRecordContain.Location = new System.Drawing.Point(329, 58);
            this.pnlRecordContain.Name = "pnlRecordContain";
            this.pnlRecordContain.Size = new System.Drawing.Size(644, 550);
            this.pnlRecordContain.TabIndex = 10000009;
            // 
            // m_ctlRecord
            // 
            this.m_ctlRecord.BackColor = System.Drawing.Color.White;
            this.m_ctlRecord.Location = new System.Drawing.Point(3, 23);
            this.m_ctlRecord.m_BlnIsShort = true;
            this.m_ctlRecord.m_ClrBorder = System.Drawing.Color.Black;
            this.m_ctlRecord.m_ClrDateValue = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrDownTemperature = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrDownTemperatureLine = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrDST = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrGridBorder = System.Drawing.Color.Black;
            this.m_ctlRecord.m_ClrLowerEventText = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrPulseLine = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrPulseParamsText = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrPulseSymbol = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrSkinTest = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrSpecialDateText = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrSpecialGridBorder = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrSpecialTimeText = System.Drawing.Color.Red;
            this.m_ctlRecord.m_ClrTemperatureLine = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrTemperatureParamsText = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrTemperatureSymbol = System.Drawing.Color.Blue;
            this.m_ctlRecord.m_ClrTitleText = System.Drawing.Color.Black;
            this.m_ctlRecord.m_ClrUpperEventText = System.Drawing.Color.Red;
            this.m_ctlRecord.m_IntWeekNum = 1;
            this.m_ctlRecord.m_StrUserID = "";
            this.m_ctlRecord.m_StrUserName = "";
            this.m_ctlRecord.Name = "m_ctlRecord";
            this.m_ctlRecord.Size = new System.Drawing.Size(619, 820);
            this.m_ctlRecord.TabIndex = 10000013;
            this.m_ctlRecord.TabStop = false;
            this.m_ctlRecord.m_evtDateRecordMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtDateRecordMouseDown);
            this.m_ctlRecord.m_evtBreathMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtBreathMouseDown);
            this.m_ctlRecord.m_evtIsShortChanged += new System.EventHandler(this.m_ctlRecord_m_evtIsShortChanged);
            this.m_ctlRecord.m_evtSpecialDateMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtSpecialDateMouseDown);
            this.m_ctlRecord.m_evtTemperatureMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtTemperatureMouseDown);
            this.m_ctlRecord.m_evtOtherMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtOtherMouseDown);
            this.m_ctlRecord.m_evtOutStreamMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtOutStreamMouseDown);
            this.m_ctlRecord.m_evtPulseMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtPulseMouseDown);
            this.m_ctlRecord.m_evtEventMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtEventMouseDown);
            this.m_ctlRecord.m_evtWeightMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtWeightMouseDown);
            this.m_ctlRecord.m_evtInputMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtInputMouseDown);
            this.m_ctlRecord.m_evtDejectaMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtDejectaMouseDown);
            this.m_ctlRecord.m_evtPressureMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtPressureMouseDown);
            this.m_ctlRecord.m_evtSkinTestMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtSkinTestMouseDown);
            this.m_ctlRecord.m_evtPeeMouseDown += new System.EventHandler(this.m_ctlRecord_m_evtPeeMouseDown);
            this.m_ctlRecord.m_evtWeekNumChanged += new System.EventHandler(this.m_ctlRecord_m_evtWeekNumChanged);
            this.m_ctlRecord.Click += new System.EventHandler(this.m_ctlRecord_Click);
            // 
            // pnlFocus
            // 
            this.pnlFocus.Location = new System.Drawing.Point(152, 308);
            this.pnlFocus.Name = "pnlFocus";
            this.pnlFocus.Size = new System.Drawing.Size(0, 0);
            this.pnlFocus.TabIndex = 502;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cmdClear);
            this.groupBox1.Controls.Add(this.grbRecordType);
            this.groupBox1.Location = new System.Drawing.Point(50, 566);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(55, 24);
            this.groupBox1.TabIndex = 10000004;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "no use";
            this.groupBox1.Visible = false;
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdClear.ForeColor = System.Drawing.Color.White;
            this.m_cmdClear.Location = new System.Drawing.Point(16, 20);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Size = new System.Drawing.Size(64, 32);
            this.m_cmdClear.TabIndex = 1503;
            this.m_cmdClear.Text = "���";
            this.m_cmdClear.Visible = false;
            // 
            // grbRecordType
            // 
            this.grbRecordType.Controls.Add(this.m_rdbSpecialDateType);
            this.grbRecordType.Controls.Add(this.m_rdbPeeType);
            this.grbRecordType.Controls.Add(this.m_rdbDejectaType);
            this.grbRecordType.Controls.Add(this.m_rdbEventType);
            this.grbRecordType.Controls.Add(this.m_rdbPulseType);
            this.grbRecordType.Controls.Add(this.m_rdbBreathType);
            this.grbRecordType.Controls.Add(this.m_rdbTemperatureType);
            this.grbRecordType.Controls.Add(this.m_rdbInputType);
            this.grbRecordType.Controls.Add(this.m_rdbOtherType);
            this.grbRecordType.Controls.Add(this.m_rdbWeightType);
            this.grbRecordType.Controls.Add(this.m_rdbSkinTestType);
            this.grbRecordType.Controls.Add(this.m_rdbOutStreamType);
            this.grbRecordType.Controls.Add(this.m_rdbDownTemperatureType);
            this.grbRecordType.Controls.Add(this.m_rdbPressureType);
            this.grbRecordType.Controls.Add(this.m_grbSpecialDate);
            this.grbRecordType.Controls.Add(this.m_grbTemperature);
            this.grbRecordType.Controls.Add(this.m_grbOutStream);
            this.grbRecordType.Controls.Add(this.m_grbInput);
            this.grbRecordType.Controls.Add(this.m_grbDejecta);
            this.grbRecordType.Controls.Add(this.m_grbEvent);
            this.grbRecordType.Controls.Add(this.m_grbDownTemperature);
            this.grbRecordType.Controls.Add(this.m_grbPulse);
            this.grbRecordType.Controls.Add(this.m_grbPressure);
            this.grbRecordType.Controls.Add(this.m_grbPee);
            this.grbRecordType.Controls.Add(this.m_grbBreath);
            this.grbRecordType.Controls.Add(this.m_grbWeight);
            this.grbRecordType.ForeColor = System.Drawing.Color.White;
            this.grbRecordType.Location = new System.Drawing.Point(76, 28);
            this.grbRecordType.Name = "grbRecordType";
            this.grbRecordType.Size = new System.Drawing.Size(56, 20);
            this.grbRecordType.TabIndex = 10;
            this.grbRecordType.TabStop = false;
            this.grbRecordType.Text = "��¼����";
            this.grbRecordType.Visible = false;
            // 
            // m_rdbSpecialDateType
            // 
            this.m_rdbSpecialDateType.Checked = true;
            this.m_rdbSpecialDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbSpecialDateType.Location = new System.Drawing.Point(8, 24);
            this.m_rdbSpecialDateType.Name = "m_rdbSpecialDateType";
            this.m_rdbSpecialDateType.Size = new System.Drawing.Size(92, 24);
            this.m_rdbSpecialDateType.TabIndex = 21;
            this.m_rdbSpecialDateType.TabStop = true;
            this.m_rdbSpecialDateType.Tag = "";
            this.m_rdbSpecialDateType.Text = "��������";
            // 
            // m_rdbPeeType
            // 
            this.m_rdbPeeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbPeeType.Location = new System.Drawing.Point(96, 88);
            this.m_rdbPeeType.Name = "m_rdbPeeType";
            this.m_rdbPeeType.Size = new System.Drawing.Size(60, 24);
            this.m_rdbPeeType.TabIndex = 28;
            this.m_rdbPeeType.Text = "����";
            // 
            // m_rdbDejectaType
            // 
            this.m_rdbDejectaType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbDejectaType.Location = new System.Drawing.Point(160, 56);
            this.m_rdbDejectaType.Name = "m_rdbDejectaType";
            this.m_rdbDejectaType.Size = new System.Drawing.Size(60, 24);
            this.m_rdbDejectaType.TabIndex = 26;
            this.m_rdbDejectaType.Text = "���";
            // 
            // m_rdbEventType
            // 
            this.m_rdbEventType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbEventType.Location = new System.Drawing.Point(96, 24);
            this.m_rdbEventType.Name = "m_rdbEventType";
            this.m_rdbEventType.Size = new System.Drawing.Size(60, 24);
            this.m_rdbEventType.TabIndex = 22;
            this.m_rdbEventType.Text = "�¼�";
            // 
            // m_rdbPulseType
            // 
            this.m_rdbPulseType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbPulseType.Location = new System.Drawing.Point(160, 24);
            this.m_rdbPulseType.Name = "m_rdbPulseType";
            this.m_rdbPulseType.Size = new System.Drawing.Size(76, 24);
            this.m_rdbPulseType.TabIndex = 23;
            this.m_rdbPulseType.Text = "����";
            // 
            // m_rdbBreathType
            // 
            this.m_rdbBreathType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbBreathType.Location = new System.Drawing.Point(8, 56);
            this.m_rdbBreathType.Name = "m_rdbBreathType";
            this.m_rdbBreathType.Size = new System.Drawing.Size(76, 24);
            this.m_rdbBreathType.TabIndex = 24;
            this.m_rdbBreathType.Text = "����";
            // 
            // m_rdbTemperatureType
            // 
            this.m_rdbTemperatureType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbTemperatureType.Location = new System.Drawing.Point(96, 56);
            this.m_rdbTemperatureType.Name = "m_rdbTemperatureType";
            this.m_rdbTemperatureType.Size = new System.Drawing.Size(60, 24);
            this.m_rdbTemperatureType.TabIndex = 25;
            this.m_rdbTemperatureType.Text = "����";
            // 
            // m_rdbInputType
            // 
            this.m_rdbInputType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbInputType.Location = new System.Drawing.Point(8, 88);
            this.m_rdbInputType.Name = "m_rdbInputType";
            this.m_rdbInputType.Size = new System.Drawing.Size(92, 24);
            this.m_rdbInputType.TabIndex = 27;
            this.m_rdbInputType.Text = "����Һ��";
            // 
            // m_rdbOtherType
            // 
            this.m_rdbOtherType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbOtherType.Location = new System.Drawing.Point(96, 156);
            this.m_rdbOtherType.Name = "m_rdbOtherType";
            this.m_rdbOtherType.Size = new System.Drawing.Size(56, 24);
            this.m_rdbOtherType.TabIndex = 34;
            this.m_rdbOtherType.Text = "����";
            // 
            // m_rdbWeightType
            // 
            this.m_rdbWeightType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbWeightType.Location = new System.Drawing.Point(160, 124);
            this.m_rdbWeightType.Name = "m_rdbWeightType";
            this.m_rdbWeightType.Size = new System.Drawing.Size(76, 24);
            this.m_rdbWeightType.TabIndex = 32;
            this.m_rdbWeightType.Text = "����";
            // 
            // m_rdbSkinTestType
            // 
            this.m_rdbSkinTestType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbSkinTestType.Location = new System.Drawing.Point(8, 156);
            this.m_rdbSkinTestType.Name = "m_rdbSkinTestType";
            this.m_rdbSkinTestType.Size = new System.Drawing.Size(76, 24);
            this.m_rdbSkinTestType.TabIndex = 33;
            this.m_rdbSkinTestType.Text = "Ƥ��";
            // 
            // m_rdbOutStreamType
            // 
            this.m_rdbOutStreamType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbOutStreamType.Location = new System.Drawing.Point(8, 124);
            this.m_rdbOutStreamType.Name = "m_rdbOutStreamType";
            this.m_rdbOutStreamType.Size = new System.Drawing.Size(76, 24);
            this.m_rdbOutStreamType.TabIndex = 30;
            this.m_rdbOutStreamType.Text = "������";
            // 
            // m_rdbDownTemperatureType
            // 
            this.m_rdbDownTemperatureType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbDownTemperatureType.Location = new System.Drawing.Point(160, 88);
            this.m_rdbDownTemperatureType.Name = "m_rdbDownTemperatureType";
            this.m_rdbDownTemperatureType.Size = new System.Drawing.Size(140, 24);
            this.m_rdbDownTemperatureType.TabIndex = 29;
            this.m_rdbDownTemperatureType.Text = "�����º�����";
            // 
            // m_rdbPressureType
            // 
            this.m_rdbPressureType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbPressureType.Location = new System.Drawing.Point(96, 124);
            this.m_rdbPressureType.Name = "m_rdbPressureType";
            this.m_rdbPressureType.Size = new System.Drawing.Size(56, 24);
            this.m_rdbPressureType.TabIndex = 31;
            this.m_rdbPressureType.Text = "Ѫѹ";
            // 
            // m_grbSpecialDate
            // 
            this.m_grbSpecialDate.Controls.Add(this.m_chkNewSpecialDate);
            this.m_grbSpecialDate.ForeColor = System.Drawing.Color.White;
            this.m_grbSpecialDate.Location = new System.Drawing.Point(-204, 4);
            this.m_grbSpecialDate.Name = "m_grbSpecialDate";
            this.m_grbSpecialDate.Size = new System.Drawing.Size(260, 84);
            this.m_grbSpecialDate.TabIndex = 100;
            this.m_grbSpecialDate.TabStop = false;
            this.m_grbSpecialDate.Text = "�������������";
            // 
            // m_chkNewSpecialDate
            // 
            this.m_chkNewSpecialDate.Checked = true;
            this.m_chkNewSpecialDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkNewSpecialDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkNewSpecialDate.Location = new System.Drawing.Point(28, 40);
            this.m_chkNewSpecialDate.Name = "m_chkNewSpecialDate";
            this.m_chkNewSpecialDate.Size = new System.Drawing.Size(104, 24);
            this.m_chkNewSpecialDate.TabIndex = 101;
            this.m_chkNewSpecialDate.TabStop = false;
            this.m_chkNewSpecialDate.Text = "��������";
            // 
            // m_grbTemperature
            // 
            this.m_grbTemperature.Controls.Add(this.panel4);
            this.m_grbTemperature.Controls.Add(this.m_cboTemperatureTimeFlag);
            this.m_grbTemperature.Controls.Add(this.lblTemperatureUnit);
            this.m_grbTemperature.Controls.Add(this.m_chkTemperatureNotLineToPre);
            this.m_grbTemperature.Controls.Add(this.m_txtTemperatureValue);
            this.m_grbTemperature.ForeColor = System.Drawing.Color.White;
            this.m_grbTemperature.Location = new System.Drawing.Point(-204, -624);
            this.m_grbTemperature.Name = "m_grbTemperature";
            this.m_grbTemperature.Size = new System.Drawing.Size(260, 285);
            this.m_grbTemperature.TabIndex = 500;
            this.m_grbTemperature.TabStop = false;
            this.m_grbTemperature.Text = "����";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_rdbAnusTemperature);
            this.panel4.Controls.Add(this.m_rdbMouthTemperature);
            this.panel4.Controls.Add(this.m_rdbArmpitTemperature);
            this.panel4.Location = new System.Drawing.Point(12, 98);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(32, 10);
            this.panel4.TabIndex = 508;
            // 
            // m_rdbAnusTemperature
            // 
            this.m_rdbAnusTemperature.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbAnusTemperature.Location = new System.Drawing.Point(164, 4);
            this.m_rdbAnusTemperature.Name = "m_rdbAnusTemperature";
            this.m_rdbAnusTemperature.Size = new System.Drawing.Size(60, 24);
            this.m_rdbAnusTemperature.TabIndex = 505;
            this.m_rdbAnusTemperature.Text = "�ر�";
            // 
            // m_rdbMouthTemperature
            // 
            this.m_rdbMouthTemperature.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbMouthTemperature.Location = new System.Drawing.Point(88, 4);
            this.m_rdbMouthTemperature.Name = "m_rdbMouthTemperature";
            this.m_rdbMouthTemperature.Size = new System.Drawing.Size(60, 24);
            this.m_rdbMouthTemperature.TabIndex = 504;
            this.m_rdbMouthTemperature.Text = "�ڱ�";
            // 
            // m_rdbArmpitTemperature
            // 
            this.m_rdbArmpitTemperature.Checked = true;
            this.m_rdbArmpitTemperature.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbArmpitTemperature.Location = new System.Drawing.Point(12, 4);
            this.m_rdbArmpitTemperature.Name = "m_rdbArmpitTemperature";
            this.m_rdbArmpitTemperature.Size = new System.Drawing.Size(56, 24);
            this.m_rdbArmpitTemperature.TabIndex = 503;
            this.m_rdbArmpitTemperature.TabStop = true;
            this.m_rdbArmpitTemperature.Text = "Ҹ��";
            // 
            // m_cboTemperatureTimeFlag
            // 
            this.m_cboTemperatureTimeFlag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboTemperatureTimeFlag.BorderColor = System.Drawing.Color.White;
            this.m_cboTemperatureTimeFlag.DropButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboTemperatureTimeFlag.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboTemperatureTimeFlag.DropButtonForeColor = System.Drawing.Color.White;
            this.m_cboTemperatureTimeFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTemperatureTimeFlag.flatFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperatureTimeFlag.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboTemperatureTimeFlag.ForeColor = System.Drawing.Color.White;
            this.m_cboTemperatureTimeFlag.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboTemperatureTimeFlag.ListForeColor = System.Drawing.Color.White;
            this.m_cboTemperatureTimeFlag.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboTemperatureTimeFlag.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboTemperatureTimeFlag.Location = new System.Drawing.Point(24, 40);
            this.m_cboTemperatureTimeFlag.m_BlnEnableItemEventMenu = true;
            this.m_cboTemperatureTimeFlag.Name = "m_cboTemperatureTimeFlag";
            this.m_cboTemperatureTimeFlag.SelectedIndex = -1;
            this.m_cboTemperatureTimeFlag.SelectedItem = null;
            this.m_cboTemperatureTimeFlag.SelectionStart = 0;
            this.m_cboTemperatureTimeFlag.Size = new System.Drawing.Size(84, 26);
            this.m_cboTemperatureTimeFlag.TabIndex = 501;
            this.m_cboTemperatureTimeFlag.TabStop = false;
            this.m_cboTemperatureTimeFlag.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboTemperatureTimeFlag.TextForeColor = System.Drawing.Color.White;
            // 
            // lblTemperatureUnit
            // 
            this.lblTemperatureUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemperatureUnit.ForeColor = System.Drawing.Color.White;
            this.lblTemperatureUnit.Location = new System.Drawing.Point(140, 128);
            this.lblTemperatureUnit.Name = "lblTemperatureUnit";
            this.lblTemperatureUnit.Size = new System.Drawing.Size(88, 20);
            this.lblTemperatureUnit.TabIndex = 465;
            this.lblTemperatureUnit.Text = "�棨���ϣ�";
            // 
            // m_chkTemperatureNotLineToPre
            // 
            this.m_chkTemperatureNotLineToPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkTemperatureNotLineToPre.Location = new System.Drawing.Point(24, 160);
            this.m_chkTemperatureNotLineToPre.Name = "m_chkTemperatureNotLineToPre";
            this.m_chkTemperatureNotLineToPre.Size = new System.Drawing.Size(220, 44);
            this.m_chkTemperatureNotLineToPre.TabIndex = 507;
            this.m_chkTemperatureNotLineToPre.TabStop = false;
            this.m_chkTemperatureNotLineToPre.Text = "������ٷ��أ�����ǰһ�μ�¼����";
            // 
            // m_txtTemperatureValue
            // 
            this.m_txtTemperatureValue.AccessibleName = "save";
            this.m_txtTemperatureValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtTemperatureValue.BorderColor = System.Drawing.Color.White;
            this.m_txtTemperatureValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtTemperatureValue.ForeColor = System.Drawing.Color.White;
            this.m_txtTemperatureValue.Location = new System.Drawing.Point(24, 124);
            this.m_txtTemperatureValue.Name = "m_txtTemperatureValue";
            this.m_txtTemperatureValue.Size = new System.Drawing.Size(112, 23);
            this.m_txtTemperatureValue.TabIndex = 506;
            // 
            // m_grbOutStream
            // 
            this.m_grbOutStream.ForeColor = System.Drawing.Color.White;
            this.m_grbOutStream.Location = new System.Drawing.Point(-204, -624);
            this.m_grbOutStream.Name = "m_grbOutStream";
            this.m_grbOutStream.Size = new System.Drawing.Size(260, 137);
            this.m_grbOutStream.TabIndex = 1000;
            this.m_grbOutStream.TabStop = false;
            this.m_grbOutStream.Text = "������";
            // 
            // m_grbInput
            // 
            this.m_grbInput.ForeColor = System.Drawing.Color.White;
            this.m_grbInput.Location = new System.Drawing.Point(-204, -624);
            this.m_grbInput.Name = "m_grbInput";
            this.m_grbInput.Size = new System.Drawing.Size(260, 137);
            this.m_grbInput.TabIndex = 700;
            this.m_grbInput.TabStop = false;
            this.m_grbInput.Text = "����Һ��";
            // 
            // m_grbDejecta
            // 
            this.m_grbDejecta.Controls.Add(this.m_nmuDejectaBeforeTimes);
            this.m_grbDejecta.Controls.Add(this.m_chkCannotDejecta);
            this.m_grbDejecta.Controls.Add(this.lblDejectaBeforeTimes);
            this.m_grbDejecta.Controls.Add(this.lblDejectaClysisTimes);
            this.m_grbDejecta.Controls.Add(this.m_nmuDejectaClysisTimes);
            this.m_grbDejecta.Controls.Add(this.lblDejectaAfterTimes);
            this.m_grbDejecta.Controls.Add(this.m_nmuDejectaAfterTimes);
            this.m_grbDejecta.Controls.Add(this.m_chkDejectaAfterMoreTimes);
            this.m_grbDejecta.ForeColor = System.Drawing.Color.White;
            this.m_grbDejecta.Location = new System.Drawing.Point(-204, -624);
            this.m_grbDejecta.Name = "m_grbDejecta";
            this.m_grbDejecta.Size = new System.Drawing.Size(260, 285);
            this.m_grbDejecta.TabIndex = 800;
            this.m_grbDejecta.TabStop = false;
            this.m_grbDejecta.Text = "���";
            this.m_grbDejecta.Visible = false;
            // 
            // m_nmuDejectaBeforeTimes
            // 
            this.m_nmuDejectaBeforeTimes.AccessibleName = "save";
            this.m_nmuDejectaBeforeTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_nmuDejectaBeforeTimes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_nmuDejectaBeforeTimes.ForeColor = System.Drawing.Color.White;
            this.m_nmuDejectaBeforeTimes.Location = new System.Drawing.Point(96, 56);
            this.m_nmuDejectaBeforeTimes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_nmuDejectaBeforeTimes.Name = "m_nmuDejectaBeforeTimes";
            this.m_nmuDejectaBeforeTimes.Size = new System.Drawing.Size(44, 19);
            this.m_nmuDejectaBeforeTimes.TabIndex = 802;
            this.m_nmuDejectaBeforeTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_chkCannotDejecta
            // 
            this.m_chkCannotDejecta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkCannotDejecta.Location = new System.Drawing.Point(20, 24);
            this.m_chkCannotDejecta.Name = "m_chkCannotDejecta";
            this.m_chkCannotDejecta.Size = new System.Drawing.Size(140, 28);
            this.m_chkCannotDejecta.TabIndex = 801;
            this.m_chkCannotDejecta.TabStop = false;
            this.m_chkCannotDejecta.Text = "���ʧ����ٸ�";
            // 
            // lblDejectaBeforeTimes
            // 
            this.lblDejectaBeforeTimes.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDejectaBeforeTimes.ForeColor = System.Drawing.Color.White;
            this.lblDejectaBeforeTimes.Location = new System.Drawing.Point(20, 56);
            this.lblDejectaBeforeTimes.Name = "lblDejectaBeforeTimes";
            this.lblDejectaBeforeTimes.Size = new System.Drawing.Size(72, 20);
            this.lblDejectaBeforeTimes.TabIndex = 465;
            this.lblDejectaBeforeTimes.Text = "������";
            // 
            // lblDejectaClysisTimes
            // 
            this.lblDejectaClysisTimes.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDejectaClysisTimes.ForeColor = System.Drawing.Color.White;
            this.lblDejectaClysisTimes.Location = new System.Drawing.Point(20, 84);
            this.lblDejectaClysisTimes.Name = "lblDejectaClysisTimes";
            this.lblDejectaClysisTimes.Size = new System.Drawing.Size(72, 20);
            this.lblDejectaClysisTimes.TabIndex = 465;
            this.lblDejectaClysisTimes.Text = "�೦����";
            // 
            // m_nmuDejectaClysisTimes
            // 
            this.m_nmuDejectaClysisTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_nmuDejectaClysisTimes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_nmuDejectaClysisTimes.ForeColor = System.Drawing.Color.White;
            this.m_nmuDejectaClysisTimes.Location = new System.Drawing.Point(96, 84);
            this.m_nmuDejectaClysisTimes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_nmuDejectaClysisTimes.Name = "m_nmuDejectaClysisTimes";
            this.m_nmuDejectaClysisTimes.Size = new System.Drawing.Size(44, 19);
            this.m_nmuDejectaClysisTimes.TabIndex = 803;
            this.m_nmuDejectaClysisTimes.TabStop = false;
            // 
            // lblDejectaAfterTimes
            // 
            this.lblDejectaAfterTimes.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDejectaAfterTimes.ForeColor = System.Drawing.Color.White;
            this.lblDejectaAfterTimes.Location = new System.Drawing.Point(20, 140);
            this.lblDejectaAfterTimes.Name = "lblDejectaAfterTimes";
            this.lblDejectaAfterTimes.Size = new System.Drawing.Size(124, 20);
            this.lblDejectaAfterTimes.TabIndex = 465;
            this.lblDejectaAfterTimes.Text = "�೦�������";
            // 
            // m_nmuDejectaAfterTimes
            // 
            this.m_nmuDejectaAfterTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_nmuDejectaAfterTimes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_nmuDejectaAfterTimes.ForeColor = System.Drawing.Color.White;
            this.m_nmuDejectaAfterTimes.Location = new System.Drawing.Point(148, 140);
            this.m_nmuDejectaAfterTimes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_nmuDejectaAfterTimes.Name = "m_nmuDejectaAfterTimes";
            this.m_nmuDejectaAfterTimes.Size = new System.Drawing.Size(44, 19);
            this.m_nmuDejectaAfterTimes.TabIndex = 805;
            this.m_nmuDejectaAfterTimes.TabStop = false;
            // 
            // m_chkDejectaAfterMoreTimes
            // 
            this.m_chkDejectaAfterMoreTimes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkDejectaAfterMoreTimes.Location = new System.Drawing.Point(20, 108);
            this.m_chkDejectaAfterMoreTimes.Name = "m_chkDejectaAfterMoreTimes";
            this.m_chkDejectaAfterMoreTimes.Size = new System.Drawing.Size(140, 28);
            this.m_chkDejectaAfterMoreTimes.TabIndex = 804;
            this.m_chkDejectaAfterMoreTimes.TabStop = false;
            this.m_chkDejectaAfterMoreTimes.Text = "�೦������";
            // 
            // m_grbEvent
            // 
            this.m_grbEvent.Controls.Add(this.lblEventType);
            this.m_grbEvent.Controls.Add(this.m_dtpEventTime_);
            this.m_grbEvent.Controls.Add(this.lblEventTime);
            this.m_grbEvent.ForeColor = System.Drawing.Color.White;
            this.m_grbEvent.Location = new System.Drawing.Point(-204, 4);
            this.m_grbEvent.Name = "m_grbEvent";
            this.m_grbEvent.Size = new System.Drawing.Size(260, 144);
            this.m_grbEvent.TabIndex = 200;
            this.m_grbEvent.TabStop = false;
            this.m_grbEvent.Text = "�����¼�";
            // 
            // lblEventType
            // 
            this.lblEventType.ForeColor = System.Drawing.Color.White;
            this.lblEventType.Location = new System.Drawing.Point(16, 60);
            this.lblEventType.Name = "lblEventType";
            this.lblEventType.Size = new System.Drawing.Size(80, 20);
            this.lblEventType.TabIndex = 471;
            this.lblEventType.Text = "�¼�����";
            // 
            // m_dtpEventTime_
            // 
            this.m_dtpEventTime_.BorderColor = System.Drawing.Color.White;
            this.m_dtpEventTime_.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpEventTime_.DropButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_dtpEventTime_.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpEventTime_.DropButtonForeColor = System.Drawing.Color.White;
            this.m_dtpEventTime_.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpEventTime_.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpEventTime_.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpEventTime_.Location = new System.Drawing.Point(16, 20);
            this.m_dtpEventTime_.m_BlnOnlyTime = false;
            this.m_dtpEventTime_.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpEventTime_.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpEventTime_.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpEventTime_.Name = "m_dtpEventTime_";
            this.m_dtpEventTime_.ReadOnly = false;
            this.m_dtpEventTime_.Size = new System.Drawing.Size(212, 22);
            this.m_dtpEventTime_.TabIndex = 100;
            this.m_dtpEventTime_.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_dtpEventTime_.TextForeColor = System.Drawing.Color.White;
            this.m_dtpEventTime_.Visible = false;
            // 
            // lblEventTime
            // 
            this.lblEventTime.ForeColor = System.Drawing.Color.White;
            this.lblEventTime.Location = new System.Drawing.Point(16, 100);
            this.lblEventTime.Name = "lblEventTime";
            this.lblEventTime.Size = new System.Drawing.Size(76, 20);
            this.lblEventTime.TabIndex = 476;
            this.lblEventTime.Text = "�¼�ʱ��";
            // 
            // m_grbDownTemperature
            // 
            this.m_grbDownTemperature.Controls.Add(this.m_cboDownBaseTime);
            this.m_grbDownTemperature.Controls.Add(this.m_chkDownBaseTimeIsHalf);
            this.m_grbDownTemperature.Controls.Add(this.m_lblDownTemperatureUnit);
            this.m_grbDownTemperature.Controls.Add(this.lblBaseTime);
            this.m_grbDownTemperature.Controls.Add(this.m_txtDownTemperatureValue);
            this.m_grbDownTemperature.ForeColor = System.Drawing.Color.White;
            this.m_grbDownTemperature.Location = new System.Drawing.Point(-204, 4);
            this.m_grbDownTemperature.Name = "m_grbDownTemperature";
            this.m_grbDownTemperature.Size = new System.Drawing.Size(260, 124);
            this.m_grbDownTemperature.TabIndex = 600;
            this.m_grbDownTemperature.TabStop = false;
            this.m_grbDownTemperature.Text = "�����º�����";
            // 
            // m_cboDownBaseTime
            // 
            this.m_cboDownBaseTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboDownBaseTime.BorderColor = System.Drawing.Color.White;
            this.m_cboDownBaseTime.DropButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboDownBaseTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboDownBaseTime.DropButtonForeColor = System.Drawing.Color.White;
            this.m_cboDownBaseTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDownBaseTime.flatFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDownBaseTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDownBaseTime.ForeColor = System.Drawing.Color.White;
            this.m_cboDownBaseTime.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboDownBaseTime.ListForeColor = System.Drawing.Color.White;
            this.m_cboDownBaseTime.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboDownBaseTime.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboDownBaseTime.Location = new System.Drawing.Point(16, 53);
            this.m_cboDownBaseTime.m_BlnEnableItemEventMenu = true;
            this.m_cboDownBaseTime.Name = "m_cboDownBaseTime";
            this.m_cboDownBaseTime.SelectedIndex = -1;
            this.m_cboDownBaseTime.SelectedItem = null;
            this.m_cboDownBaseTime.SelectionStart = 0;
            this.m_cboDownBaseTime.Size = new System.Drawing.Size(82, 26);
            this.m_cboDownBaseTime.TabIndex = 601;
            this.m_cboDownBaseTime.TabStop = false;
            this.m_cboDownBaseTime.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboDownBaseTime.TextForeColor = System.Drawing.Color.White;
            // 
            // m_chkDownBaseTimeIsHalf
            // 
            this.m_chkDownBaseTimeIsHalf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkDownBaseTimeIsHalf.Location = new System.Drawing.Point(107, 52);
            this.m_chkDownBaseTimeIsHalf.Name = "m_chkDownBaseTimeIsHalf";
            this.m_chkDownBaseTimeIsHalf.Size = new System.Drawing.Size(113, 28);
            this.m_chkDownBaseTimeIsHalf.TabIndex = 602;
            this.m_chkDownBaseTimeIsHalf.TabStop = false;
            this.m_chkDownBaseTimeIsHalf.Text = "ʹ�ð�Сʱ";
            // 
            // m_lblDownTemperatureUnit
            // 
            this.m_lblDownTemperatureUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblDownTemperatureUnit.ForeColor = System.Drawing.Color.White;
            this.m_lblDownTemperatureUnit.Location = new System.Drawing.Point(124, 87);
            this.m_lblDownTemperatureUnit.Name = "m_lblDownTemperatureUnit";
            this.m_lblDownTemperatureUnit.Size = new System.Drawing.Size(92, 20);
            this.m_lblDownTemperatureUnit.TabIndex = 465;
            this.m_lblDownTemperatureUnit.Text = "�棨���ϣ�";
            // 
            // lblBaseTime
            // 
            this.lblBaseTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBaseTime.ForeColor = System.Drawing.Color.White;
            this.lblBaseTime.Location = new System.Drawing.Point(16, 24);
            this.lblBaseTime.Name = "lblBaseTime";
            this.lblBaseTime.Size = new System.Drawing.Size(95, 20);
            this.lblBaseTime.TabIndex = 465;
            this.lblBaseTime.Text = "����ǰʱ��";
            // 
            // m_txtDownTemperatureValue
            // 
            this.m_txtDownTemperatureValue.AccessibleName = "save";
            this.m_txtDownTemperatureValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtDownTemperatureValue.BorderColor = System.Drawing.Color.White;
            this.m_txtDownTemperatureValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtDownTemperatureValue.ForeColor = System.Drawing.Color.White;
            this.m_txtDownTemperatureValue.Location = new System.Drawing.Point(12, 84);
            this.m_txtDownTemperatureValue.Name = "m_txtDownTemperatureValue";
            this.m_txtDownTemperatureValue.Size = new System.Drawing.Size(112, 23);
            this.m_txtDownTemperatureValue.TabIndex = 603;
            // 
            // m_grbPulse
            // 
            this.m_grbPulse.Controls.Add(this.panel2);
            this.m_grbPulse.Controls.Add(this.m_cboPulseTimeFlag);
            this.m_grbPulse.Controls.Add(this.lblPulseUnit);
            this.m_grbPulse.Controls.Add(this.m_chkPulseNotLineToPre);
            this.m_grbPulse.Controls.Add(this.m_txtPulseValue);
            this.m_grbPulse.ForeColor = System.Drawing.Color.White;
            this.m_grbPulse.Location = new System.Drawing.Point(-204, 4);
            this.m_grbPulse.Name = "m_grbPulse";
            this.m_grbPulse.Size = new System.Drawing.Size(260, 232);
            this.m_grbPulse.TabIndex = 300;
            this.m_grbPulse.TabStop = false;
            this.m_grbPulse.Text = "���������ʣ�";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_rdbPulse);
            this.panel2.Controls.Add(this.m_rdbHeartRate);
            this.panel2.Location = new System.Drawing.Point(16, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 40);
            this.panel2.TabIndex = 466;
            // 
            // m_rdbPulse
            // 
            this.m_rdbPulse.Checked = true;
            this.m_rdbPulse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbPulse.Location = new System.Drawing.Point(12, 8);
            this.m_rdbPulse.Name = "m_rdbPulse";
            this.m_rdbPulse.Size = new System.Drawing.Size(60, 24);
            this.m_rdbPulse.TabIndex = 303;
            this.m_rdbPulse.TabStop = true;
            this.m_rdbPulse.Text = "����";
            // 
            // m_rdbHeartRate
            // 
            this.m_rdbHeartRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbHeartRate.Location = new System.Drawing.Point(104, 8);
            this.m_rdbHeartRate.Name = "m_rdbHeartRate";
            this.m_rdbHeartRate.Size = new System.Drawing.Size(60, 24);
            this.m_rdbHeartRate.TabIndex = 304;
            this.m_rdbHeartRate.Text = "����";
            // 
            // m_cboPulseTimeFlag
            // 
            this.m_cboPulseTimeFlag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboPulseTimeFlag.BorderColor = System.Drawing.Color.White;
            this.m_cboPulseTimeFlag.DropButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboPulseTimeFlag.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboPulseTimeFlag.DropButtonForeColor = System.Drawing.Color.White;
            this.m_cboPulseTimeFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPulseTimeFlag.flatFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulseTimeFlag.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboPulseTimeFlag.ForeColor = System.Drawing.Color.White;
            this.m_cboPulseTimeFlag.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboPulseTimeFlag.ListForeColor = System.Drawing.Color.White;
            this.m_cboPulseTimeFlag.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboPulseTimeFlag.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboPulseTimeFlag.Location = new System.Drawing.Point(28, 40);
            this.m_cboPulseTimeFlag.m_BlnEnableItemEventMenu = true;
            this.m_cboPulseTimeFlag.Name = "m_cboPulseTimeFlag";
            this.m_cboPulseTimeFlag.SelectedIndex = -1;
            this.m_cboPulseTimeFlag.SelectedItem = null;
            this.m_cboPulseTimeFlag.SelectionStart = 0;
            this.m_cboPulseTimeFlag.Size = new System.Drawing.Size(80, 26);
            this.m_cboPulseTimeFlag.TabIndex = 301;
            this.m_cboPulseTimeFlag.TabStop = false;
            this.m_cboPulseTimeFlag.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboPulseTimeFlag.TextForeColor = System.Drawing.Color.White;
            // 
            // lblPulseUnit
            // 
            this.lblPulseUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPulseUnit.ForeColor = System.Drawing.Color.White;
            this.lblPulseUnit.Location = new System.Drawing.Point(92, 132);
            this.lblPulseUnit.Name = "lblPulseUnit";
            this.lblPulseUnit.Size = new System.Drawing.Size(64, 20);
            this.lblPulseUnit.TabIndex = 465;
            this.lblPulseUnit.Text = "(��/��)";
            // 
            // m_chkPulseNotLineToPre
            // 
            this.m_chkPulseNotLineToPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkPulseNotLineToPre.Location = new System.Drawing.Point(24, 176);
            this.m_chkPulseNotLineToPre.Name = "m_chkPulseNotLineToPre";
            this.m_chkPulseNotLineToPre.Size = new System.Drawing.Size(220, 44);
            this.m_chkPulseNotLineToPre.TabIndex = 306;
            this.m_chkPulseNotLineToPre.TabStop = false;
            this.m_chkPulseNotLineToPre.Text = "������ٷ��أ�����ǰһ�μ�¼����";
            // 
            // m_txtPulseValue
            // 
            this.m_txtPulseValue.AccessibleName = "save";
            this.m_txtPulseValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtPulseValue.BorderColor = System.Drawing.Color.White;
            this.m_txtPulseValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtPulseValue.ForeColor = System.Drawing.Color.White;
            this.m_txtPulseValue.Location = new System.Drawing.Point(28, 130);
            this.m_txtPulseValue.Name = "m_txtPulseValue";
            this.m_txtPulseValue.Size = new System.Drawing.Size(60, 23);
            this.m_txtPulseValue.TabIndex = 305;
            // 
            // m_grbPressure
            // 
            this.m_grbPressure.Controls.Add(this.lblPressureDiastolicUnit);
            this.m_grbPressure.Controls.Add(this.lblPressureDiastolic);
            this.m_grbPressure.Controls.Add(this.lblPressureSystolic);
            this.m_grbPressure.ForeColor = System.Drawing.Color.White;
            this.m_grbPressure.Location = new System.Drawing.Point(-204, 4);
            this.m_grbPressure.Name = "m_grbPressure";
            this.m_grbPressure.Size = new System.Drawing.Size(260, 144);
            this.m_grbPressure.TabIndex = 1100;
            this.m_grbPressure.TabStop = false;
            this.m_grbPressure.Text = "Ѫѹ";
            // 
            // lblPressureDiastolicUnit
            // 
            this.lblPressureDiastolicUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPressureDiastolicUnit.ForeColor = System.Drawing.Color.White;
            this.lblPressureDiastolicUnit.Location = new System.Drawing.Point(148, 112);
            this.lblPressureDiastolicUnit.Name = "lblPressureDiastolicUnit";
            this.lblPressureDiastolicUnit.Size = new System.Drawing.Size(40, 20);
            this.lblPressureDiastolicUnit.TabIndex = 465;
            this.lblPressureDiastolicUnit.Text = "mmHg";
            // 
            // lblPressureDiastolic
            // 
            this.lblPressureDiastolic.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPressureDiastolic.ForeColor = System.Drawing.Color.White;
            this.lblPressureDiastolic.Location = new System.Drawing.Point(28, 88);
            this.lblPressureDiastolic.Name = "lblPressureDiastolic";
            this.lblPressureDiastolic.Size = new System.Drawing.Size(64, 20);
            this.lblPressureDiastolic.TabIndex = 465;
            this.lblPressureDiastolic.Text = "����ѹ";
            // 
            // lblPressureSystolic
            // 
            this.lblPressureSystolic.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPressureSystolic.ForeColor = System.Drawing.Color.White;
            this.lblPressureSystolic.Location = new System.Drawing.Point(28, 36);
            this.lblPressureSystolic.Name = "lblPressureSystolic";
            this.lblPressureSystolic.Size = new System.Drawing.Size(64, 20);
            this.lblPressureSystolic.TabIndex = 465;
            this.lblPressureSystolic.Text = "����ѹ";
            // 
            // m_grbPee
            // 
            this.m_grbPee.Controls.Add(this.m_chkIsIrretention);
            this.m_grbPee.Controls.Add(this.m_txtPeeValue);
            this.m_grbPee.ForeColor = System.Drawing.Color.White;
            this.m_grbPee.Location = new System.Drawing.Point(-204, 4);
            this.m_grbPee.Name = "m_grbPee";
            this.m_grbPee.Size = new System.Drawing.Size(260, 124);
            this.m_grbPee.TabIndex = 900;
            this.m_grbPee.TabStop = false;
            this.m_grbPee.Text = "����";
            // 
            // m_chkIsIrretention
            // 
            this.m_chkIsIrretention.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkIsIrretention.Location = new System.Drawing.Point(28, 32);
            this.m_chkIsIrretention.Name = "m_chkIsIrretention";
            this.m_chkIsIrretention.Size = new System.Drawing.Size(104, 28);
            this.m_chkIsIrretention.TabIndex = 901;
            this.m_chkIsIrretention.TabStop = false;
            this.m_chkIsIrretention.Text = "С��ʧ��";
            // 
            // m_txtPeeValue
            // 
            this.m_txtPeeValue.AccessibleName = "save";
            this.m_txtPeeValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtPeeValue.BorderColor = System.Drawing.Color.White;
            this.m_txtPeeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtPeeValue.ForeColor = System.Drawing.Color.White;
            this.m_txtPeeValue.Location = new System.Drawing.Point(24, 68);
            this.m_txtPeeValue.Name = "m_txtPeeValue";
            this.m_txtPeeValue.Size = new System.Drawing.Size(112, 23);
            this.m_txtPeeValue.TabIndex = 902;
            // 
            // m_grbBreath
            // 
            this.m_grbBreath.Controls.Add(this.panel1);
            this.m_grbBreath.Controls.Add(this.m_cboBreathTime);
            this.m_grbBreath.Controls.Add(this.lblBreathUnit);
            this.m_grbBreath.Controls.Add(this.m_txtBreathValue);
            this.m_grbBreath.Controls.Add(this.lblBreathTime);
            this.m_grbBreath.ForeColor = System.Drawing.Color.White;
            this.m_grbBreath.Location = new System.Drawing.Point(-204, 4);
            this.m_grbBreath.Name = "m_grbBreath";
            this.m_grbBreath.Size = new System.Drawing.Size(260, 180);
            this.m_grbBreath.TabIndex = 400;
            this.m_grbBreath.TabStop = false;
            this.m_grbBreath.Text = "����";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_rdbBreathStopAssistant);
            this.panel1.Controls.Add(this.m_rdbBreathStartAssistant);
            this.panel1.Controls.Add(this.m_rdbBreathNormal);
            this.panel1.Location = new System.Drawing.Point(8, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 40);
            this.panel1.TabIndex = 466;
            // 
            // m_rdbBreathStopAssistant
            // 
            this.m_rdbBreathStopAssistant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbBreathStopAssistant.Location = new System.Drawing.Point(140, 8);
            this.m_rdbBreathStopAssistant.Name = "m_rdbBreathStopAssistant";
            this.m_rdbBreathStopAssistant.Size = new System.Drawing.Size(105, 24);
            this.m_rdbBreathStopAssistant.TabIndex = 404;
            this.m_rdbBreathStopAssistant.Text = "ͣ��������";
            // 
            // m_rdbBreathStartAssistant
            // 
            this.m_rdbBreathStartAssistant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbBreathStartAssistant.Location = new System.Drawing.Point(52, 8);
            this.m_rdbBreathStartAssistant.Name = "m_rdbBreathStartAssistant";
            this.m_rdbBreathStartAssistant.Size = new System.Drawing.Size(92, 24);
            this.m_rdbBreathStartAssistant.TabIndex = 403;
            this.m_rdbBreathStartAssistant.Text = "��������";
            // 
            // m_rdbBreathNormal
            // 
            this.m_rdbBreathNormal.Checked = true;
            this.m_rdbBreathNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbBreathNormal.Location = new System.Drawing.Point(0, 8);
            this.m_rdbBreathNormal.Name = "m_rdbBreathNormal";
            this.m_rdbBreathNormal.Size = new System.Drawing.Size(56, 24);
            this.m_rdbBreathNormal.TabIndex = 402;
            this.m_rdbBreathNormal.TabStop = true;
            this.m_rdbBreathNormal.Text = "һ��";
            // 
            // m_cboBreathTime
            // 
            this.m_cboBreathTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboBreathTime.BorderColor = System.Drawing.Color.White;
            this.m_cboBreathTime.DropButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboBreathTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboBreathTime.DropButtonForeColor = System.Drawing.Color.White;
            this.m_cboBreathTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboBreathTime.flatFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBreathTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboBreathTime.ForeColor = System.Drawing.Color.White;
            this.m_cboBreathTime.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboBreathTime.ListForeColor = System.Drawing.Color.White;
            this.m_cboBreathTime.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboBreathTime.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboBreathTime.Location = new System.Drawing.Point(92, 28);
            this.m_cboBreathTime.m_BlnEnableItemEventMenu = true;
            this.m_cboBreathTime.Name = "m_cboBreathTime";
            this.m_cboBreathTime.SelectedIndex = -1;
            this.m_cboBreathTime.SelectedItem = null;
            this.m_cboBreathTime.SelectionStart = 0;
            this.m_cboBreathTime.Size = new System.Drawing.Size(76, 26);
            this.m_cboBreathTime.TabIndex = 401;
            this.m_cboBreathTime.TabStop = false;
            this.m_cboBreathTime.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_cboBreathTime.TextForeColor = System.Drawing.Color.White;
            // 
            // lblBreathUnit
            // 
            this.lblBreathUnit.AutoSize = true;
            this.lblBreathUnit.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBreathUnit.ForeColor = System.Drawing.Color.White;
            this.lblBreathUnit.Location = new System.Drawing.Point(128, 124);
            this.lblBreathUnit.Name = "lblBreathUnit";
            this.lblBreathUnit.Size = new System.Drawing.Size(48, 16);
            this.lblBreathUnit.TabIndex = 465;
            this.lblBreathUnit.Text = "��/��";
            // 
            // m_txtBreathValue
            // 
            this.m_txtBreathValue.AccessibleName = "save";
            this.m_txtBreathValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtBreathValue.BorderColor = System.Drawing.Color.White;
            this.m_txtBreathValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtBreathValue.ForeColor = System.Drawing.Color.White;
            this.m_txtBreathValue.Location = new System.Drawing.Point(8, 120);
            this.m_txtBreathValue.Name = "m_txtBreathValue";
            this.m_txtBreathValue.Size = new System.Drawing.Size(112, 23);
            this.m_txtBreathValue.TabIndex = 405;
            // 
            // lblBreathTime
            // 
            this.lblBreathTime.AutoSize = true;
            this.lblBreathTime.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBreathTime.ForeColor = System.Drawing.Color.White;
            this.lblBreathTime.Location = new System.Drawing.Point(16, 31);
            this.lblBreathTime.Name = "lblBreathTime";
            this.lblBreathTime.Size = new System.Drawing.Size(72, 16);
            this.lblBreathTime.TabIndex = 465;
            this.lblBreathTime.Text = "ʱ��Σ�";
            // 
            // m_grbWeight
            // 
            this.m_grbWeight.Controls.Add(this.panel3);
            this.m_grbWeight.Controls.Add(this.m_txtWeightValue);
            this.m_grbWeight.ForeColor = System.Drawing.Color.White;
            this.m_grbWeight.Location = new System.Drawing.Point(-204, 4);
            this.m_grbWeight.Name = "m_grbWeight";
            this.m_grbWeight.Size = new System.Drawing.Size(260, 116);
            this.m_grbWeight.TabIndex = 1200;
            this.m_grbWeight.TabStop = false;
            this.m_grbWeight.Text = "����";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_rdbWeightNormal);
            this.panel3.Controls.Add(this.m_rdbWeightCar);
            this.panel3.Controls.Add(this.m_rdbWeightBed);
            this.panel3.Location = new System.Drawing.Point(12, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 32);
            this.panel3.TabIndex = 1205;
            // 
            // m_rdbWeightNormal
            // 
            this.m_rdbWeightNormal.Checked = true;
            this.m_rdbWeightNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbWeightNormal.Location = new System.Drawing.Point(8, 4);
            this.m_rdbWeightNormal.Name = "m_rdbWeightNormal";
            this.m_rdbWeightNormal.Size = new System.Drawing.Size(56, 24);
            this.m_rdbWeightNormal.TabIndex = 1201;
            this.m_rdbWeightNormal.TabStop = true;
            this.m_rdbWeightNormal.Text = "һ��";
            // 
            // m_rdbWeightCar
            // 
            this.m_rdbWeightCar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbWeightCar.Location = new System.Drawing.Point(84, 4);
            this.m_rdbWeightCar.Name = "m_rdbWeightCar";
            this.m_rdbWeightCar.Size = new System.Drawing.Size(60, 24);
            this.m_rdbWeightCar.TabIndex = 1202;
            this.m_rdbWeightCar.Text = "����";
            // 
            // m_rdbWeightBed
            // 
            this.m_rdbWeightBed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_rdbWeightBed.Location = new System.Drawing.Point(168, 4);
            this.m_rdbWeightBed.Name = "m_rdbWeightBed";
            this.m_rdbWeightBed.Size = new System.Drawing.Size(56, 24);
            this.m_rdbWeightBed.TabIndex = 1203;
            this.m_rdbWeightBed.Text = "�Դ�";
            // 
            // m_txtWeightValue
            // 
            this.m_txtWeightValue.AccessibleDescription = "����";
            this.m_txtWeightValue.AccessibleName = "save";
            this.m_txtWeightValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(113)))), ((int)(((byte)(152)))));
            this.m_txtWeightValue.BorderColor = System.Drawing.Color.White;
            this.m_txtWeightValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtWeightValue.ForeColor = System.Drawing.Color.White;
            this.m_txtWeightValue.Location = new System.Drawing.Point(20, 72);
            this.m_txtWeightValue.Name = "m_txtWeightValue";
            this.m_txtWeightValue.Size = new System.Drawing.Size(112, 23);
            this.m_txtWeightValue.TabIndex = 1204;
            // 
            // frmThreeMeasureRecord
            // 
            this.AccessibleDescription = "";
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(907, 589);
            this.Controls.Add(this.pnlRecordContain);
            this.Controls.Add(this.cboCreateDate);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.m_grpOperation);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.m_cmdGeneralNurse);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_cmdDelete);
            this.Controls.Add(this.m_cmdGetDovueData);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = false;
            this.Name = "frmThreeMeasureRecord";
            this.Text = "���µ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmThreeMeasureRecord_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthKeyDown);
            this.Load += new System.EventHandler(this.frmThreeMeasureRecord_Load);
            this.Controls.SetChildIndex(this.m_cmdGetDovueData, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_cmdDelete, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.m_cmdGeneralNurse, 0);
            this.Controls.SetChildIndex(this.m_cmdSave, 0);
            this.Controls.SetChildIndex(this.m_grpOperation, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
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
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.cboCreateDate, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.pnlRecordContain, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.m_grbOther.ResumeLayout(false);
            this.m_grbSkinTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuSkinBadCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuEventHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuEventMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picZoom)).EndInit();
            this.m_grpOperation.ResumeLayout(false);
            this.m_grpOperation.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.pnlRecordContain.ResumeLayout(false);
            this.pnlRecordContain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctlRecord)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grbRecordType.ResumeLayout(false);
            this.m_grbSpecialDate.ResumeLayout(false);
            this.m_grbTemperature.ResumeLayout(false);
            this.m_grbTemperature.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.m_grbDejecta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaBeforeTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaClysisTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nmuDejectaAfterTimes)).EndInit();
            this.m_grbEvent.ResumeLayout(false);
            this.m_grbDownTemperature.ResumeLayout(false);
            this.m_grbDownTemperature.PerformLayout();
            this.m_grbPulse.ResumeLayout(false);
            this.m_grbPulse.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.m_grbPressure.ResumeLayout(false);
            this.m_grbPee.ResumeLayout(false);
            this.m_grbPee.PerformLayout();
            this.m_grbBreath.ResumeLayout(false);
            this.m_grbBreath.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.m_grbWeight.ResumeLayout(false);
            this.m_grbWeight.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// ��ǰѡ�еĵ�ѡť
		/// </summary>
		//		private RadioButton m_rdbCur;

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_lngSave();
				
				//				m_mthJumpToNextRadio();

				//				m_mthClear();
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox("��������Ч����ֵ��");
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// ��ս���
		/// </summary>
		private void m_mthClear()
		{
            if (m_cboTimeFlag == null) return;

			m_cboTimeFlag.SelectedIndex = m_intGetLastestTime(DateTime.Now);
			m_cboEventType.Text = "";
			m_cboTemperatureType.Text = "Ҹ��:";
			m_cboPulseType.Text = "����:";
			m_cboTemperature.Text = "";
			m_cboPulse.Text = "";
			m_cboBreathValue.Text = "";
			m_txtPressureSystolicValue.Text = "";
			m_txtPressureDiastolicValue.Text = "";
			m_cboDejectaBeforeTimes.Text = "";
			m_chkNeedWeight.Checked = false;
			m_txtDejectaWeightValue.Text = "";
			m_cboPeeValue.Text = "";
			m_txtOutStreamValue.Text = "";
			m_txtInputValue.Text = "";
			m_cboWeightValue.Text = "";
			m_cboSkinTestMedicine.Text = "";
			m_cboSkinBadCount.Text = "";
			m_cboOtherItem.Text = "";
			m_txtOtherValue.Text = "";
			m_nmuEventHour.Value = DateTime.Now.Hour;
			m_nmuEventMinute.Value = DateTime.Now.Minute;

			for(int i = 0; i < m_grpOperation.Controls.Count; i++)
			{
				m_grpOperation.Controls[i].Enabled = true;
			}
		}

		/// <summary>
		/// ������һ��ѡť
		/// </summary>
		private void m_mthJumpToNextRadio()
		{
			foreach(Control ctlSub in grbRecordType.Controls)
			{
				RadioButton rdb = ctlSub as RadioButton;
				if(rdb != null && rdb.Checked)
				{
					rdb.Focus();
					SendKeys.Send("{right}");
				}
			}
			//			if(m_rdbCur != null)
			//			{
			//				m_rdbCur.Focus();
			//				SendKeys.Send("{right}");
			//			}
		}

		#region �޸ĺ������޸Ľ������ʾ�������������ݿ⡣��
		private long m_lngModify(string p_strServerTime)
		{
			if(m_dtpRecordDateTime.Tag == null)
			{
				clsPublicFunction.ShowInformationMessageBox("��ѡ�����еļ�¼��");
				return -6;
			}

			long lngRes = -1;

			#region old
			//			if(m_rdbSpecialDateType.Checked)
			//			{
			//				clsThreeMeasureSpecialDate objOldValue = (clsThreeMeasureSpecialDate)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureSpecialDate objNewValue = new clsThreeMeasureSpecialDate();
			//				objNewValue.m_dtmSpecialDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_blnIsNewStart = m_chkNewSpecialDate.Checked;
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifySpecialDate(objOldValue,objNewValue)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSpecialDate();
			//				}
			//			}
			//			else if(m_rdbEventType.Checked)
			//			{
			//				clsThreeMeasureEvent objOldValue = (clsThreeMeasureEvent)m_dtpRecordDateTime.Tag;
			//
			//				clsThreeMeasureEvent objNewValue = new clsThreeMeasureEvent();
			//				objNewValue.m_dtmEventTime = m_dtpRecordDateTime.Value.Date.AddHours(Decimal.ToDouble(m_nmuEventHour.Value)).AddMinutes(Decimal.ToDouble(m_nmuEventMinute.Value));
			//				objNewValue.m_enmEventType = (enmThreeMeasureEventType)m_cboEventType.SelectedItem;
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyEvent(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetEvent();
			//				}
			//			}
			//			else if(m_rdbPulseType.Checked)
			//			{
			//				int intValue = int.Parse(m_txtPulseValue.Text);
			//				if(intValue > 180 || intValue < 0)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ������0��180֮�䡣");
			//					return -1;
			//				}
			//
			//				clsThreeMeasurePulseValue objOldValue = (clsThreeMeasurePulseValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasurePulseValue objNewValue = new clsThreeMeasurePulseValue();
			//
			//				switch(m_cboPulseTimeFlag.SelectedIndex)
			//				{
			//					case 0://4 am
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am4;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am4h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
			//						}
			//						break;
			//					case 1://8 am
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am8;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am8h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
			//						}
			//						break;
			//					case 2://12 am
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am12;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am12h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
			//						}
			//						break;
			//					case 3://4 pm
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm4;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm4h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
			//						}
			//						break;
			//					case 4://8 pm
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm8;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm8h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
			//						}
			//						break;
			//					case 5://12 pm
			//						if(!m_chkPulseIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm12;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm12h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
			//						}
			//						break;
			//				}
			//
			//				if(m_rdbPulse.Checked)
			//					objNewValue.m_enmType = enmThreeMeasurePulseType.����;
			//				else
			//					objNewValue.m_enmType = enmThreeMeasurePulseType.����;
			//				objNewValue.m_intValue = int.Parse(m_txtPulseValue.Text);
			//				objNewValue.m_blnLineToPreValue = !m_chkPulseNotLineToPre.Checked;
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyPulseValue(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPulse();
			//				}
			//			}
			//			else if(m_rdbBreathType.Checked)
			//			{
			//				clsThreeMeasureBreathValue objOldValue = (clsThreeMeasureBreathValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureBreathValue objNewValue = new clsThreeMeasureBreathValue();
			//
			//				switch(m_cboBreathTime.SelectedIndex)
			//				{
			//					case 0://4 am					
			//						objNewValue.m_enmParamTime = enmParamTime.am4;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);					
			//						break;
			//					case 1://8 am					
			//						objNewValue.m_enmParamTime = enmParamTime.am8;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);					
			//						break;
			//					case 2://12 am					
			//						objNewValue.m_enmParamTime = enmParamTime.am12;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);					
			//						break;
			//					case 3://4 pm					
			//						objNewValue.m_enmParamTime = enmParamTime.pm4;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
			//						break;
			//					case 4://8 pm
			//						objNewValue.m_enmParamTime = enmParamTime.pm8;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
			//						break;
			//					case 5://12 pm
			//						objNewValue.m_enmParamTime = enmParamTime.pm12;
			//
			//						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
			//						break;
			//				}
			//
			//				if(m_rdbBreathStartAssistant.Checked)
			//					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.��������;
			//				else if(m_rdbBreathStopAssistant.Checked)
			//					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.ͣ��������;
			//				else
			//				{
			//					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.һ��;
			//					objNewValue.m_intValue = int.Parse(m_txtBreathValue.Text);
			//				}
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyBreath(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetBreath();
			//				}
			//			}
			//			else if(m_rdbTemperatureType.Checked)
			//			{
			//				float fltValue = float.Parse(m_txtTemperatureValue.Text);
			//				if(fltValue > 42f)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
			//					return -1;
			//				}
			//
			//				clsThreeMeasureTemperatureValue objOldValue = (clsThreeMeasureTemperatureValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureTemperatureValue objNewValue = new clsThreeMeasureTemperatureValue();
			//				
			//				switch(m_cboTemperatureTimeFlag.SelectedIndex)
			//				{
			//					case 0://4 am
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am4;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am4h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
			//						}
			//						break;
			//					case 1://8 am
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am8;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am8h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
			//						}
			//						break;
			//					case 2://12 am
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am12;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.am12h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
			//						}
			//						break;
			//					case 3://4 pm
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm4;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm4h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
			//						}
			//						break;
			//					case 4://8 pm
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm8;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm8h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
			//						}
			//						break;
			//					case 5://12 pm
			//						if(!m_chkTemperatureIsHalf.Checked)
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm12;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
			//						}
			//						else
			//						{
			//							objNewValue.m_enmParamTime = enmParamTime.pm12h;
			//
			//							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
			//						}
			//						break;
			//				}
			//
			//				if(m_rdbMouthTemperature.Checked)
			//					objNewValue.m_enmType = enmThreeMeasureTemperatureType.�ڱ��¶�;
			//				else if(m_rdbArmpitTemperature.Checked)
			//					objNewValue.m_enmType = enmThreeMeasureTemperatureType.Ҹ���¶�;
			//				else
			//					objNewValue.m_enmType = enmThreeMeasureTemperatureType.�ر��¶�;
			//				objNewValue.m_blnLineToPreValue = !m_chkTemperatureNotLineToPre.Checked;
			//				objNewValue.m_fltValue = float.Parse(m_txtTemperatureValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyTemperatureValue(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetTemperature();
			//				}
			//			}
			//			else if(m_rdbDownTemperatureType.Checked)
			//			{
			//				float fltValue = float.Parse(m_txtDownTemperatureValue.Text);
			//				if(fltValue > 42f)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
			//					return -1;
			//				}
			//
			//				clsThreeMeasureTemperaturePhyscalDownValue objOldValue = (clsThreeMeasureTemperaturePhyscalDownValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureTemperaturePhyscalDownValue objNewValue = new clsThreeMeasureTemperaturePhyscalDownValue();
			//				objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value;
			//				objNewValue.m_fltValue = float.Parse(m_txtDownTemperatureValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//				
			//				lngRes = m_ctlRecord.m_blnModifyPhyscalDownValue(objOldValue,objNewValue,m_objBaseTemperature,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetDownTemperature();
			//				}
			//			}
			//			else if(m_rdbInputType.Checked)
			//			{
			//				clsThreeMeasureInputValue objOldValue = (clsThreeMeasureInputValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureInputValue objNewValue = new clsThreeMeasureInputValue();
			//				objNewValue.m_dtmInputDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_fltValue = float.Parse(m_txtInputValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyInput(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetInput();
			//				}
			//			}
			//			else if(m_rdbDejectaType.Checked)
			//			{
			//				clsThreeMeasureDejectaValue objOldValue = (clsThreeMeasureDejectaValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureDejectaValue objNewValue = new clsThreeMeasureDejectaValue();
			//				objNewValue.m_dtmDejectaDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_blnCanDejecta = !m_chkCannotDejecta.Checked;
			//				if(objNewValue.m_blnCanDejecta)
			//				{
			//					objNewValue.m_blnNeedWeight = m_chkNeedWeight.Checked;
			//					if(objNewValue.m_blnNeedWeight)
			//					{
			//						objNewValue.m_fltWeight = float.Parse(m_txtDejectaWeightValue.Text);
			//					}
			//					objNewValue.m_blnAfterMoreTimes = m_chkDejectaAfterMoreTimes.Checked;
			//					if(!objNewValue.m_blnAfterMoreTimes)
			//						objNewValue.m_intAfterTimes = Decimal.ToInt32(m_nmuDejectaAfterTimes.Value);
			//					objNewValue.m_intBeforeTimes = Decimal.ToInt32(m_nmuDejectaBeforeTimes.Value);
			//					objNewValue.m_intClysisTimes = Decimal.ToInt32(m_nmuDejectaClysisTimes.Value);
			//				}
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyDejecta(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetDejecta();
			//				}
			//			}
			//			else if(m_rdbPeeType.Checked)
			//			{
			//				clsThreeMeasurePeeValue objOldValue = (clsThreeMeasurePeeValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasurePeeValue objNewValue = new clsThreeMeasurePeeValue();
			//				objNewValue.m_dtmPeeDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_blnIsIrretention = m_chkIsIrretention.Checked;
			//				if(!objNewValue.m_blnIsIrretention)
			//				{
			//					objNewValue.m_fltValue = float.Parse(m_txtPeeValue.Text);
			//				}
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//			
			//				lngRes = m_ctlRecord.m_blnModifyPee(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPee();
			//				}
			//			}
			//			else if(m_rdbOutStreamType.Checked)
			//			{
			//				clsThreeMeasureOutStreamValue objOldValue = (clsThreeMeasureOutStreamValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureOutStreamValue objNewValue = new clsThreeMeasureOutStreamValue();
			//				objNewValue.m_dtmOutStreamDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_fltValue = float.Parse(m_txtOutStreamValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyOutStream(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOutStream();
			//				}
			//			}
			//			else if(m_rdbPressureType.Checked)
			//			{
			//				clsThreeMeasurePressureArg objArgValue = (clsThreeMeasurePressureArg)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasurePressureValue objNewValue = new clsThreeMeasurePressureValue();
			//				objNewValue.m_dtmPressureDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_fltDiastolicValue = float.Parse(m_txtPressureDiastolicValue.Text);
			//				objNewValue.m_fltSystolicValue = float.Parse(m_txtPressureSystolicValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyPressure(objArgValue.m_objPressure,objNewValue,objArgValue.m_intPressureIndex,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPressure();
			//				}
			//			}
			//			else if(m_rdbWeightType.Checked)
			//			{
			//				clsThreeMeasureWeightValue objOldValue = (clsThreeMeasureWeightValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureWeightValue objNewValue = new clsThreeMeasureWeightValue();
			//				objNewValue.m_dtmWeightDate = m_dtpRecordDateTime.Value;
			//				if(m_rdbWeightNormal.Checked)
			//				{
			//					objNewValue.m_enmWeightType = enmThreeMeasureWeightType.һ��;
			//					objNewValue.m_fltValue = float.Parse(m_txtWeightValue.Text);			
			//				}
			//				else if(m_rdbWeightBed.Checked)
			//					objNewValue.m_enmWeightType = enmThreeMeasureWeightType.�Դ�;
			//				else
			//					objNewValue.m_enmWeightType = enmThreeMeasureWeightType.����;
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//
			//				lngRes = m_ctlRecord.m_blnModifyWeight(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetWeight();
			//				}
			//			}
			//			else if(m_rdbSkinTestType.Checked)
			//			{
			//				clsThreeMeasureSkinTestValue objOldValue = (clsThreeMeasureSkinTestValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureSkinTestValue objNewValue = new clsThreeMeasureSkinTestValue();
			//				objNewValue.m_dtmSkinTestDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_strMedicineName = m_cboSkinTestMedicine.Text;
			//				objNewValue.m_blnIsBad = m_rdbSkinTestBad.Checked;
			//				objNewValue.m_intBadCount = Decimal.ToInt32(m_nmuSkinBadCount.Value);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//			
			//				lngRes = m_ctlRecord.m_blnModifySkinTest(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSkinTest();
			//				}
			//			}
			//			else if(m_rdbOtherType.Checked)
			//			{
			//				clsThreeMeasureOtherValue objOldValue = (clsThreeMeasureOtherValue)m_dtpRecordDateTime.Tag;
			//				
			//				clsThreeMeasureOtherValue objNewValue = new clsThreeMeasureOtherValue();
			//				objNewValue.m_dtmOtherDate = m_dtpRecordDateTime.Value;
			//				objNewValue.m_strOtherItem = m_cboOtherItem.Text;
			//				objNewValue.m_strOtherUnit = m_cboOtherUnit.Text;
			//				objNewValue.m_fltOtherValue = float.Parse(m_txtOtherValue.Text);
			//				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			//			
			//				lngRes = m_ctlRecord.m_blnModifyOther(objOldValue,objNewValue,m_blnIsControl)?1:0;
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOther();
			//				}
			//			}
			#endregion
			if(m_cboEventType.Enabled && !m_nmuEventHour.Enabled)
			{
				clsThreeMeasureSpecialDate objOldValue = (clsThreeMeasureSpecialDate)m_dtpRecordDateTime.Tag;
				
				clsThreeMeasureSpecialDate objNewValue = new clsThreeMeasureSpecialDate();
				objNewValue.m_dtmSpecialDate = m_dtpRecordDateTime.Value;
				//				objNewValue.m_blnIsNewStart = m_chkNewSpecialDate.Checked;
				objNewValue.m_blnIsNewStart = m_cboEventType.Text=="����";
				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

				lngRes = m_ctlRecord.m_blnModifySpecialDate(objOldValue,objNewValue)?1:0;

				if(lngRes == 1)
				{
					m_mthResetSpecialDate();
				}
			}
			else if(m_cboEventType.Enabled && m_nmuEventHour.Enabled)
			{
				clsThreeMeasureEvent objOldValue = (clsThreeMeasureEvent)m_dtpRecordDateTime.Tag;

				clsThreeMeasureEvent objNewValue = new clsThreeMeasureEvent();
				objNewValue.m_dtmEventTime = m_dtpRecordDateTime.Value.Date.AddHours(Decimal.ToDouble(m_nmuEventHour.Value)).AddMinutes(Decimal.ToDouble(m_nmuEventMinute.Value));
				objNewValue.m_enmEventType = (enmThreeMeasureEventType)m_cboEventType.SelectedItem;
				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

				lngRes = m_ctlRecord.m_blnModifyEvent(objOldValue,objNewValue,m_blnIsControl)?1:0;

				if(lngRes == 1)
				{
					m_mthResetEvent();
				}
			}
			else if(m_cboPulseType.Enabled)
			{
				int intValue = int.Parse(m_cboPulse.Text);
				if(intValue > 180 || intValue < 0)
				{
					clsPublicFunction.ShowInformationMessageBox("������ֵ������0��180֮�䡣");
					return -1;
				}

				clsThreeMeasurePulseValue objOldValue = (clsThreeMeasurePulseValue)m_dtpRecordDateTime.Tag;
				
				clsThreeMeasurePulseValue objNewValue = new clsThreeMeasurePulseValue();

				switch(m_cboTimeFlag.SelectedIndex)
				{
					case 0://4 am
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am4;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am4h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
						}
						break;
					case 1://8 am
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am8;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am8h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
						}
						break;
					case 2://12 am
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am12;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am12h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
						}
						break;
					case 3://4 pm
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm4;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm4h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
						}
						break;
					case 4://8 pm
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm8;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm8h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
						}
						break;
					case 5://12 pm
						if(!m_chkPulseIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm12;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm12h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
						}
						break;
				}

				if(m_cboPulseType.SelectedIndex == 0)
					objNewValue.m_enmType = enmThreeMeasurePulseType.����;
				else
					objNewValue.m_enmType = enmThreeMeasurePulseType.����;
				objNewValue.m_intValue = int.Parse(m_cboPulse.Text);
				//				objNewValue.m_blnLineToPreValue = !m_chkPulseNotLineToPre.Checked;
				objNewValue.m_blnLineToPreValue = true;
				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

				lngRes = m_ctlRecord.m_blnModifyPulseValue(objOldValue,objNewValue,m_blnIsControl)?1:0;

				if(lngRes == 1)
				{
					m_mthResetPulse();
				}
			}
			else if(m_cboBreathValue.Enabled)
			{
				clsThreeMeasureBreathValue objOldValue = (clsThreeMeasureBreathValue)m_dtpRecordDateTime.Tag;
				
				clsThreeMeasureBreathValue objNewValue = new clsThreeMeasureBreathValue();

				switch(m_cboTimeFlag.SelectedIndex)
				{
					case 0://4 am					
						objNewValue.m_enmParamTime = enmParamTime.am4;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);					
						break;
					case 1://8 am					
						objNewValue.m_enmParamTime = enmParamTime.am8;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);					
						break;
					case 2://12 am					
						objNewValue.m_enmParamTime = enmParamTime.am12;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);					
						break;
					case 3://4 pm					
						objNewValue.m_enmParamTime = enmParamTime.pm4;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
						break;
					case 4://8 pm
						objNewValue.m_enmParamTime = enmParamTime.pm8;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
						break;
					case 5://12 pm
						objNewValue.m_enmParamTime = enmParamTime.pm12;

						objNewValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
						break;
				}

				if(m_cboBreathValue.Text=="��������")
					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.��������;
				else if(m_rdbBreathStopAssistant.Checked)
					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.ͣ��������;
				else
				{
					objNewValue.m_enmBreathType = enmThreeMeasureBreathType.һ��;
					objNewValue.m_intValue = int.Parse(m_cboBreathValue.Text);
				}
				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

				lngRes = m_ctlRecord.m_blnModifyBreath(objOldValue,objNewValue,m_blnIsControl)?1:0;

				if(lngRes == 1)
				{
					m_mthResetBreath();
				}
			}
			else if(m_cboTemperatureType.Enabled && m_cboTemperatureType.SelectedIndex != 3)
			{
				float fltValue = float.Parse(m_cboTemperature.Text);
				if(fltValue > 42f)
				{
					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
					return -1;
				}

				clsThreeMeasureTemperatureValue objOldValue = (clsThreeMeasureTemperatureValue)m_dtpRecordDateTime.Tag;
				
				clsThreeMeasureTemperatureValue objNewValue = new clsThreeMeasureTemperatureValue();
				
				switch(m_cboTimeFlag.SelectedIndex)
				{
					case 0://4 am
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am4;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am4h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
						}
						break;
					case 1://8 am
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am8;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am8h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
						}
						break;
					case 2://12 am
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.am12;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.am12h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
						}
						break;
					case 3://4 pm
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm4;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm4h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
						}
						break;
					case 4://8 pm
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm8;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm8h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
						}
						break;
					case 5://12 pm
						if(!m_chkTemperatureIsHalf.Checked)
						{
							objNewValue.m_enmParamTime = enmParamTime.pm12;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
						}
						else
						{
							objNewValue.m_enmParamTime = enmParamTime.pm12h;

							objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
						}
						break;
				}

				if(m_cboTemperatureType.SelectedIndex==0)
					objNewValue.m_enmType = enmThreeMeasureTemperatureType.�ڱ��¶�;
				else if(m_cboTemperatureType.SelectedIndex==2)
					objNewValue.m_enmType = enmThreeMeasureTemperatureType.Ҹ���¶�;
				else if(m_cboTemperatureType.SelectedIndex==1)
					objNewValue.m_enmType = enmThreeMeasureTemperatureType.�ر��¶�;
				//				objNewValue.m_blnLineToPreValue = !m_chkTemperatureNotLineToPre.Checked;
				objNewValue.m_blnLineToPreValue = true;
				objNewValue.m_fltValue = float.Parse(m_cboTemperature.Text);
				objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

				lngRes = m_ctlRecord.m_blnModifyTemperatureValue(objOldValue,objNewValue,m_blnIsControl)?1:0;

				if(lngRes == 1)
				{
					m_mthResetTemperature();
				}
			}
            else if (!m_cboTemperatureType.Enabled && m_cboTemperatureType.SelectedIndex == 3)
            {
                float fltValue = float.Parse(m_txtDownTemperatureValue.Text);
                if (fltValue > 42f)
                {
                    clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
                    return -1;
                }

                clsThreeMeasureTemperaturePhyscalDownValue objOldValue = (clsThreeMeasureTemperaturePhyscalDownValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureTemperaturePhyscalDownValue objNewValue = new clsThreeMeasureTemperaturePhyscalDownValue();
                objNewValue.m_dtmValueTime = m_dtpRecordDateTime.Value;
                objNewValue.m_fltValue = float.Parse(m_cboTemperature.Text);
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyPhyscalDownValue(objOldValue, objNewValue, m_objBaseTemperature, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetDownTemperature();
                }
            }
            else if (m_txtInputValue.Enabled)
            {
                clsThreeMeasureInputValue objOldValue = (clsThreeMeasureInputValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureInputValue objNewValue = new clsThreeMeasureInputValue();
                objNewValue.m_dtmInputDate = m_dtpRecordDateTime.Value;
                objNewValue.m_fltValue = float.Parse(m_txtInputValue.Text);
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyInput(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetInput();
                }
            }
            else if (m_cboDejectaBeforeTimes.Enabled)
            {
                clsThreeMeasureDejectaValue objOldValue = (clsThreeMeasureDejectaValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureDejectaValue objNewValue = new clsThreeMeasureDejectaValue();
                objNewValue.m_dtmDejectaDate = m_dtpRecordDateTime.Value;
                objNewValue.m_blnCanDejecta = !(m_cboDejectaBeforeTimes.Text == "*");
                if (objNewValue.m_blnCanDejecta)
                {
                    string strContent = m_cboDejectaBeforeTimes.Text.Trim();
                    objNewValue.m_blnAfterMoreTimes = strContent.EndsWith("*/E");
                    int intIndexOfSpace = strContent.IndexOf(" ");
                    int intIndexOfBias = strContent.IndexOf("/");
                    if (intIndexOfSpace == -1)
                    {
                        if (intIndexOfBias == -1)
                        {
                            objNewValue.m_intBeforeTimes = int.Parse(strContent);
                            objNewValue.m_intClysisTimes = 0;
                        }
                        else
                            objNewValue.m_intBeforeTimes = 0;

                        if (!objNewValue.m_blnAfterMoreTimes && intIndexOfBias != -1)
                        {
                            objNewValue.m_intAfterTimes = int.Parse(strContent.Substring(0, intIndexOfBias));
                            objNewValue.m_intClysisTimes = 1;
                        }
                    }
                    else
                    {
                        objNewValue.m_intBeforeTimes = int.Parse(strContent.Substring(0, intIndexOfSpace));
                        if (!objNewValue.m_blnAfterMoreTimes && intIndexOfBias != -1)
                        {
                            objNewValue.m_intAfterTimes = int.Parse(strContent.Substring(intIndexOfSpace, intIndexOfBias - intIndexOfSpace));
                            objNewValue.m_intClysisTimes = 1;
                        }

                    }
                }

                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyDejecta(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetDejecta();
                }
            }
            else if (m_cboPeeValue.Enabled)
            {
                clsThreeMeasurePeeValue objOldValue = (clsThreeMeasurePeeValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasurePeeValue objNewValue = new clsThreeMeasurePeeValue();
                objNewValue.m_dtmPeeDate = m_dtpRecordDateTime.Value;
                objNewValue.m_blnIsIrretention = m_cboPeeValue.Text == "*";
                if (!objNewValue.m_blnIsIrretention)
                {
                    objNewValue.m_fltValue = float.Parse(m_cboPeeValue.Text);
                }
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyPee(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetPee();
                }
            }
            else if (m_txtOutStreamValue.Enabled)
            {
                clsThreeMeasureOutStreamValue objOldValue = (clsThreeMeasureOutStreamValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureOutStreamValue objNewValue = new clsThreeMeasureOutStreamValue();
                objNewValue.m_dtmOutStreamDate = m_dtpRecordDateTime.Value;
                objNewValue.m_fltValue = float.Parse(m_txtOutStreamValue.Text);
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyOutStream(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetOutStream();
                }
            }
            else if (m_txtPressureSystolicValue.Enabled && m_txtPressureDiastolicValue.Enabled)
            {
                clsThreeMeasurePressureArg objArgValue = (clsThreeMeasurePressureArg)m_dtpRecordDateTime.Tag;

                clsThreeMeasurePressureValue objNewValue = new clsThreeMeasurePressureValue();
                objNewValue.m_dtmPressureDate = m_dtpRecordDateTime.Value;
                objNewValue.m_fltDiastolicValue = float.Parse(m_txtPressureDiastolicValue.Text);
                objNewValue.m_fltSystolicValue = float.Parse(m_txtPressureSystolicValue.Text);
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyPressure(objArgValue.m_objPressure, objNewValue, objArgValue.m_intPressureIndex, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetPressure();
                }
            }
            else if (m_cboWeightValue.Enabled)
            {
                clsThreeMeasureWeightValue objOldValue = (clsThreeMeasureWeightValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureWeightValue objNewValue = new clsThreeMeasureWeightValue();
                objNewValue.m_dtmWeightDate = m_dtpRecordDateTime.Value;
                try
                {
                    objNewValue.m_enmWeightType = enmThreeMeasureWeightType.һ��;
                    objNewValue.m_fltValue = float.Parse(m_cboWeightValue.Text);
                }
                catch
                {
                    if (m_cboWeightValue.Text == "�Դ�")
                        objNewValue.m_enmWeightType = enmThreeMeasureWeightType.�Դ�;
                    else
                        objNewValue.m_enmWeightType = enmThreeMeasureWeightType.����;
                }
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyWeight(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetWeight();
                }
            }
            else if (m_cboSkinBadCount.Enabled)
            {
                clsThreeMeasureSkinTestValue objOldValue = (clsThreeMeasureSkinTestValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureSkinTestValue objNewValue = new clsThreeMeasureSkinTestValue();
                objNewValue.m_dtmSkinTestDate = m_dtpRecordDateTime.Value;
                objNewValue.m_strMedicineName = m_cboSkinTestMedicine.Text;
                objNewValue.m_blnIsBad = m_cboSkinBadCount.Text.StartsWith("+");
                objNewValue.m_intBadCount = m_cboSkinBadCount.Text.Trim().Length;
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifySkinTest(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetSkinTest();
                }
            }
            else if (m_txtOtherValue.Enabled)
            {
                clsThreeMeasureOtherValue objOldValue = (clsThreeMeasureOtherValue)m_dtpRecordDateTime.Tag;

                clsThreeMeasureOtherValue objNewValue = new clsThreeMeasureOtherValue();
                objNewValue.m_dtmOtherDate = m_dtpRecordDateTime.Value;
                objNewValue.m_strOtherItem = m_cboOtherItem.Text;
                objNewValue.m_strOtherUnit = m_cboOtherUnit.Text;
                objNewValue.m_fltOtherValue = float.Parse(m_txtOtherValue.Text);
                objNewValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

                lngRes = m_ctlRecord.m_blnModifyOther(objOldValue, objNewValue, m_blnIsControl) ? 1 : 0;

                if (lngRes == 1)
                {
                    m_mthResetOther();
                }
            }

			m_ctlRecord.m_mthUpdateDisplay();

			return lngRes;
		}
		#endregion
		
		#region ��Ӻ������޸Ľ������ʾ�������������ݿ⣩
		private long m_lngSaveNew(string p_strServerTime)
		{
			m_ctlRecord.m_mthAddRecordDate(m_dtpRecordDateTime.Value.Date);

			long lngRes = -1;

			#region old
			//			if(m_rdbSpecialDateType.Checked)
			//			{
			//				lngRes = m_lngSaveNewSpecialDate(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSpecialDate();
			//				}
			//			}
			//			else if(m_rdbEventType.Checked)
			//			{
			//				lngRes = m_lngSaveNewEvent(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetEvent();
			//				}
			//			}
			//			else if(m_rdbPulseType.Checked)
			//			{
			//				int intValue = int.Parse(m_txtPulseValue.Text);
			//				if(intValue > 180 || intValue < 0)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ������0��180֮�䡣");
			//					return -1;
			//				}
			//
			//				lngRes = m_lngSaveNewPulse(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthSyncPluseTime(m_cboPulseTimeFlag.SelectedIndex);
			//
			//					m_mthResetPulse();					
			//				}
			//			}
			//			else if(m_rdbBreathType.Checked)
			//			{
			//				lngRes = m_lngSaveNewBreath(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthSyncPluseTime(m_cboBreathTime.SelectedIndex);
			//
			//					m_mthResetBreath();
			//				}
			//			}
			//			else if(m_rdbTemperatureType.Checked)
			//			{
			//				float fltValue = float.Parse(m_txtTemperatureValue.Text);
			//				if(fltValue > 42f)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
			//					return -1;
			//				}
			//
			//				lngRes = m_lngSaveNewTemperature(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthSyncPluseTime(m_cboTemperatureTimeFlag.SelectedIndex);
			//
			//					m_mthResetTemperature();
			//				}
			//			}
			//			else if(m_rdbDownTemperatureType.Checked)
			//			{
			//				float fltValue = float.Parse(m_txtDownTemperatureValue.Text);
			//				if(fltValue > 42f)
			//				{
			//					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
			//					return -1;
			//				}
			//
			//				lngRes = m_lngSaveNewDownTemperature(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthSyncPluseTime(m_cboDownBaseTime.SelectedIndex);
			//
			//					m_mthResetDownTemperature();
			//				}
			//			}
			//			else if(m_rdbInputType.Checked)
			//			{
			//				lngRes = m_lngSaveNewInput(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetInput();
			//				}
			//			}
			//			else if(m_rdbDejectaType.Checked)
			//			{
			//				lngRes = m_lngSaveNewDejecta(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetDejecta();
			//				}
			//			}
			//			else if(m_rdbPeeType.Checked)
			//			{
			//				lngRes = m_lngSaveNewPee(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPee();
			//				}
			//			}
			//			else if(m_rdbOutStreamType.Checked)
			//			{
			//				lngRes = m_lngSaveNewOutStream(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOutStream();
			//				}
			//			}
			//			else if(m_rdbPressureType.Checked)
			//			{
			//				lngRes = m_lngSaveNewPressure(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPressure();
			//				}
			//			}
			//			else if(m_rdbWeightType.Checked)
			//			{
			//				lngRes = m_lngSaveNewWeight(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetWeight();
			//				}
			//			}
			//			else if(m_rdbSkinTestType.Checked)
			//			{
			//				lngRes = m_lngSaveNewSkinTest(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSkinTest();
			//				}
			//			}
			//			else if(m_rdbOtherType.Checked)
			//			{
			//				lngRes = m_lngSaveNewOther(p_strServerTime);
			//
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOther();
			//				}
			//			}
			#endregion

			if(m_cboEventType.Text.Trim() == "����" || m_cboEventType.Text.Trim() == "����")
			{
				lngRes = m_lngSaveNewSpecialDate(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetSpecialDate();
				}
			}
			if(m_cboEventType.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewEvent(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetEvent();
				}
			}
			if(m_cboPulse.Text.Trim()!="")
			{
				int intValue = int.Parse(m_cboPulse.Text);
				if(intValue > 180 || intValue < 0)
				{
					clsPublicFunction.ShowInformationMessageBox("������ֵ������0��180֮�䡣");
					return -1;
				}

				lngRes = m_lngSaveNewPulse(p_strServerTime);

				if(lngRes == 1)
				{
					//					m_mthSyncPluseTime(m_cboPulseTimeFlag.SelectedIndex);

					m_mthResetPulse();
				}
			}
			if(m_cboBreathValue.Text.Trim()!="")
			{
				lngRes = m_lngSaveNewBreath(p_strServerTime);

				if(lngRes == 1)
				{
					//					m_mthSyncPluseTime(m_cboBreathTime.SelectedIndex);

					m_mthResetBreath();
				}
			}
			if(m_cboTemperature.Text.Trim()!="" && m_cboTemperatureType.SelectedIndex != 3)
			{
				float fltValue = float.Parse(m_cboTemperature.Text);
				if(fltValue > 42f)
				{
					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
					return -1;
				}

				lngRes = m_lngSaveNewTemperature(p_strServerTime);

				if(lngRes == 1)
				{
					//					m_mthSyncPluseTime(m_cboTemperatureTimeFlag.SelectedIndex);

					m_mthResetTemperature();
				}
			}
			if(m_cboTemperatureType.SelectedIndex == 3 && m_cboTemperature.Text.Trim()!="")
			{
				float fltValue = float.Parse(m_cboTemperature.Text);
				if(fltValue > 42f)
				{
					clsPublicFunction.ShowInformationMessageBox("������ֵ����С��42�档");
					return -1;
				}

				lngRes = m_lngSaveNewDownTemperature(p_strServerTime);

				if(lngRes == 1)
				{
					//					m_mthSyncPluseTime(m_cboDownBaseTime.SelectedIndex);

					m_mthResetDownTemperature();
				}
			}
			if(m_cboSkinTestMedicine.Text.Trim() != "" && m_cboSkinBadCount.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewSkinTest(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetSkinTest();
				}
			}
			else if(m_cboSkinTestMedicine.Text.Trim() != "" && m_cboSkinBadCount.Text.Trim() == "")
				{
					clsPublicFunction.ShowInformationMessageBox("����дƤ�Խ����");
					m_cboSkinBadCount.Focus();
					return -1;
				}
			if(m_txtInputValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewInput(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetInput();
				}
			}
			if(m_cboDejectaBeforeTimes.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewDejecta(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetDejecta();
				}
			}
			if(m_cboPeeValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewPee(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetPee();
				}
			}
			if(m_txtOutStreamValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewOutStream(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetOutStream();
				}
			}
			if(m_txtPressureSystolicValue.Text.Trim() != "" && m_txtPressureDiastolicValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewPressure(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetPressure();
				}
			}
			if(m_cboWeightValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewWeight(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetWeight();
				}
			}
			if(m_cboOtherItem.Text.Trim() != "" && m_txtOtherValue.Text.Trim() != "")
			{
				lngRes = m_lngSaveNewOther(p_strServerTime);

				if(lngRes == 1)
				{
					m_mthResetOther();
				}
			}

			m_ctlRecord.m_mthUpdateDisplay();

			return lngRes;
		}
		private long m_lngSaveNewSpecialDate(string p_strServerTime)
		{
			clsThreeMeasureSpecialDate objValue = new clsThreeMeasureSpecialDate();
			objValue.m_dtmSpecialDate = m_dtpRecordDateTime.Value;
			//			objValue.m_blnIsNewStart = m_chkNewSpecialDate.Checked;
			objValue.m_blnIsNewStart = true;
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddSpecialDate(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewEvent(string p_strServerTime)
		{
			clsThreeMeasureEvent objValue = new clsThreeMeasureEvent();
			objValue.m_dtmEventTime = m_dtpRecordDateTime.Value.Date.AddHours(Decimal.ToDouble(m_nmuEventHour.Value)).AddMinutes(Decimal.ToDouble(m_nmuEventMinute.Value));
			objValue.m_enmEventType = (enmThreeMeasureEventType)m_cboEventType.SelectedItem;			
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddEvent(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewPulse(string p_strServerTime)
		{
			clsThreeMeasurePulseValue objValue = new clsThreeMeasurePulseValue();

			//			switch(m_cboPulseTimeFlag.SelectedIndex)
			switch(m_cboTimeFlag.SelectedIndex)
			{
				case 0://4 am
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am4;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am4h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
					}
					break;
				case 1://8 am
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am8;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am8h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
					}
					break;
				case 2://12 am
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am12;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am12h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
					}
					break;
				case 3://4 pm
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm4;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm4h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
					}
					break;
				case 4://8 pm
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm8;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm8h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
					}
					break;
				case 5://12 pm
					if(!m_chkPulseIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm12;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm12h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
					}
					break;
			}

			//			if(m_rdbPulse.Checked)
			if(m_cboPulseType.SelectedIndex == 0)
				objValue.m_enmType = enmThreeMeasurePulseType.����;
			else
				objValue.m_enmType = enmThreeMeasurePulseType.����;
			objValue.m_intValue = int.Parse(m_cboPulse.Text);
			//			objValue.m_blnLineToPreValue = !m_chkPulseNotLineToPre.Checked;
			objValue.m_blnLineToPreValue = true;
			//			try
			//			{
			//				int.Parse(m_cboPulse.Text);
			//			}
			//			catch
			//			{
			//				objValue.m_blnLineToPreValue = false;
			//			}
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddPulseValue(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewBreath(string p_strServerTime)
		{
			clsThreeMeasureBreathValue objValue = new clsThreeMeasureBreathValue();

			//			switch(m_cboBreathTime.SelectedIndex)
			switch(m_cboTimeFlag.SelectedIndex)
			{
				case 0://4 am					
					objValue.m_enmParamTime = enmParamTime.am4;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);					
					break;
				case 1://8 am					
					objValue.m_enmParamTime = enmParamTime.am8;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);					
					break;
				case 2://12 am					
					objValue.m_enmParamTime = enmParamTime.am12;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);					
					break;
				case 3://4 pm					
					objValue.m_enmParamTime = enmParamTime.pm4;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
					break;
				case 4://8 pm
					objValue.m_enmParamTime = enmParamTime.pm8;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
					break;
				case 5://12 pm
					objValue.m_enmParamTime = enmParamTime.pm12;

					objValue.m_dtmBreathTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
					break;
			}

			//			if(m_rdbBreathStartAssistant.Checked)
			if(m_cboBreathValue.Text=="��������")
				objValue.m_enmBreathType = enmThreeMeasureBreathType.��������;
			else if(m_cboBreathValue.Text=="ͣ��������")
				objValue.m_enmBreathType = enmThreeMeasureBreathType.ͣ��������;
			else
			{
				objValue.m_enmBreathType = enmThreeMeasureBreathType.һ��;
				objValue.m_intValue = int.Parse(m_cboBreathValue.Text);
			}
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddBreath(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewTemperature(string p_strServerTime)
		{
			clsThreeMeasureTemperatureValue objValue = new clsThreeMeasureTemperatureValue();
			
			//			switch(m_cboTemperatureTimeFlag.SelectedIndex)
			switch(m_cboTimeFlag.SelectedIndex)
			{
				case 0://4 am
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am4;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am4h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h);
					}
					break;
				case 1://8 am
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am8;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am8h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h);
					}
					break;
				case 2://12 am
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.am12;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.am12h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h);
					}
					break;
				case 3://4 pm
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm4;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm4h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h);
					}
					break;
				case 4://8 pm
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm8;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm8h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h);
					}
					break;
				case 5://12 pm
					if(!m_chkTemperatureIsHalf.Checked)
					{
						objValue.m_enmParamTime = enmParamTime.pm12;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12);
					}
					else
					{
						objValue.m_enmParamTime = enmParamTime.pm12h;

						objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h);
					}
					break;
			}
			
			//			if(m_rdbMouthTemperature.Checked)
			if(m_cboTemperatureType.SelectedIndex==0)
				objValue.m_enmType = enmThreeMeasureTemperatureType.�ڱ��¶�;
			else if(m_cboTemperatureType.SelectedIndex==2)
				objValue.m_enmType = enmThreeMeasureTemperatureType.Ҹ���¶�;
			else if(m_cboTemperatureType.SelectedIndex==1)
				objValue.m_enmType = enmThreeMeasureTemperatureType.�ر��¶�;
			//			objValue.m_blnLineToPreValue = !m_chkTemperatureNotLineToPre.Checked;
			objValue.m_blnLineToPreValue = true;
			objValue.m_fltValue = float.Parse(m_cboTemperature.Text);
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddTemperatureValue(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewDownTemperature(string p_strServerTime)
		{
			clsThreeMeasureTemperaturePhyscalDownValue objValue = new clsThreeMeasureTemperaturePhyscalDownValue();
			objValue.m_dtmValueTime = m_dtpRecordDateTime.Value.Date;
			objValue.m_fltValue = float.Parse(m_cboTemperature.Text);
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			
			DateTime dtmBaseTime = m_dtpRecordDateTime.Value.Date;

			//			switch(m_cboDownBaseTime.SelectedIndex)
			switch(m_cboTimeFlag.SelectedIndex)
			{
				case 0://4 am
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am4);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am4h);
					}
					break;
				case 1://8 am
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am8);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am8h);
					}
					break;
				case 2://12 am
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am12);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.am12h);
					}
					break;
				case 3://4 pm
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm4);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm4h);
					}
					break;
				case 4://8 pm
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm8);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm8h);
					}
					break;
				case 5://12 pm
					if(!m_chkDownBaseTimeIsHalf.Checked)
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm12);
					}
					else
					{
						dtmBaseTime = dtmBaseTime.AddHours((int)enmParamTime.pm12h);
					}
					break;
			}

			m_ctlRecord.m_blnAddPhyscalDownValue(objValue,dtmBaseTime);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewInput(string p_strServerTime)
		{
			clsThreeMeasureInputValue objValue = new clsThreeMeasureInputValue();
			objValue.m_dtmInputDate = m_dtpRecordDateTime.Value;
			objValue.m_fltValue = float.Parse(m_txtInputValue.Text);
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddInput(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewDejecta(string p_strServerTime)
		{
			clsThreeMeasureDejectaValue objValue = new clsThreeMeasureDejectaValue();
			objValue.m_dtmDejectaDate = m_dtpRecordDateTime.Value;
			//			objValue.m_blnCanDejecta = !m_chkCannotDejecta.Checked;
			objValue.m_blnCanDejecta = !(m_cboDejectaBeforeTimes.Text == "*");
			if(objValue.m_blnCanDejecta)
			{				
				objValue.m_blnNeedWeight = m_chkNeedWeight.Checked;
				if(objValue.m_blnNeedWeight)
				{
					objValue.m_fltWeight = float.Parse(m_txtDejectaWeightValue.Text);
				}

				//				objValue.m_blnAfterMoreTimes = m_chkDejectaAfterMoreTimes.Checked;
				string strContent = m_cboDejectaBeforeTimes.Text.Trim();
				objValue.m_blnAfterMoreTimes = strContent.EndsWith("*/E");
				int intIndexOfSpace = strContent.IndexOf(" ");
				int intIndexOfBias = strContent.IndexOf("/");
				if(intIndexOfSpace == -1)
				{
					if(intIndexOfBias == -1)//û�೦
					{
						objValue.m_intBeforeTimes = int.Parse(strContent);
						objValue.m_intClysisTimes = 0;
					}
					else
						objValue.m_intBeforeTimes = 0;

					if(!objValue.m_blnAfterMoreTimes && intIndexOfBias != -1)
					{
						objValue.m_intAfterTimes = int.Parse(strContent.Substring(0,intIndexOfBias));
						objValue.m_intClysisTimes = 1;
					}
				}
				else
				{
					objValue.m_intBeforeTimes = int.Parse(strContent.Substring(0,intIndexOfSpace));
					if(!objValue.m_blnAfterMoreTimes && intIndexOfBias != -1)
					{
						objValue.m_intAfterTimes = int.Parse(strContent.Substring(intIndexOfSpace,intIndexOfBias - intIndexOfSpace));
						objValue.m_intClysisTimes = 1;
					}

				}

				//				if(!objValue.m_blnAfterMoreTimes)
				//				{					
				//					objValue.m_intAfterTimes = Decimal.ToInt32(m_nmuDejectaAfterTimes.Value);
				//				}
				//				objValue.m_intBeforeTimes = Decimal.ToInt32(m_nmuDejectaBeforeTimes.Value);
				//�೦������û�õ�
				//				objValue.m_intClysisTimes = Decimal.ToInt32(m_nmuDejectaClysisTimes.Value);	
			}
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddDejecta(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewPee(string p_strServerTime)
		{
			clsThreeMeasurePeeValue objValue = new clsThreeMeasurePeeValue();
			objValue.m_dtmPeeDate = m_dtpRecordDateTime.Value;
			//			objValue.m_blnIsIrretention = m_chkIsIrretention.Checked;
			objValue.m_blnIsIrretention = m_cboPeeValue.Text == "*";
			if(!objValue.m_blnIsIrretention)
			{
				objValue.m_fltValue = float.Parse(m_cboPeeValue.Text);
			}
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			
			m_ctlRecord.m_blnAddPee(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewOutStream(string p_strServerTime)
		{
			clsThreeMeasureOutStreamValue objValue = new clsThreeMeasureOutStreamValue();
			objValue.m_dtmOutStreamDate = m_dtpRecordDateTime.Value;
			objValue.m_fltValue = float.Parse(m_txtOutStreamValue.Text);
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddOutStream(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewPressure(string p_strServerTime)
		{
			clsThreeMeasurePressureValue objValue = new clsThreeMeasurePressureValue();
			objValue.m_dtmPressureDate = m_dtpRecordDateTime.Value;
			objValue.m_fltDiastolicValue = float.Parse(m_txtPressureDiastolicValue.Text);
			objValue.m_fltSystolicValue = float.Parse(m_txtPressureSystolicValue.Text);
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddPressure(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewWeight(string p_strServerTime)
		{
			clsThreeMeasureWeightValue objValue = new clsThreeMeasureWeightValue();
			objValue.m_dtmWeightDate = m_dtpRecordDateTime.Value;
			try
			{
				objValue.m_enmWeightType = enmThreeMeasureWeightType.һ��;
				objValue.m_fltValue = float.Parse(m_cboWeightValue.Text);
			}
			catch
			{
				if(m_cboWeightValue.Text == "�Դ�")
					objValue.m_enmWeightType = enmThreeMeasureWeightType.�Դ�;
				else
					objValue.m_enmWeightType = enmThreeMeasureWeightType.����;
			}
			//			if(m_rdbWeightNormal.Checked)
			//			{
			//				objValue.m_enmWeightType = enmThreeMeasureWeightType.һ��;
			//				objValue.m_fltValue = float.Parse(m_txtWeightValue.Text);			
			//			}
			//			else if(m_rdbWeightBed.Checked)
			//				objValue.m_enmWeightType = enmThreeMeasureWeightType.�Դ�;
			//			else
			//				objValue.m_enmWeightType = enmThreeMeasureWeightType.����;
			//			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);

			m_ctlRecord.m_blnAddWeight(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewSkinTest(string p_strServerTime)
		{
			clsThreeMeasureSkinTestValue objValue = new clsThreeMeasureSkinTestValue();
			objValue.m_dtmSkinTestDate = m_dtpRecordDateTime.Value;
			objValue.m_strMedicineName = m_cboSkinTestMedicine.Text;
			//			objValue.m_blnIsBad = m_rdbSkinTestBad.Checked;
			objValue.m_blnIsBad = m_cboSkinBadCount.Text.StartsWith("+");
			//			objValue.m_intBadCount = Decimal.ToInt32(m_nmuSkinBadCount.Value);
			objValue.m_intBadCount = m_cboSkinBadCount.Text.Trim().Length;
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			
			m_ctlRecord.m_blnAddSkinTest(objValue);

			m_objSaveValue = objValue;

			return 1;
		}

		private long m_lngSaveNewOther(string p_strServerTime)
		{
			clsThreeMeasureOtherValue objValue = new clsThreeMeasureOtherValue();
			objValue.m_dtmOtherDate = m_dtpRecordDateTime.Value;
			objValue.m_fltOtherValue = float.Parse(m_txtOtherValue.Text);
			objValue.m_strOtherItem = m_cboOtherItem.Text;
			objValue.m_strOtherUnit = m_cboOtherUnit.Text;
			objValue.m_dtmModifyTime = DateTime.Parse(p_strServerTime);
			
			m_ctlRecord.m_blnAddOther(objValue);

			m_objSaveValue = objValue;

			return 1;
		}
		#endregion		

		#region ���ú���
		private void m_mthResetSpecialDate()
		{
			m_dtpRecordDateTime.Tag = null;

			m_chkNewSpecialDate.Checked = true;

			//			m_chkNewSpecialDate.Enabled = false;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetEvent()
		{
			m_dtpRecordDateTime.Tag = null;

			//			m_dtpEventTime.Value = DateTime.Now;
			m_nmuEventHour.Value = DateTime.Now.Hour;
			m_nmuEventMinute.Value = DateTime.Now.Minute;

			//			m_cboEventType.SelectedIndex = 0;
			m_cboEventType.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetPulse()
		{
			m_dtpRecordDateTime.Tag = null;

			m_cboPulseTimeFlag.SelectedIndex = 0;
			m_chkPulseIsHalf.Checked = false;
			m_rdbPulse.Checked = true;
			m_txtPulseValue.Text = "";
			m_chkPulseNotLineToPre.Checked = false;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetBreath()
		{
			m_dtpRecordDateTime.Tag = null;

			m_cboBreathTime.SelectedIndex = 0;
			m_rdbBreathNormal.Checked = true;
			m_txtBreathValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetTemperature()
		{
			m_dtpRecordDateTime.Tag = null;

			m_cboTemperatureTimeFlag.SelectedIndex = 0;
			m_chkTemperatureIsHalf.Checked = false;

			m_rdbArmpitTemperature.Checked = true;
			m_txtTemperatureValue.Text = "";
			m_chkTemperatureNotLineToPre.Checked = false;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetDownTemperature()
		{
			m_dtpRecordDateTime.Tag = null;

			m_cboDownBaseTime.SelectedIndex = 0;
			m_chkDownBaseTimeIsHalf.Checked = false;

			m_txtDownTemperatureValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetInput()
		{
			m_dtpRecordDateTime.Tag = null;

			m_txtInputValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetDejecta()
		{
			m_dtpRecordDateTime.Tag = null;

			m_chkCannotDejecta.Checked = false;
			m_nmuDejectaBeforeTimes.Value = 0;
			m_chkDejectaAfterMoreTimes.Checked = false;
			m_nmuDejectaAfterTimes.Value = 0;
			m_nmuDejectaClysisTimes.Value = 0;
			m_chkNeedWeight.Checked = false;
			m_txtDejectaWeightValue.Text = "";

            m_cboPeeValue.Enabled = true;
            m_txtOutStreamValue.Enabled = true;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetPee()
		{
			m_dtpRecordDateTime.Tag = null;

			m_chkIsIrretention.Checked = false;
			m_txtPeeValue.Text = "";

            m_cboDejectaBeforeTimes.Enabled = true;
            m_chkNeedWeight.Enabled = true;
            m_txtDejectaWeightValue.Enabled = true;
            m_txtOutStreamValue.Enabled = true;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetOutStream()
		{
			m_dtpRecordDateTime.Tag = null;

			m_txtOutStreamValue.Text = "";

            m_cboDejectaBeforeTimes.Enabled = true;
            m_chkNeedWeight.Enabled = true;
            m_txtDejectaWeightValue.Enabled = true;
            m_cboPeeValue.Enabled = true;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetPressure()
		{
			m_dtpRecordDateTime.Tag = null;

			m_txtPressureDiastolicValue.Text = "";
			m_txtPressureSystolicValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetWeight()
		{
			m_dtpRecordDateTime.Tag = null;

			m_rdbWeightNormal.Checked = true;
			m_txtWeightValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetSkinTest()
		{
			m_dtpRecordDateTime.Tag = null;

			m_cboSkinTestMedicine.Text = "";
			m_rdbSkinTestGood.Checked = true;

			m_nmuSkinBadCount.Value = 1;

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetOther()
		{
			m_dtpRecordDateTime.Tag = null;

			//			m_cboOtherItem.SelectedIndex = -1;
			string strOtherName = m_ctlRecord.m_strGetOtherName();
			if(strOtherName != null && strOtherName != "")
			{
				m_cboOtherItem.Text = strOtherName;
				m_cboOtherItem.Enabled = false;
			}
			else
			{
				m_cboOtherItem.Text = "";
				m_cboOtherItem.SelectedIndex = -1;
			}
			m_cboOtherUnit.SelectedIndex = -1;
			m_txtOtherValue.Text = "";

			m_dtpRecordDateTime.Enabled = true;
		}

		private void m_mthResetAll()
		{
			m_hasLastModifyDateInRecord.Clear();
			m_hasOpenDateInRecord.Clear();

			m_ctlRecord.m_mthClearAll();

			m_rdbSpecialDateType.Checked = true;
			m_dtpRecordDateTime.Value = DateTime.Now;

			m_strCurrentOpenDate = "";

			m_mthResetSpecialDate();
			m_mthResetEvent();
			m_mthResetPulse();
			m_mthResetBreath();
			m_mthResetTemperature();
			m_mthResetDownTemperature();
			m_mthResetInput();
			m_mthResetDejecta();
			m_mthResetPee();
			m_mthResetOutStream();
			m_mthResetPressure();
			m_mthResetWeight();
			m_mthResetSkinTest();
			m_mthResetOther();
		}

		private void m_mthClearAll()
		{
			txtInPatientID.Tag = null;

			m_mthClearPatientBaseInfo();

			m_ctlRecord.m_mthClearAll();

			m_mthResetAll();
		}
		#endregion

		private void m_mthRecorTypeCheckedChanged(object sender, System.EventArgs e)
		{
			/*
			 * ���ݽ���Ĳ�ͬѡ����ʾ��ͬ���������ݣ��������ݷ���GroupBox�£�
			 */
			RadioButton rdbSender = (RadioButton)sender;

			GroupBox grbInfo = (GroupBox)rdbSender.Tag;
			grbInfo.Visible = rdbSender.Checked;			
			
			m_dtpRecordDateTime.Tag = null;
			m_dtpRecordDateTime.Enabled = true;
			//			m_dtpRecordDateTime.Value = DateTime.Now;

			if(grbInfo.Equals(m_grbOther))
			{
				string strOtherName = m_ctlRecord.m_strGetOtherName();
				if(strOtherName != null && strOtherName != "")
				{
					m_cboOtherItem.Text = strOtherName;
					m_cboOtherItem.Enabled = false;
				}
				else
				{
					m_cboOtherItem.Text = "";
					m_cboOtherItem.SelectedIndex = -1;
					m_cboOtherItem.Enabled = true;
				}
			}
			
		}

		/// <summary>
		/// ���²�����״̬���Ƿ���Ҫ���ƣ�
		/// </summary>
		/// <param name="dtmParamDate">������ʱ��</param>
		private void m_mthUpdateParamStat(DateTime dtmParamDate)
		{
			if(dtmParamDate.AddHours(6) < DateTime.Now)
			{
				m_blnIsControl = true;
			}
			else
				m_blnIsControl = false;

			m_dtpRecordDateTime.Enabled = false;
		}

		#region ����¼�����ʾ�û�������Ĳ�������Ϣ��
		private void m_ctlRecord_m_evtBreathMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureBreathArg objArg = (clsThreeMeasureBreathArg)e;

			string strTemp = "������¼��\r\n";

			strTemp += objArg.m_objBreath.m_dtmBreathTime.Date.ToString("yyyy-MM-dd 00:00:00")+"\r\n";

			switch(objArg.m_objBreath.m_enmParamTime)
			{
				case enmParamTime.am4:
					strTemp += "4 am\r\n";
					break;
				case enmParamTime.am8:
					strTemp += "8 am\r\n";
					break;
				case enmParamTime.am12:
					strTemp += "12 am\r\n";
					break;
				case enmParamTime.pm4:
					strTemp += "4 pm\r\n";
					break;
				case enmParamTime.pm8:
					strTemp += "8 pm\r\n";
					break;
				case enmParamTime.pm12:
					strTemp += "12 pm\r\n";
					break;
			}

			strTemp	+= objArg.m_objBreath.m_enmBreathType.ToString();

			if(objArg.m_objBreath.m_enmBreathType == enmThreeMeasureBreathType.һ��)
				strTemp += objArg.m_objBreath.m_intValue+"\r\n";

			if(objArg.m_objBreath.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbBreathType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_cboBreathValue});					

					m_dtpRecordDateTime.Tag = objArg.m_objBreath;

					m_dtpRecordDateTime.Value = objArg.m_objBreath.m_dtmBreathTime;
				
					DateTime dtmCreateDate = objArg.m_objBreath.m_dtmBreathTime.Date;

					switch(objArg.m_objBreath.m_enmParamTime)
					{
						case enmParamTime.am4:
							m_cboTimeFlag.SelectedIndex = 0;
							dtmCreateDate = dtmCreateDate.AddHours(4);
							break;
						case enmParamTime.am8:
							m_cboTimeFlag.SelectedIndex = 1;
							dtmCreateDate = dtmCreateDate.AddHours(8);
							break;
						case enmParamTime.am12:
							m_cboTimeFlag.SelectedIndex = 2;
							dtmCreateDate = dtmCreateDate.AddHours(12);
							break;
						case enmParamTime.pm4:
							m_cboTimeFlag.SelectedIndex = 3;
							dtmCreateDate = dtmCreateDate.AddHours(16);
							break;
						case enmParamTime.pm8:
							m_cboTimeFlag.SelectedIndex = 4;
							dtmCreateDate = dtmCreateDate.AddHours(20);
							break;
						case enmParamTime.pm12:
							m_cboTimeFlag.SelectedIndex = 5;
							dtmCreateDate = dtmCreateDate.AddHours(24);
							break;
					}

					switch(objArg.m_objBreath.m_enmBreathType)
					{
						case enmThreeMeasureBreathType.һ��:
							//							m_rdbBreathNormal.Checked = true;
							//							m_txtBreathValue.Text = objArg.m_objBreath.m_intValue.ToString();
							m_cboBreathValue.Text = objArg.m_objBreath.m_intValue.ToString();
							break;
						case enmThreeMeasureBreathType.ͣ��������:
							//							m_rdbBreathStopAssistant.Checked = true;
							//							m_txtBreathValue.Text = "";
							m_cboBreathValue.Text = "ͣ��������";
							break;
						case enmThreeMeasureBreathType.��������:
							//							m_rdbBreathStartAssistant.Checked = true;
							//							m_txtBreathValue.Text = "";
							m_cboBreathValue.Text = "��������";
							break;
					}					

					m_mthUpdateParamStat(objArg.m_objBreath.m_dtmModifyTime);

					m_cboBreathValue.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objBreath.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtDateRecordMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureDateRecordArg objArg = (clsThreeMeasureDateRecordArg)e;

			string strTemp = ""+objArg.m_objRecord.m_dtmRecordDate.ToString()+"\r\n�Ƿ�ɾ�������ں͸������µ����м�¼��";

			if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
			{
				clsPatient objPatient = m_objBaseCurrentPatient;
	
				if(objPatient == null)
				{

					clsPublicFunction.ShowInformationMessageBox("����ѡ���ˡ�");
					return;
				}

				string strLastModifyDate = (string)m_hasLastModifyDateInRecord[objArg.m_objRecord.m_dtmRecordDate];
				string strOpenDate = (string)m_hasOpenDateInRecord[objArg.m_objRecord.m_dtmRecordDate];

				if(strLastModifyDate == null)
				{
					//��ʾ������
					m_mthAsk_Reload(objPatient);
					return;
				}

				bool blnIsLast;
				bool blnIsDelete;
				string strChangedUserID = null;
				string strChangedDate = null;
					
				long lngRes = m_objRecordDomain.m_lngCheckLastModifyDate(objPatient.m_StrInPatientID,m_strInPatientDate,strOpenDate,strLastModifyDate,out blnIsLast,out blnIsDelete,out strChangedUserID,out strChangedDate);

				if(lngRes <= 0)
				{
					m_mthShowDBError();
					return;
				}

				if(blnIsDelete)
				{
					m_mthShowRecordDeleted(strChangedUserID,strChangedDate);
					return;
				}

				if(!blnIsLast)
				{
					if(m_bolShowRecordModified(strChangedUserID,strChangedDate))
					{
						m_mthSetPatientInfo(objPatient);
					}
					else
					{
						m_ctlRecord.m_blnSetXml(m_objOleXml);
					}	
					return;
				}

				lngRes = m_objRecordDomain.m_lngDeleteRecord(objPatient.m_StrInPatientID,m_strInPatientDate,strOpenDate,m_objCurrentContext.m_ObjEmployee.m_StrEmployeeID);

				if(lngRes <= 0)
				{
					m_mthShowDBError();
				}
				else
				{
					m_hasLastModifyDateInRecord.Remove(objArg.m_objRecord.m_dtmRecordDate);
					m_hasOpenDateInRecord.Remove(objArg.m_objRecord.m_dtmRecordDate);

					m_ctlRecord.m_blnDeleteRecordDate(objArg.m_objRecord);

					m_ctlRecord.m_mthUpdateDisplay();
				}
			}			
		}

		private void m_ctlRecord_m_evtDejectaMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureDejectaArg objArg = (clsThreeMeasureDejectaArg)e;

			string strTemp = "����¼��\r\n"+objArg.m_objDejecta.m_dtmDejectaDate.ToString()+"\r\n";

			if(!objArg.m_objDejecta.m_blnCanDejecta)
				strTemp += "���ʧ����ٸء�\r\n";
			else
			{
				strTemp += "��������"+objArg.m_objDejecta.m_intBeforeTimes
					+"\r\n�೦������"+objArg.m_objDejecta.m_intClysisTimes;

				if(!objArg.m_objDejecta.m_blnAfterMoreTimes)
					strTemp += "\r\n�೦���������"+objArg.m_objDejecta.m_intAfterTimes;
				else
					strTemp += "\r\n�೦������������";

				if(objArg.m_objDejecta.m_blnNeedWeight)
				{
					strTemp += "\r\n�������"+objArg.m_objDejecta.m_fltWeight+"��\r\n";
				}
			}

			if(objArg.m_objDejecta.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbDejectaType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{ groupBox3, m_cboDejectaBeforeTimes,m_chkNeedWeight,m_txtDejectaWeightValue});
                    m_cboPeeValue.Enabled = false;
                    m_txtOutStreamValue.Enabled = false;

					m_dtpRecordDateTime.Tag = objArg.m_objDejecta;

					m_dtpRecordDateTime.Value = objArg.m_objDejecta.m_dtmDejectaDate;

					if(!objArg.m_objDejecta.m_blnCanDejecta)
					{
						m_cboDejectaBeforeTimes.Text = "*";
						return;
					}

					System.Text.StringBuilder sb = new System.Text.StringBuilder("");
					
					if(objArg.m_objDejecta.m_intBeforeTimes > 0)
					{
						sb.Append(objArg.m_objDejecta.m_intBeforeTimes);
						sb.Append(" ");
					}

					if(objArg.m_objDejecta.m_intClysisTimes > 0)//����й೦
					{
						if(objArg.m_objDejecta.m_blnAfterMoreTimes)
						{
							sb.Append("*/E");
						}
						else
						{
							sb.Append(objArg.m_objDejecta.m_intAfterTimes);
							sb.Append("/E");
						}
					}

					m_cboDejectaBeforeTimes.Text = sb.ToString().Trim();
				
					//					m_nmuDejectaBeforeTimes.Value = objArg.m_objDejecta.m_intBeforeTimes;
					//					m_nmuDejectaClysisTimes.Value = objArg.m_objDejecta.m_intClysisTimes;				

					//					if(objArg.m_objDejecta.m_blnAfterMoreTimes)
					//						m_nmuDejectaAfterTimes.Value = 0;
					//					else
					//						m_nmuDejectaAfterTimes.Value = objArg.m_objDejecta.m_intAfterTimes;
					//					m_chkDejectaAfterMoreTimes.Checked = objArg.m_objDejecta.m_blnAfterMoreTimes;

					if(objArg.m_objDejecta.m_blnNeedWeight)
						m_txtDejectaWeightValue.Text = objArg.m_objDejecta.m_fltWeight.ToString("0.00");
					else
						m_txtDejectaWeightValue.Text = "";
					m_chkNeedWeight.Checked = objArg.m_objDejecta.m_blnNeedWeight;

					//					m_chkCannotDejecta.Checked = !objArg.m_objDejecta.m_blnCanDejecta;

					m_mthUpdateParamStat(objArg.m_objDejecta.m_dtmModifyTime);

					m_cboDejectaBeforeTimes.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objDejecta.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					
				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtEventMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureEventArg objArg = (clsThreeMeasureEventArg)e;

			for(int i=0;i<objArg.m_objEventArr.Length;i++)
			{
				string strTemp = "�¼���¼��\r\n";

				strTemp += objArg.m_objEventArr[i].m_dtmEventTime+"\r\n"
					+objArg.m_objEventArr[i].m_enmEventType.ToString()+"\r\n";

				if(objArg.m_objEventArr[i].m_objDeleteInfo == null)
				{
					strTemp += "�Ƿ��޸Ļ�ɾ����";

					if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
					{
						//						m_rdbEventType.Checked = true;

						m_mthSetSomeControlEnable(new Control[]{m_cboEventType,m_nmuEventHour,m_nmuEventMinute});

						m_dtpRecordDateTime.Tag = objArg.m_objEventArr[i];

						m_dtpRecordDateTime.Value = objArg.m_objEventArr[i].m_dtmEventTime;

						//						m_dtpEventTime.Value = objArg.m_objEventArr[i].m_dtmEventTime;
						m_nmuEventHour.Value = objArg.m_objEventArr[i].m_dtmEventTime.Hour;
						m_nmuEventMinute.Value = objArg.m_objEventArr[i].m_dtmEventTime.Minute;

						//						m_cboEventType.SelectedItem = objArg.m_objEventArr[i].m_enmEventType;
						m_cboEventType.Text = objArg.m_objEventArr[i].m_enmEventType.ToString();
						
						m_mthUpdateParamStat(objArg.m_objEventArr[i].m_dtmModifyTime);
						m_cboEventType.Focus();
						break;
					}
				}
				else
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objEventArr[i].m_objDeleteInfo;

					strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
						+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
						
					clsPublicFunction.ShowInformationMessageBox(strTemp);
				}
			}					
		}

		private void m_ctlRecord_m_evtInputMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureInputArg objArg = (clsThreeMeasureInputArg)e;

			string strTemp = "����Һ����\r\n"+objArg.m_objInput.m_dtmInputDate.ToString()+"\r\n"
				+objArg.m_objInput.m_fltValue.ToString("0.00")+"\r\n";

			if(objArg.m_objInput.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbInputType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_txtInputValue});

					m_dtpRecordDateTime.Tag = objArg.m_objInput;

					m_dtpRecordDateTime.Value = objArg.m_objInput.m_dtmInputDate;

					m_txtInputValue.Text = objArg.m_objInput.m_fltValue.ToString("0.00");

					m_mthUpdateParamStat(objArg.m_objInput.m_dtmModifyTime);
					m_txtInputValue.Focus();
				}	
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objInput.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					
				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}		
		}

		private void m_ctlRecord_m_evtOtherMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureOtherArg objArg = (clsThreeMeasureOtherArg)e;

			string strTemp = "������\r\n"+objArg.m_objOther.m_dtmOtherDate.ToString()+"\r\n"
				+objArg.m_objOther.m_strOtherItem+": "+objArg.m_objOther.m_fltOtherValue.ToString("0.00")
				+objArg.m_objOther.m_strOtherUnit+"\r\n";	
	
			if(objArg.m_objOther.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbOtherType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_cboOtherItem,m_txtOtherValue});

					m_dtpRecordDateTime.Tag = objArg.m_objOther;

					m_dtpRecordDateTime.Value = objArg.m_objOther.m_dtmOtherDate;

					string strOtherName = m_ctlRecord.m_strGetOtherName();
					if(strOtherName != null && strOtherName != "")
					{
						m_cboOtherItem.Text = strOtherName;
						m_cboOtherItem.Enabled = false;
					}
					else
					{
						m_cboOtherItem.Text = objArg.m_objOther.m_strOtherItem;
					}
					m_cboOtherUnit.Text = objArg.m_objOther.m_strOtherUnit;
					m_txtOtherValue.Text = objArg.m_objOther.m_fltOtherValue.ToString("0.00");

					m_mthUpdateParamStat(objArg.m_objOther.m_dtmModifyTime);
					m_txtOtherValue.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objOther.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					
				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtOutStreamMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureOutStreamArg objArg = (clsThreeMeasureOutStreamArg)e;

			string strTemp = "��������\r\n"+objArg.m_objOutStream.m_dtmOutStreamDate.ToString()+"\r\n"
				+objArg.m_objOutStream.m_fltValue.ToString("0.00")+"\r\n";

			if(objArg.m_objOutStream.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbOutStreamType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{ groupBox3, m_txtOutStreamValue});
                    
                    m_cboDejectaBeforeTimes.Enabled = false;
                    m_chkNeedWeight.Enabled = false;
                    m_txtDejectaWeightValue.Enabled = false;
                    m_cboPeeValue.Enabled = false;
                    


					m_dtpRecordDateTime.Tag = objArg.m_objOutStream;

					m_dtpRecordDateTime.Value = objArg.m_objOutStream.m_dtmOutStreamDate;

					m_txtOutStreamValue.Text = objArg.m_objOutStream.m_fltValue.ToString("0.00");

					m_mthUpdateParamStat(objArg.m_objOutStream.m_dtmModifyTime);
					m_txtOutStreamValue.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objOutStream.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					 

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtPeeMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasurePeeArg objArg = (clsThreeMeasurePeeArg)e;

			string strTemp = "������\r\n"+objArg.m_objPee.m_dtmPeeDate.ToString()+"\r\n";
			if(!objArg.m_objPee.m_blnIsIrretention)
				strTemp += objArg.m_objPee.m_fltValue.ToString("0.00")+"\r\n";
			else
				strTemp += "С��ʧ����\r\n";

			if(objArg.m_objPee.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";
			
				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbPeeType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{ groupBox3, m_cboPeeValue});

                    m_cboDejectaBeforeTimes.Enabled = false;
                    m_chkNeedWeight.Enabled = false;
                    m_txtDejectaWeightValue.Enabled = false;
                    m_txtOutStreamValue.Enabled = false;

					m_dtpRecordDateTime.Tag = objArg.m_objPee;

					m_dtpRecordDateTime.Value = objArg.m_objPee.m_dtmPeeDate;

					if(objArg.m_objPee.m_blnIsIrretention)
						m_cboPeeValue.Text = "*";
					else
						m_cboPeeValue.Text = objArg.m_objPee.m_fltValue.ToString("0.00");
					//					m_chkIsIrretention.Checked = objArg.m_objPee.m_blnIsIrretention;

					m_mthUpdateParamStat(objArg.m_objPee.m_dtmModifyTime);
					m_cboPeeValue.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objPee.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName
					;

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtPressureMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasurePressureArg objArg = (clsThreeMeasurePressureArg)e;

			string strTemp = "Ѫѹ��\r\n"+objArg.m_objPressure.m_dtmPressureDate.ToString()
				+"\r\n����ѹ��"+objArg.m_objPressure.m_fltSystolicValue.ToString("0")
				+"\r\n����ѹ��"+objArg.m_objPressure.m_fltDiastolicValue.ToString("0")+"\r\n";

			if(objArg.m_objPressure.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbPressureType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_txtPressureDiastolicValue,m_txtPressureSystolicValue});

					m_dtpRecordDateTime.Tag = objArg;

					m_dtpRecordDateTime.Value = objArg.m_objPressure.m_dtmPressureDate;

					m_txtPressureDiastolicValue.Text = objArg.m_objPressure.m_fltDiastolicValue.ToString("0");
					m_txtPressureSystolicValue.Text = objArg.m_objPressure.m_fltSystolicValue.ToString("0");

					m_mthUpdateParamStat(objArg.m_objPressure.m_dtmModifyTime);
					m_txtPressureSystolicValue.Focus();
				}
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objPressure.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtSkinTestMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureSkinTestArg objArg = (clsThreeMeasureSkinTestArg)e;

			string strTemp = "Ƥ�ԣ�\r\n"+objArg.m_objSkinTest.m_dtmSkinTestDate.ToString()
				+"\r\n"+objArg.m_objSkinTest.m_strMedicineName;

			if(objArg.m_objSkinTest.m_blnIsBad)
			{
				strTemp += "(";
				for(int i=0;i<objArg.m_objSkinTest.m_intBadCount;i++)
				{
					strTemp += "+";
				}
				strTemp += ")\r\n";
			}
			else
				strTemp += "(-)\r\n";

			if(objArg.m_objSkinTest.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbSkinTestType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_cboSkinTestMedicine,m_cboSkinBadCount});

					m_dtpRecordDateTime.Tag = objArg.m_objSkinTest;

					m_dtpRecordDateTime.Value = objArg.m_objSkinTest.m_dtmSkinTestDate;

					m_cboSkinTestMedicine.Text = objArg.m_objSkinTest.m_strMedicineName;

					string strSymbol = (objArg.m_objSkinTest.m_blnIsBad) ? "+" : "-";
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					for(int i = 0; i < objArg.m_objSkinTest.m_intBadCount; i++)
					{
						sb.Append(strSymbol);
					}
					m_cboSkinBadCount.Text = sb.ToString();

					//					if(objArg.m_objSkinTest.m_blnIsBad)
					//						m_rdbSkinTestBad.Checked = true;
					//					else
					//						m_rdbSkinTestGood.Checked = true;
					//
					//					m_nmuSkinBadCount.Value = objArg.m_objSkinTest.m_intBadCount;

					m_mthUpdateParamStat(objArg.m_objSkinTest.m_dtmModifyTime);
					m_cboSkinBadCount.Focus();
				}	
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objSkinTest.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName ;
					

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}

		private void m_ctlRecord_m_evtPulseMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasurePulseArg objArg = (clsThreeMeasurePulseArg)e;

			for(int i=0;i<objArg.m_objPulseValueArr.Length;i++)
			{
				string strTemp = "������\r\n";

				strTemp += objArg.m_objPulseValueArr[i].m_dtmValueTime.Date.ToString("yyyy-MM-dd 00:00:00")+"\r\n";

				switch(objArg.m_objPulseValueArr[i].m_enmParamTime)
				{
					case enmParamTime.am4:
						strTemp += "4 am\r\n";
						break;
					case enmParamTime.am8:
						strTemp += "8 am\r\n";
						break;
					case enmParamTime.am12:
						strTemp += "12 am\r\n";
						break;
					case enmParamTime.pm4:
						strTemp += "4 pm\r\n";
						break;
					case enmParamTime.pm8:
						strTemp += "8 pm\r\n";
						break;
					case enmParamTime.pm12:
						strTemp += "12 pm\r\n";
						break;
					case enmParamTime.am4h:
						strTemp += "4 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.am8h:
						strTemp += "8 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.am12h:
						strTemp += "12 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm4h:
						strTemp += "4 pm��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm8h:
						strTemp += "8 pm��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm12h:
						strTemp += "12 pm��ʹ�ð�Сʱ\r\n";
						break;
				}

				strTemp	+= objArg.m_objPulseValueArr[i].m_enmType.ToString()+"\r\n"
					+objArg.m_objPulseValueArr[i].m_intValue+"\r\n";

				if(!objArg.m_objPulseValueArr[i].m_blnLineToPreValue)
					strTemp += "������ٷ��أ�����ǰһ�μ�¼������\r\n";
				
				if(objArg.m_objPulseValueArr[i].m_objDeleteInfo == null)
				{
					strTemp += "�Ƿ��޸Ļ�ɾ����";

					if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
					{
						//						m_rdbPulseType.Checked = true;

						m_mthSetSomeControlEnable(new Control[]{m_cboPulseType,m_cboPulse,m_chkPulseIsHalf});

						m_dtpRecordDateTime.Tag = objArg.m_objPulseValueArr[i];

						m_dtpRecordDateTime.Value = objArg.m_objPulseValueArr[i].m_dtmValueTime;

						DateTime dtmCreateDate = objArg.m_objPulseValueArr[i].m_dtmValueTime.Date;

						switch(objArg.m_objPulseValueArr[i].m_enmParamTime)
						{
							case enmParamTime.am4:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 0;
								dtmCreateDate = dtmCreateDate.AddHours(4);
								break;
							case enmParamTime.am8:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 1;
								dtmCreateDate = dtmCreateDate.AddHours(8);
								break;
							case enmParamTime.am12:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 2;
								dtmCreateDate = dtmCreateDate.AddHours(12);
								break;
							case enmParamTime.pm4:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 3;
								dtmCreateDate = dtmCreateDate.AddHours(16);
								break;
							case enmParamTime.pm8:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 4;
								dtmCreateDate = dtmCreateDate.AddHours(20);
								break;
							case enmParamTime.pm12:
								m_chkPulseIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 5;
								dtmCreateDate = dtmCreateDate.AddHours(24);
								break;
							case enmParamTime.am4h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 0;
								dtmCreateDate = dtmCreateDate.AddHours(4).AddMinutes(30);
								break;
							case enmParamTime.am8h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 1;
								dtmCreateDate = dtmCreateDate.AddHours(8).AddMinutes(30);
								break;
							case enmParamTime.am12h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 2;
								dtmCreateDate = dtmCreateDate.AddHours(12).AddMinutes(30);
								break;
							case enmParamTime.pm4h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 3;
								dtmCreateDate = dtmCreateDate.AddHours(16).AddMinutes(30);
								break;
							case enmParamTime.pm8h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 4;
								dtmCreateDate = dtmCreateDate.AddHours(20).AddMinutes(30);
								break;
							case enmParamTime.pm12h:
								m_chkPulseIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 5;
								dtmCreateDate = dtmCreateDate.AddHours(24).AddMinutes(30);
								break;
						}

						switch(objArg.m_objPulseValueArr[i].m_enmType)
						{
							case enmThreeMeasurePulseType.����:
								//								m_rdbHeartRate.Checked = true;
								//								m_cboPulseType.SelectedIndex = 0;
								m_cboPulseType.Text = "����:";
								break;
							case enmThreeMeasurePulseType.����:
								//								m_rdbPulse.Checked = true;
								//								m_cboPulseType.SelectedIndex = 1;
								m_cboPulseType.Text = "����:";
								break;
						}
						m_cboPulse.Text = objArg.m_objPulseValueArr[i].m_intValue.ToString();
						//						m_chkPulseNotLineToPre.Checked = !objArg.m_objPulseValueArr[i].m_blnLineToPreValue;
					
						m_mthUpdateParamStat(objArg.m_objPulseValueArr[i].m_dtmModifyTime);
						m_cboPulse.Focus();
						break;
					}
				}
				else
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objPulseValueArr[i].m_objDeleteInfo;

					strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
						+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
						

					clsPublicFunction.ShowInformationMessageBox(strTemp);
				}

			}			
		}

		private void m_ctlRecord_m_evtSpecialDateMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureSpecialDateArg objArg = (clsThreeMeasureSpecialDateArg)e;

			string strTemp = "������������ڣ�\r\n"+objArg.m_objSpecialDate.m_dtmSpecialDate.ToString()+"\r\n";
			strTemp += objArg.m_objSpecialDate.m_blnIsNewStart?"�����������ڡ�\r\n�Ƿ��޸Ļ�ɾ����":"�������������ڡ�\r\n�Ƿ��޸Ļ�ɾ����";

			if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
			{
				//				m_rdbSpecialDateType.Checked = true;

				m_mthSetSomeControlEnable(new Control[]{m_cboEventType});

				m_dtpRecordDateTime.Tag = objArg.m_objSpecialDate;

				m_dtpRecordDateTime.Value = objArg.m_objSpecialDate.m_dtmSpecialDate;

				//				m_chkNewSpecialDate.Checked = objArg.m_objSpecialDate.m_blnIsNewStart;
				//				m_chkNewSpecialDate.Enabled = true;

				if(objArg.m_objSpecialDate.m_blnIsNewStart)
				{
					m_cboEventType.Text = "����";
				}

				m_mthUpdateParamStat(objArg.m_objSpecialDate.m_dtmModifyTime);
			}	
		}

		private void m_ctlRecord_m_evtTemperatureMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureTemperatureArg objArg = (clsThreeMeasureTemperatureArg)e;

			string strTemp = "";

			for(int i=0;i<objArg.m_objTemperatureArr.Length;i++)
			{
				strTemp = "���£�\r\n";

				strTemp += objArg.m_objTemperatureArr[i].m_dtmValueTime.Date.ToString("yyyy-MM-dd 00:00:00")+"\r\n";

				switch(objArg.m_objTemperatureArr[i].m_enmParamTime)
				{
					case enmParamTime.am4:
						strTemp += "4 am\r\n";
						break;
					case enmParamTime.am8:
						strTemp += "8 am\r\n";
						break;
					case enmParamTime.am12:
						strTemp += "12 am\r\n";
						break;
					case enmParamTime.pm4:
						strTemp += "4 pm\r\n";
						break;
					case enmParamTime.pm8:
						strTemp += "8 pm\r\n";
						break;
					case enmParamTime.pm12:
						strTemp += "12 pm\r\n";
						break;
					case enmParamTime.am4h:
						strTemp += "4 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.am8h:
						strTemp += "8 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.am12h:
						strTemp += "12 am��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm4h:
						strTemp += "4 pm��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm8h:
						strTemp += "8 pm��ʹ�ð�Сʱ\r\n";
						break;
					case enmParamTime.pm12h:
						strTemp += "12 pm��ʹ�ð�Сʱ\r\n";
						break;
				}
				
				strTemp += objArg.m_objTemperatureArr[i].m_enmType.ToString()+"\r\n"
					+objArg.m_objTemperatureArr[i].m_fltValue.ToString("0.00")+"\r\n";

				if(!objArg.m_objTemperatureArr[i].m_blnLineToPreValue)
					strTemp += "������ٷ��أ�����ǰһ�μ�¼������\r\n";
				
				if(objArg.m_objTemperatureArr[i].m_objDeleteInfo == null)
				{
					strTemp += "�Ƿ��޸Ļ�ɾ����";

					DateTime dtmCreateDate = objArg.m_objTemperatureArr[i].m_dtmValueTime.Date;

					if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
					{
						//						m_rdbTemperatureType.Checked = true;

						m_mthSetSomeControlEnable(new Control[]{m_cboTemperatureType,m_cboTemperature,m_chkTemperatureIsHalf});

						m_dtpRecordDateTime.Tag = objArg.m_objTemperatureArr[i];

						m_dtpRecordDateTime.Value = objArg.m_objTemperatureArr[i].m_dtmValueTime;

						switch(objArg.m_objTemperatureArr[i].m_enmParamTime)
						{
							case enmParamTime.am4:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 0;
								dtmCreateDate = dtmCreateDate.AddHours(4);
								break;
							case enmParamTime.am8:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 1;
								dtmCreateDate = dtmCreateDate.AddHours(8);
								break;
							case enmParamTime.am12:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 2;
								dtmCreateDate = dtmCreateDate.AddHours(12);
								break;
							case enmParamTime.pm4:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 3;
								dtmCreateDate = dtmCreateDate.AddHours(16);
								break;
							case enmParamTime.pm8:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 4;
								dtmCreateDate = dtmCreateDate.AddHours(20);
								break;
							case enmParamTime.pm12:
								m_chkTemperatureIsHalf.Checked = false;
								m_cboTimeFlag.SelectedIndex = 5;
								dtmCreateDate = dtmCreateDate.AddHours(24);
								break;
							case enmParamTime.am4h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 0;
								dtmCreateDate = dtmCreateDate.AddHours(4).AddMinutes(30);
								break;
							case enmParamTime.am8h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 1;
								dtmCreateDate = dtmCreateDate.AddHours(8).AddMinutes(30);
								break;
							case enmParamTime.am12h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 2;
								dtmCreateDate = dtmCreateDate.AddHours(12).AddMinutes(30);
								break;
							case enmParamTime.pm4h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 3;
								dtmCreateDate = dtmCreateDate.AddHours(16).AddMinutes(30);
								break;
							case enmParamTime.pm8h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 4;
								dtmCreateDate = dtmCreateDate.AddHours(20).AddMinutes(30);
								break;
							case enmParamTime.pm12h:
								m_chkTemperatureIsHalf.Checked = true;
								m_cboTimeFlag.SelectedIndex = 5;
								dtmCreateDate = dtmCreateDate.AddHours(20).AddMinutes(30);
								break;
						}

						switch(objArg.m_objTemperatureArr[i].m_enmType)
						{
							case enmThreeMeasureTemperatureType.�ڱ��¶�:
								//								m_rdbMouthTemperature.Checked = true;
								//								m_cboTemperatureType.SelectedIndex = 0;
								m_cboTemperatureType.Text = "�ڱ�:";
								break;
							case enmThreeMeasureTemperatureType.�ر��¶�:
								//								m_rdbAnusTemperature.Checked = true;
								//								m_cboTemperatureType.SelectedIndex = 1;
								m_cboTemperatureType.Text = "�ر�:";
								break;
							case enmThreeMeasureTemperatureType.Ҹ���¶�:
								//								m_rdbArmpitTemperature.Checked = true;
								//								m_cboTemperatureType.SelectedIndex = 2;
								m_cboTemperatureType.Text = "Ҹ��:";
								break;
                        }
                        m_cboTemperatureType.Enabled = true;
						m_cboTemperature.Text = objArg.m_objTemperatureArr[i].m_fltValue.ToString("0.00");
						//						m_chkTemperatureNotLineToPre.Checked = !objArg.m_objTemperatureArr[i].m_blnLineToPreValue;
					
						m_mthUpdateParamStat(objArg.m_objTemperatureArr[i].m_dtmModifyTime);
						m_cboTemperature.Focus();
						break;
					}
				}
				else
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objTemperatureArr[i].m_objDeleteInfo;

					strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
						+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
						

					clsPublicFunction.ShowInformationMessageBox(strTemp);
				}
				
				if(objArg.m_objTemperatureArr[i].m_arlPhyscalDownValue.Count > 0)
				{
					if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox("��"+objArg.m_objTemperatureArr[i].m_arlPhyscalDownValue.Count+"�������¼�¼���Ƿ���ʾ��"))
					{
						for(int j2=0;j2<objArg.m_objTemperatureArr[i].m_arlPhyscalDownValue.Count;j2++)
						{
							clsThreeMeasureTemperaturePhyscalDownValue objDownValue = (clsThreeMeasureTemperaturePhyscalDownValue)objArg.m_objTemperatureArr[i].m_arlPhyscalDownValue[j2];
					
							strTemp = "�����º�\r\n"+objDownValue.m_dtmValueTime.ToString()+"\r\n"
								+objDownValue.m_fltValue.ToString("0.00")+"\r\n�Ƿ��޸Ļ�ɾ����";						

							if(objDownValue.m_objDeleteInfo == null)
							{
                                //strTemp += "�Ƿ��޸Ļ�ɾ��";

								if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
								{
									//									m_rdbDownTemperatureType.Checked = true;

									m_dtpRecordDateTime.Tag = objDownValue;
									m_objBaseTemperature = objArg.m_objTemperatureArr[i];

									m_dtpRecordDateTime.Value = objDownValue.m_dtmValueTime.Date;

									switch(objArg.m_objTemperatureArr[i].m_enmParamTime)
									{
										case enmParamTime.am4:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 0;
											break;
										case enmParamTime.am8:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 1;
											break;
										case enmParamTime.am12:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 2;
											break;
										case enmParamTime.pm4:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 3;
											break;
										case enmParamTime.pm8:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 4;
											break;
										case enmParamTime.pm12:
											m_chkDownBaseTimeIsHalf.Checked = false;
											m_cboTimeFlag.SelectedIndex = 5;
											break;
										case enmParamTime.am4h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 0;
											break;
										case enmParamTime.am8h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 1;
											break;
										case enmParamTime.am12h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 2;
											break;
										case enmParamTime.pm4h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 3;
											break;
										case enmParamTime.pm8h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 4;
											break;
										case enmParamTime.pm12h:
											m_chkDownBaseTimeIsHalf.Checked = true;
											m_cboTimeFlag.SelectedIndex = 5;
											break;
									}

									m_cboTemperature.Text = objDownValue.m_fltValue.ToString("0.00");

                                    m_cboTemperatureType.Text = "����:";
                                    m_cboTemperatureType.Enabled = false;
									m_mthUpdateParamStat(objDownValue.m_dtmModifyTime);
									return;
								}
							}
							else
							{
								clsThreeMeasureDeleteInfo objDownDeleteInfo = objDownValue.m_objDeleteInfo;

								strTemp += "\r\n\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDownDeleteInfo.m_dtmDeleteTime.ToString()
									+ "\r\nɾ���û�����: "+objDownDeleteInfo.m_strUserName;
									

								clsPublicFunction.ShowInformationMessageBox(strTemp);
							}
						}
					}
				}
			}			
		}

		private void m_ctlRecord_m_evtWeightMouseDown(object sender, System.EventArgs e)
		{
			clsThreeMeasureWeightArg objArg = (clsThreeMeasureWeightArg)e;

			string strTemp = "���أ�\r\n"+objArg.m_objWeight.m_dtmWeightDate.ToString()+"\r\n";
			if(objArg.m_objWeight.m_enmWeightType == enmThreeMeasureWeightType.һ��)
				strTemp += objArg.m_objWeight.m_fltValue.ToString("0.00")+"\r\n";
			else
				strTemp += objArg.m_objWeight.m_enmWeightType.ToString()+"\r\n";

			if(objArg.m_objWeight.m_objDeleteInfo == null)
			{
				strTemp += "�Ƿ��޸Ļ�ɾ����";

				if(DialogResult.Yes == clsPublicFunction.ShowQuestionMessageBox(strTemp))
				{
					//					m_rdbWeightType.Checked = true;

					m_mthSetSomeControlEnable(new Control[]{m_cboWeightValue});

					m_dtpRecordDateTime.Tag = objArg.m_objWeight;

					m_dtpRecordDateTime.Value = objArg.m_objWeight.m_dtmWeightDate;

					switch(objArg.m_objWeight.m_enmWeightType)
					{
						case enmThreeMeasureWeightType.һ��:
							//							m_rdbWeightNormal.Checked = true;
							m_cboWeightValue.Text = objArg.m_objWeight.m_fltValue.ToString("0.00");
							break;
						case enmThreeMeasureWeightType.�Դ�:
							//							m_rdbWeightBed.Checked = true;
							//							m_txtWeightValue.Text = "";
							m_cboWeightValue.Text = "�Դ�";
							break;
						case enmThreeMeasureWeightType.����:
							//							m_rdbWeightCar.Checked = true;
							//							m_txtWeightValue.Text = "";
							m_cboWeightValue.Text = "����";
							break;
					}		
		
					m_mthUpdateParamStat(objArg.m_objWeight.m_dtmModifyTime);
					m_cboWeightValue.Focus();
				}	
			}
			else
			{
				clsThreeMeasureDeleteInfo objDeleteInfo = objArg.m_objWeight.m_objDeleteInfo;

				strTemp += "\r\n��¼�ѱ��޸Ļ�ɾ����\r\nɾ������: "+objDeleteInfo.m_dtmDeleteTime.ToString()
					+ "\r\nɾ���û�����: "+objDeleteInfo.m_strUserName;
					

				clsPublicFunction.ShowInformationMessageBox(strTemp);
			}
		}
		#endregion

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			m_lngDelete();
			//			m_mthClear();
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			m_mthClearAll();
		}

		private void m_mthKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.F2:
					try
					{
						m_lngSave();
					}
					catch
					{
						clsPublicFunction.ShowInformationMessageBox("��������Ч����ֵ��");
						this.Cursor = Cursors.Default;
					}
					break;
				case Keys.F3:
					m_lngDelete();
					break;
				case Keys.F4:
					m_lngPrint();
					break;
				case Keys.F5:
					m_mthClearAll();					
					break;
				case Keys.Enter:
					//					if(((Control)sender).GetType().Name.Equals("RadioButton") && ((Control)sender).Parent.Name.Equals("grbRecordType"))
					//						m_rdbCur = (RadioButton)sender;
					Control ctl = (Control)sender;
					if(ctl.AccessibleName!=null && ctl.AccessibleName.Equals("save"))
						m_cmdSave_Click(null,EventArgs.Empty);
					else if(!ctl.GetType().Name.Equals("Button"))
						SendKeys.Send("{tab}");
					break;
			}
		}

		#region override
		protected override bool m_BlnCanTextChanged
		{
			get
			{
				return m_blnCanTextChanged;
			}
		}

		protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
		{
			if(p_objSelectedPatient == null)
				return;

            this.m_ctlRecord.m_DtmInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate;
            clsThreeMeasureRecordInfo[] objRecordArr = m_objRecordDomain.m_objGetThreeMeasureRecordInfoArr(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));

			if(objRecordArr == null)
				return;

			m_mthResetAll();

			for(int i=0;i<objRecordArr.Length;i++)
			{
				//add modifydate
				bool blnOk = m_ctlRecord.m_blnSetXml(objRecordArr[i].m_objXmlValue);

				if(!blnOk)
				{
					clsPublicFunction.ShowInformationMessageBox("��¼�����������ݿ⡣");
					m_mthResetAll();					
					return;
				}

				m_strCurrentOpenDate = objRecordArr[i].m_strOpenDate;

				DateTime dtmRecordDate = DateTime.Parse(objRecordArr[i].m_strCreateDate).Date;
				m_hasLastModifyDateInRecord[dtmRecordDate] = DateTime.Parse(objRecordArr[i].m_strModifyDate).ToString("yyyy-MM-dd HH:mm:ss");
				m_hasOpenDateInRecord[dtmRecordDate] =DateTime.Parse( objRecordArr[i].m_strOpenDate).ToString("yyyy-MM-dd HH:mm:ss");
			}
		
			int intIndex = m_intGetLastestTime(DateTime.Now);
			m_cboPulseTimeFlag.SelectedIndex = intIndex;
			m_cboTemperatureTimeFlag.SelectedIndex = intIndex;
			m_cboDownBaseTime.SelectedIndex = intIndex;
			m_cboBreathTime.SelectedIndex = intIndex;
			
			m_txtWhichWeek.Text = m_ctlRecord.m_IntTotalWeek.ToString();
		}

		private void m_mthAsk_Reload(clsPatient p_objSelectedPatient)
		{
			if(clsPublicFunction.ShowQuestionMessageBox("��¼�Ѿ����޸ģ��Ƿ��������¼�¼��")
				== DialogResult.Yes)
			{
				m_mthSetPatientInfo(p_objSelectedPatient);
			}
		}


		private void m_mthMakeContentAccessInfo(ArrayList p_arlSaveTemp,clsThreeMeasureNewContentAccess p_objContentAccess,string p_strInPatientID,string p_strInPatientDate,string p_strCreateTime,string p_strModifyDate,string p_strModifyUserID)
		{
			m_mthMakeContentAccessInfo(p_arlSaveTemp,p_objContentAccess,p_strInPatientID,p_strInPatientDate,p_strCreateTime,p_strModifyDate,p_strModifyUserID,"");
		}
		private void m_mthMakeContentAccessInfo(ArrayList p_arlSaveTemp,clsThreeMeasureNewContentAccess p_objContentAccess,string p_strInPatientID,string p_strInPatientDate,string p_strCreateTime,string p_strModifyDate,string p_strModifyUserID,string p_strOpenDate)
		{
			clsThreeMeasureRecordContentAccessInfo objInfo = null;

            if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(Convert.ToDateTime(p_strCreateTime)))
            {
                return;
            }

            if (p_objContentAccess.m_objPulseValueArr.Length <= 0 && p_objContentAccess.m_objTemperatureValueArr.Length <= 0
                && p_objContentAccess.m_objBreathValueArr.Length <= 0)
            {
                return;
            }

            objInfo = new clsThreeMeasureRecordContentAccessInfo();
            objInfo.m_strInPatientID = p_strInPatientID;
            objInfo.m_strInPatientDate = p_strInPatientDate;
            objInfo.m_strOpenDate = p_strOpenDate;
            objInfo.m_strCreateTime = p_strCreateTime;
            objInfo.m_strModifyDate = p_strModifyDate;
            objInfo.m_strModifyUserID = p_strModifyUserID;

            p_arlSaveTemp.Add(objInfo);

			for(int i=0;i<p_objContentAccess.m_objPulseValueArr.Length;i++)
			{
				objInfo.m_strPulseLinePreValue = p_objContentAccess.m_objPulseValueArr[i].m_blnLineToPreValue?"1":"0";
				objInfo.m_strPulseTimeFlag = ((int)p_objContentAccess.m_objPulseValueArr[i].m_enmParamTime).ToString();
				objInfo.m_strPulseType = ((int)p_objContentAccess.m_objPulseValueArr[i].m_enmType).ToString();
				objInfo.m_strPulseValue = p_objContentAccess.m_objPulseValueArr[i].m_intValue.ToString();
			}			

			for(int i=0;i<p_objContentAccess.m_objTemperatureValueArr.Length;i++)
			{
				objInfo.m_strTemperatureLinePreValue = p_objContentAccess.m_objTemperatureValueArr[i].m_blnLineToPreValue?"1":"0";
				objInfo.m_strTemperatureTimeFlag = ((int)p_objContentAccess.m_objTemperatureValueArr[i].m_enmParamTime).ToString();
				objInfo.m_strTemperatureType = ((int)p_objContentAccess.m_objTemperatureValueArr[i].m_enmType).ToString();
				objInfo.m_strTemperatureValue = p_objContentAccess.m_objTemperatureValueArr[i].m_fltValue.ToString("0.00");

				for(int j2=0;j2<p_objContentAccess.m_objTemperatureValueArr[i].m_arlPhyscalDownValue.Count;j2++)
				{
					clsThreeMeasureTemperaturePhyscalDownValue objDown = (clsThreeMeasureTemperaturePhyscalDownValue)p_objContentAccess.m_objTemperatureValueArr[i].m_arlPhyscalDownValue[j2];

					if(objDown.m_objDeleteInfo == null)
					{
						objInfo.m_strDownTemperaturValue = objDown.m_fltValue.ToString("0.00");
						break;
					}
				}
			}

			for(int i=0;i<p_objContentAccess.m_objBreathValueArr.Length;i++)
			{
				objInfo.m_strBreathTimeFlag = ((int)p_objContentAccess.m_objBreathValueArr[i].m_enmParamTime).ToString();
				objInfo.m_strBreathType = ((int)p_objContentAccess.m_objBreathValueArr[i].m_enmBreathType).ToString();
				objInfo.m_strBreathValue = p_objContentAccess.m_objBreathValueArr[i].m_intValue.ToString();
			}
		}
		
		protected override long m_lngSubAddNew()
		{	
			m_dtpRecordDateTime.Value = m_dtpRecordDateTime.Value.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);

            clsPatient objPatient = m_objBaseCurrentPatient;
	
			if(objPatient == null)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("����ѡ���ˡ�");
#endif								
				return -6;
			}

			m_objOleXml = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);

			string strServModifyDate = m_objPublic.m_strGetServerTime();
					
			long lngRes = m_lngSaveNew(strServModifyDate);	

			//			long lngRes = m_lngNewSaveNew(strServModifyDate);

			if(lngRes < 0)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("������Ӽ�¼��");
#endif								
				return -5;
			}

			bool blnIsAddNew;

			lngRes = m_objRecordDomain.m_lngCheckNewCreateDate(objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.ToString("yyyy-MM-dd 00:00:00"),out blnIsAddNew);

			if(lngRes <= 0)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);
				m_mthShowDBError();
				return lngRes;
			}

			#region Main
			clsThreeMeasureRecordInfo objNewInfo = new clsThreeMeasureRecordInfo();
			objNewInfo.m_objXmlValue = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);			
			objNewInfo.m_strCreateDate = m_dtpRecordDateTime.Value.ToString("yyyy-MM-dd 00:00:00");
			objNewInfo.m_strInPatientDate = m_strInPatientDate;
			objNewInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			#endregion

			clsThreeMeasureNewValueInDate objDateValue = m_ctlRecord.m_objGetDateValue(m_dtpRecordDateTime.Value);

			#region Content
			clsThreeMeasureRecordContentInfo objNewContentInfo = new clsThreeMeasureRecordContentInfo();
			objNewContentInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			objNewContentInfo.m_strInPatientDate = m_strInPatientDate;
			objNewContentInfo.m_strModifyDate = strServModifyDate;
			objNewContentInfo.m_strModifyUserID = m_strUserID;
			if(objDateValue.m_objSpecialDate != null)
			{
				objNewContentInfo.m_strSpecialDateIsNew = objDateValue.m_objSpecialDate.m_blnIsNewStart?"1":"0";
			}
			if(objDateValue.m_objInputValue != null)
			{
				objNewContentInfo.m_strInputStreamValue = objDateValue.m_objInputValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objDejectaValue != null)
			{
				objNewContentInfo.m_strDejectaNeedWeight = objDateValue.m_objDejectaValue.m_blnNeedWeight?"1":"0";
				objNewContentInfo.m_strDejectaBeforeTimes = objDateValue.m_objDejectaValue.m_intBeforeTimes.ToString();
				objNewContentInfo.m_strDejectaAfterMoreTimes = objDateValue.m_objDejectaValue.m_blnAfterMoreTimes?"1":"0";
				objNewContentInfo.m_strDejectaAfterTimes = objDateValue.m_objDejectaValue.m_intAfterTimes.ToString();
				objNewContentInfo.m_strDejectaClysisTimes = objDateValue.m_objDejectaValue.m_intClysisTimes.ToString();
				objNewContentInfo.m_strDejectaWeight = objDateValue.m_objDejectaValue.m_fltWeight.ToString("0.00");
				objNewContentInfo.m_strCanDejecta = objDateValue.m_objDejectaValue.m_blnCanDejecta?"0":"1";
			}
			if(objDateValue.m_objPeeValue != null)
			{
				objNewContentInfo.m_strPeeIsIrretention = objDateValue.m_objPeeValue.m_blnIsIrretention?"1":"0";
				objNewContentInfo.m_strPeeValue = objDateValue.m_objPeeValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objOutStreamValue != null)
			{
				objNewContentInfo.m_strOutStreamValue = objDateValue.m_objOutStreamValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue1 != null)
			{
				objNewContentInfo.m_strDiastolicValue1 = objDateValue.m_objPressureValue1.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue1 = objDateValue.m_objPressureValue1.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue2 != null)
			{
				objNewContentInfo.m_strDiastolicValue2 = objDateValue.m_objPressureValue2.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue2 = objDateValue.m_objPressureValue2.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objWeightValue != null)
			{
				objNewContentInfo.m_strWeightType = ((int)objDateValue.m_objWeightValue.m_enmWeightType).ToString();
				objNewContentInfo.m_strWeightValue = objDateValue.m_objWeightValue.m_fltValue.ToString("0.00");
			}
			#endregion
					
			#region ContentAccess
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID);

			for(int i=0;i<objDateValue.m_objEventArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objEventArr[i].m_dtmEventTime))
                {
                    continue;
                }

				clsThreeMeasureRecordContentAccessInfo objInfo = null;

				for(int j2=0;j2<m_arlSaveTemp.Count;j2++)
				{
					clsThreeMeasureRecordContentAccessInfo objTempInfo = (clsThreeMeasureRecordContentAccessInfo)m_arlSaveTemp[j2];

					if(objTempInfo.m_strCreateTime == objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss"))
					{
						objInfo = objTempInfo;
						break;
					}
				}

				if(objInfo == null)
				{
					objInfo = new clsThreeMeasureRecordContentAccessInfo();
						
					objInfo.m_strInPatientID = objPatient.m_StrInPatientID;
					objInfo.m_strInPatientDate = m_strInPatientDate;
					objInfo.m_strCreateTime = objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss");
					objInfo.m_strModifyDate = strServModifyDate;
					objInfo.m_strModifyUserID = m_strUserID;

					m_arlSaveTemp.Add(objInfo);
				}

				objInfo.m_strEventFlag = ((int)objDateValue.m_objEventArr[i].m_enmEventType).ToString();
			}		
			clsThreeMeasureRecordContentAccessInfo [] objNewContentAccessArr = (clsThreeMeasureRecordContentAccessInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentAccessInfo));
			m_arlSaveTemp.Clear();
			#endregion

			#region ContentEvent
			for(int i=0;i<objDateValue.m_objSkinTestValueArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objSkinTestValueArr[i].m_dtmSkinTestDate))
                {
                    continue;
                }

				clsThreeMeasureSkinTestValue objSkinTestValue = (clsThreeMeasureSkinTestValue)objDateValue.m_objSkinTestValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strCreateTime = objSkinTestValue.m_dtmSkinTestDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				m_cboSkinTestMedicine.Text = objSkinTestValue.m_strMedicineName;

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboSkinTestMedicine.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboSkinTestMedicine.Text;
				objEventInfo.m_strBeginEventDate = "2003-1-1 00:00:00";
				m_cboSkinTestMedicine.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objSkinTestValue.m_blnIsBad.ToString();

				m_arlSaveTemp.Add(objEventInfo);
			}

			for(int i=0;i<objDateValue.m_objOtherValueArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objOtherValueArr[i].m_dtmOtherDate))
                {
                    continue;
                }

				clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objDateValue.m_objOtherValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strCreateTime = objOtherValue.m_dtmOtherDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				//				m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
				string strOtherName = m_ctlRecord.m_strGetOtherName();
				if(strOtherName != null && strOtherName != "")
				{
					m_cboOtherItem.Text = strOtherName;
					m_cboOtherItem.Enabled = false;
				}
				else
				{
					m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
					m_cboOtherItem.SelectedIndex = -1;
				}

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboOtherItem.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboOtherItem.Text;
				objEventInfo.m_strBeginEventDate = "2003-01-01 00:00:00";
				//				m_cboOtherItem.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objOtherValue.m_fltOtherValue.ToString("0.00");

				m_arlSaveTemp.Add(objEventInfo);
			}
			clsThreeMeasureRecordContentEventInfo [] objNewContentEventArr = (clsThreeMeasureRecordContentEventInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentEventInfo));
			m_arlSaveTemp.Clear();
			#endregion

			if(m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date] == null && blnIsAddNew)
			{
				objNewInfo.m_strOpenDate = strServModifyDate;
				objNewInfo.m_strCreateID = m_strUserID;

				objNewContentInfo.m_strOpenDate = strServModifyDate;
				for(int i=0;i<objNewContentAccessArr.Length;i++)
				{
					objNewContentAccessArr[i].m_strOpenDate = strServModifyDate;
				}
				for(int i=0;i<objNewContentEventArr.Length;i++)
				{
					objNewContentEventArr[i].m_strOpenDate = strServModifyDate;
				}

				
				//Add New
				lngRes = m_objRecordDomain.m_lngAddNew(objNewInfo,objNewContentInfo,objNewContentAccessArr,objNewContentEventArr,true);

				if(lngRes > 0)
					m_hasOpenDateInRecord[m_dtpRecordDateTime.Value.Date] = objNewContentInfo.m_strModifyDate;
			}
			else
			{
				string strLastModifyDate = (string)m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date];
				string strOpenDate = (string)m_hasOpenDateInRecord[m_dtpRecordDateTime.Value.Date];

				if(strLastModifyDate == null)
				{
					m_ctlRecord.m_blnSetXml(m_objOleXml);
#if !Debug
					//��ʾ������
					m_mthAsk_Reload(objPatient);
#endif
					
					return -4;
				}
				else
				{
					bool blnIsLast;
					bool blnIsDelete;
					string strChangedUserID = null;
					string strChangedDate = null;

					lngRes = m_objRecordDomain.m_lngCheckLastModifyDate(objPatient.m_StrInPatientID,m_strInPatientDate,strOpenDate,strLastModifyDate,out blnIsLast,out blnIsDelete,out strChangedUserID,out strChangedDate);

					if(lngRes <= 0)
					{
						m_ctlRecord.m_blnSetXml(m_objOleXml);
						m_mthShowDBError();
						return lngRes;
					}

					if(blnIsDelete)
					{
						m_ctlRecord.m_blnSetXml(m_objOleXml);
						m_mthShowRecordDeleted(strChangedUserID,strChangedDate);
						return -9;
					}

					if(!blnIsLast)
					{
						if(m_bolShowRecordModified(strChangedUserID,strChangedDate))
						{
							m_mthSetPatientInfo(objPatient);
						}
						else
						{
							m_ctlRecord.m_blnSetXml(m_objOleXml);
						}						
#if Debug
						m_strLastModifyDate = strLastModifyDate;
#endif						
						return -3;
					}
					else
					{
						objNewInfo.m_strOpenDate = strOpenDate;
						objNewContentInfo.m_strOpenDate = strOpenDate;
						for(int i=0;i<objNewContentAccessArr.Length;i++)
						{
							objNewContentAccessArr[i].m_strOpenDate = strOpenDate;
						}
						for(int i=0;i<objNewContentEventArr.Length;i++)
						{
							objNewContentEventArr[i].m_strOpenDate = strOpenDate;
						}
				
						lngRes = m_objRecordDomain.m_lngAddNew(objNewInfo,objNewContentInfo,objNewContentAccessArr,objNewContentEventArr,false);
					}
				}
			}
            if (string.IsNullOrEmpty(m_strCurrentOpenDate))
                m_strCurrentOpenDate = objNewInfo.m_strOpenDate;
			if(lngRes <= 0)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);
				m_mthShowDBError();
				return lngRes;
			}

			m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date] = objNewContentInfo.m_strModifyDate;
			
			//			m_dtpRecordDateTime.Value = DateTime.Now;

			m_mthClear();

			return 1;
		}

		protected override long m_lngSubModify()
		{
            clsPatient objPatient = m_objBaseCurrentPatient;
	
			if(objPatient == null)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("����ѡ����");
#endif
				return -6;
			}

            if (m_cboTemperatureType.Enabled && m_cboTemperatureType.SelectedIndex == 3)
            {
                clsPublicFunction.ShowInformationMessageBox("���ܽ������¶���Ŀ��Ϊ�����¡�");
                return -1;
            }

			m_objOleXml = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);
			
			string strServModifyDate = m_objPublic.m_strGetServerTime();

			long lngRes = m_lngModify(strServModifyDate);	

			if(lngRes < 0)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("�����޸ļ�¼��");
#endif
				return -5;
			}

			string strLastModifyDate = (string)m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date];
			string strOpenDate = (string)m_hasOpenDateInRecord[m_dtpRecordDateTime.Value.Date];

			if(strLastModifyDate == null)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);
#if !Debug
				//��ʾ������
				m_mthAsk_Reload(objPatient);
#endif
				return -4;
			}
			else
			{
				bool blnIsLast;
				bool blnIsDelete;
				string strChangedUserID = null;
				string strChangedDate = null;
					
				lngRes = m_objRecordDomain.m_lngCheckLastModifyDate(objPatient.m_StrInPatientID,m_strInPatientDate,strOpenDate,strLastModifyDate,out blnIsLast,out blnIsDelete,out strChangedUserID,out strChangedDate);

				if(lngRes <= 0)
				{
					m_ctlRecord.m_blnSetXml(m_objOleXml);
					m_mthShowDBError();
					return lngRes;
				}

				if(blnIsDelete)
				{
					m_ctlRecord.m_blnSetXml(m_objOleXml);
					m_mthShowRecordDeleted(strChangedUserID,strChangedDate);
					return -9;
				}

				if(!blnIsLast)
				{
					if(m_bolShowRecordModified(strChangedUserID,strChangedDate))
					{
						m_mthSetPatientInfo(objPatient);
					}
					else
					{
						m_ctlRecord.m_blnSetXml(m_objOleXml);
					}	

					return -3;
				}				
			}

			#region Main
			clsThreeMeasureRecordInfo objNewInfo = new clsThreeMeasureRecordInfo();
			objNewInfo.m_objXmlValue = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);
			objNewInfo.m_strOpenDate = strOpenDate;
			objNewInfo.m_strCreateDate = m_dtpRecordDateTime.Value.ToString("yyyy-MM-dd 00:00:00");
			objNewInfo.m_strInPatientDate = m_strInPatientDate;
			objNewInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			#endregion

			clsThreeMeasureNewValueInDate objDateValue = m_ctlRecord.m_objGetDateValue(m_dtpRecordDateTime.Value);

			#region Content
			clsThreeMeasureRecordContentInfo objNewContentInfo = new clsThreeMeasureRecordContentInfo();
			objNewContentInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			objNewContentInfo.m_strInPatientDate = m_strInPatientDate;
			objNewContentInfo.m_strOpenDate = strOpenDate;
			objNewContentInfo.m_strModifyDate = strServModifyDate;
			objNewContentInfo.m_strModifyUserID = m_strUserID;
			if(objDateValue.m_objSpecialDate != null)
			{
				objNewContentInfo.m_strSpecialDateIsNew = objDateValue.m_objSpecialDate.m_blnIsNewStart?"1":"0";
			}
			if(objDateValue.m_objInputValue != null)
			{
				objNewContentInfo.m_strInputStreamValue = objDateValue.m_objInputValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objDejectaValue != null)
			{
				objNewContentInfo.m_strDejectaNeedWeight = objDateValue.m_objDejectaValue.m_blnNeedWeight?"1":"0";
				objNewContentInfo.m_strDejectaBeforeTimes = objDateValue.m_objDejectaValue.m_intBeforeTimes.ToString();
				objNewContentInfo.m_strDejectaAfterMoreTimes = objDateValue.m_objDejectaValue.m_blnAfterMoreTimes?"1":"0";
				objNewContentInfo.m_strDejectaAfterTimes = objDateValue.m_objDejectaValue.m_intAfterTimes.ToString();
				objNewContentInfo.m_strDejectaClysisTimes = objDateValue.m_objDejectaValue.m_intClysisTimes.ToString();
				objNewContentInfo.m_strDejectaWeight = objDateValue.m_objDejectaValue.m_fltWeight.ToString("0.00");
				objNewContentInfo.m_strCanDejecta = objDateValue.m_objDejectaValue.m_blnCanDejecta?"0":"1";
			}
			if(objDateValue.m_objPeeValue != null)
			{
				objNewContentInfo.m_strPeeIsIrretention = objDateValue.m_objPeeValue.m_blnIsIrretention?"1":"0";
				objNewContentInfo.m_strPeeValue = objDateValue.m_objPeeValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objOutStreamValue != null)
			{
				objNewContentInfo.m_strOutStreamValue = objDateValue.m_objOutStreamValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue1 != null)
			{
				objNewContentInfo.m_strDiastolicValue1 = objDateValue.m_objPressureValue1.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue1 = objDateValue.m_objPressureValue1.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue2 != null)
			{
				objNewContentInfo.m_strDiastolicValue2 = objDateValue.m_objPressureValue2.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue2 = objDateValue.m_objPressureValue2.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objWeightValue != null)
			{
				objNewContentInfo.m_strWeightType = ((int)objDateValue.m_objWeightValue.m_enmWeightType).ToString();
				objNewContentInfo.m_strWeightValue = objDateValue.m_objWeightValue.m_fltValue.ToString("0.00");
			}
			#endregion
					
			#region ContentAccess
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);

			for(int i=0;i<objDateValue.m_objEventArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objEventArr[i].m_dtmEventTime))
                {
                    continue;
                }

				clsThreeMeasureRecordContentAccessInfo objInfo = null;

				for(int j2=0;j2<m_arlSaveTemp.Count;j2++)
				{
					clsThreeMeasureRecordContentAccessInfo objTempInfo = (clsThreeMeasureRecordContentAccessInfo)m_arlSaveTemp[j2];

					if(objTempInfo.m_strCreateTime == objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss"))
					{
						objInfo = objTempInfo;
						break;
					}
				}

				if(objInfo == null)
				{
					objInfo = new clsThreeMeasureRecordContentAccessInfo();
						
					objInfo.m_strInPatientID = objPatient.m_StrInPatientID;
					objInfo.m_strInPatientDate = m_strInPatientDate;
					objInfo.m_strOpenDate = strOpenDate;
					objInfo.m_strCreateTime = objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss");
					objInfo.m_strModifyDate = strServModifyDate;
					objInfo.m_strModifyUserID = m_strUserID;

					m_arlSaveTemp.Add(objInfo);
				}

				objInfo.m_strEventFlag = ((int)objDateValue.m_objEventArr[i].m_enmEventType).ToString();
			}		
			clsThreeMeasureRecordContentAccessInfo [] objNewContentAccessArr = (clsThreeMeasureRecordContentAccessInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentAccessInfo));
			m_arlSaveTemp.Clear();
			#endregion

			#region ContentEvent
			for(int i=0;i<objDateValue.m_objSkinTestValueArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objSkinTestValueArr[i].m_dtmSkinTestDate))
                {
                    continue;
                }

				clsThreeMeasureSkinTestValue objSkinTestValue = (clsThreeMeasureSkinTestValue)objDateValue.m_objSkinTestValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strOpenDate = strOpenDate;
				objEventInfo.m_strCreateTime = objSkinTestValue.m_dtmSkinTestDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				m_cboSkinTestMedicine.Text = objSkinTestValue.m_strMedicineName;

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboSkinTestMedicine.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboSkinTestMedicine.Text;
				objEventInfo.m_strBeginEventDate = "2003-1-1 00:00:00";
				m_cboSkinTestMedicine.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objSkinTestValue.m_blnIsBad.ToString();

				m_arlSaveTemp.Add(objEventInfo);
			}

			for(int i=0;i<objDateValue.m_objOtherValueArr.Length;i++)
			{
                if (m_cboTimeFlag.SelectedIndex != m_intGetLastestTime(objDateValue.m_objOtherValueArr[i].m_dtmOtherDate))
                {
                    continue;
                }

				clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objDateValue.m_objOtherValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strOpenDate = strOpenDate;
				objEventInfo.m_strCreateTime = objOtherValue.m_dtmOtherDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				//				m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
				string strOtherName = m_ctlRecord.m_strGetOtherName();
				if(strOtherName != null && strOtherName != "")
				{
					m_cboOtherItem.Text = strOtherName;
					m_cboOtherItem.Enabled = false;
				}
				else
				{
					m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
					m_cboOtherItem.SelectedIndex = -1;
				}

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboOtherItem.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboOtherItem.Text;
				objEventInfo.m_strBeginEventDate = "2003-1-1 00:00:00";
				//				m_cboOtherItem.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objOtherValue.m_fltOtherValue.ToString("0.00");

				m_arlSaveTemp.Add(objEventInfo);
			}
			clsThreeMeasureRecordContentEventInfo [] objNewContentEventArr = (clsThreeMeasureRecordContentEventInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentEventInfo));
			m_arlSaveTemp.Clear();
			#endregion

			lngRes = m_objRecordDomain.m_lngAddNew(objNewInfo,objNewContentInfo,objNewContentAccessArr,objNewContentEventArr,false);			

			if(lngRes <= 0)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);
				m_mthShowDBError();
				return lngRes;
			}

			m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date] = objNewContentInfo.m_strModifyDate;

			//			m_dtpRecordDateTime.Value = DateTime.Now;

			m_mthClear();

			return 1;
		}

		protected override long m_lngSubDelete()
		{
            clsPatient objPatient = m_objBaseCurrentPatient;
	
			if(objPatient == null)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("����ѡ����");
#endif
				return -6;
			}

			if(m_dtpRecordDateTime.Tag == null)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("��ѡ�����еļ�¼��");
#endif
				return -5;
			}

			m_objOleXml = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);
			
			long lngRes = -1;

			#region �ɵ�ɾ������
			//			if(m_rdbSpecialDateType.Checked)
			//			{
			//				clsThreeMeasureSpecialDate objValue = (clsThreeMeasureSpecialDate)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteSpecialDate(objValue)?1:0;
			//				
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSpecialDate();
			//				}
			//			}
			//			else if(m_rdbEventType.Checked)
			//			{
			//				clsThreeMeasureEvent objValue = (clsThreeMeasureEvent)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteEvent(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetEvent();
			//				}
			//			}
			//			else if(m_rdbPulseType.Checked)
			//			{
			//				clsThreeMeasurePulseValue objValue = (clsThreeMeasurePulseValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeletePulseValue(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPulse();
			//				}
			//			}
			//			else if(m_rdbBreathType.Checked)
			//			{
			//				clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteBreath(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetBreath();
			//				}
			//			}
			//			else if(m_rdbTemperatureType.Checked)
			//			{
			//				clsThreeMeasureTemperatureValue objValue = (clsThreeMeasureTemperatureValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteTemperatureValue(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetTemperature();
			//				}
			//			}
			//			else if(m_rdbDownTemperatureType.Checked)
			//			{
			//				clsThreeMeasureTemperaturePhyscalDownValue objValue = (clsThreeMeasureTemperaturePhyscalDownValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeletePhyscalDownValue(objValue,m_objBaseTemperature,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetDownTemperature();
			//				}
			//			}
			//			else if(m_rdbInputType.Checked)
			//			{
			//				clsThreeMeasureInputValue objValue = (clsThreeMeasureInputValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteInput(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetInput();
			//				}
			//			}
			//			else if(m_rdbDejectaType.Checked)
			//			{
			//				clsThreeMeasureDejectaValue objValue = (clsThreeMeasureDejectaValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteDejecta(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetDejecta();
			//				}
			//			}
			//			else if(m_rdbPeeType.Checked)
			//			{
			//				clsThreeMeasurePeeValue objValue = (clsThreeMeasurePeeValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeletePee(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPee();
			//				}
			//			}
			//			else if(m_rdbOutStreamType.Checked)
			//			{
			//				clsThreeMeasureOutStreamValue objValue = (clsThreeMeasureOutStreamValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteOutStream(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOutStream();
			//				}
			//			}
			//			else if(m_rdbPressureType.Checked)
			//			{
			//				clsThreeMeasurePressureArg objArgValue = (clsThreeMeasurePressureArg)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeletePressure(objArgValue.m_objPressure,objArgValue.m_intPressureIndex,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetPressure();
			//				}
			//			}
			//			else if(m_rdbWeightType.Checked)
			//			{
			//				clsThreeMeasureWeightValue objValue = (clsThreeMeasureWeightValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteWeight(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetWeight();
			//				}
			//			}
			//			else if(m_rdbSkinTestType.Checked)
			//			{
			//				clsThreeMeasureSkinTestValue objValue = (clsThreeMeasureSkinTestValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteSkinTest(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetSkinTest();
			//				}
			//			}
			//			else if(m_rdbOtherType.Checked)
			//			{
			//				clsThreeMeasureOtherValue objValue = (clsThreeMeasureOtherValue)m_dtpRecordDateTime.Tag;
			//				lngRes = m_ctlRecord.m_blnDeleteOther(objValue,m_blnIsControl)?1:0;
			//			
			//				if(lngRes == 1)
			//				{
			//					m_mthResetOther();
			//				}
			//			}
			#endregion
			#region �µ�ɾ������
			if(m_cboEventType.Enabled && !m_nmuEventHour.Enabled)
			{
				clsThreeMeasureSpecialDate objValue = (clsThreeMeasureSpecialDate)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteSpecialDate(objValue)?1:0;
				
				if(lngRes == 1)
				{
					m_mthResetSpecialDate();
				}
			}
			else if(m_cboEventType.Enabled && m_nmuEventHour.Enabled)
			{
				clsThreeMeasureEvent objValue = (clsThreeMeasureEvent)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteEvent(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetEvent();
				}
			}
			else if(m_cboPulseType.Enabled)
			{
				clsThreeMeasurePulseValue objValue = (clsThreeMeasurePulseValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeletePulseValue(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetPulse();
				}
			}
			else if(m_cboBreathValue.Enabled)
			{
				clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteBreath(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetBreath();
				}
			}
			else if(m_cboTemperatureType.Enabled && m_cboTemperatureType.SelectedIndex != 3)
			{
				clsThreeMeasureTemperatureValue objValue = (clsThreeMeasureTemperatureValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteTemperatureValue(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetTemperature();
				}
			}
			else if(m_cboTemperatureType.Enabled && m_cboTemperatureType.SelectedIndex == 3)
			{
				clsThreeMeasureTemperaturePhyscalDownValue objValue = (clsThreeMeasureTemperaturePhyscalDownValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeletePhyscalDownValue(objValue,m_objBaseTemperature,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetDownTemperature();
				}
			}
			else if(m_txtInputValue.Enabled)
			{
				clsThreeMeasureInputValue objValue = (clsThreeMeasureInputValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteInput(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetInput();
				}
			}
			else if(m_cboDejectaBeforeTimes.Enabled)
			{
				clsThreeMeasureDejectaValue objValue = (clsThreeMeasureDejectaValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteDejecta(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetDejecta();
				}
			}
			else if(m_cboPeeValue.Enabled)
			{
				clsThreeMeasurePeeValue objValue = (clsThreeMeasurePeeValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeletePee(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetPee();
				}
			}
			else if(m_txtOutStreamValue.Enabled)
			{
				clsThreeMeasureOutStreamValue objValue = (clsThreeMeasureOutStreamValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteOutStream(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetOutStream();
				}
			}
			else if(m_txtPressureSystolicValue.Enabled && m_txtPressureDiastolicValue.Enabled)
			{
				clsThreeMeasurePressureArg objArgValue = (clsThreeMeasurePressureArg)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeletePressure(objArgValue.m_objPressure,objArgValue.m_intPressureIndex,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetPressure();
				}
			}
			else if(m_cboWeightValue.Enabled)
			{
				clsThreeMeasureWeightValue objValue = (clsThreeMeasureWeightValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteWeight(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetWeight();
				}
			}
			else if(m_cboSkinBadCount.Enabled)
			{
				clsThreeMeasureSkinTestValue objValue = (clsThreeMeasureSkinTestValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteSkinTest(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetSkinTest();
				}
			}
			else if(m_txtOtherValue.Enabled)
			{
				clsThreeMeasureOtherValue objValue = (clsThreeMeasureOtherValue)m_dtpRecordDateTime.Tag;
				lngRes = m_ctlRecord.m_blnDeleteOther(objValue,m_blnIsControl)?1:0;
			
				if(lngRes == 1)
				{
					m_mthResetOther();
				}
			}
			#endregion

			m_ctlRecord.m_mthUpdateDisplay();

			if(lngRes < 0)
			{
#if !Debug
				clsPublicFunction.ShowInformationMessageBox("����ɾ����¼��");
#endif
				return -7;
			}

			string strLastModifyDate = (string)m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date];
			string strOpenDate = (string)m_hasOpenDateInRecord[m_dtpRecordDateTime.Value.Date];
			
			if(strLastModifyDate == null)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);

				//��ʾ������
#if !Debug
				m_mthAsk_Reload(objPatient);
#endif
				
				return -4;
			}
			else
			{
				bool blnIsLast;
				bool blnIsDelete;
				string strChangedUserID = null;
				string strChangedDate = null;					
				
				lngRes = m_objRecordDomain.m_lngCheckLastModifyDate(objPatient.m_StrInPatientID,m_strInPatientDate,strOpenDate,strLastModifyDate,out blnIsLast,out blnIsDelete,out strChangedUserID,out strChangedDate);

				if(lngRes <= 0)
				{
					m_ctlRecord.m_blnSetXml(m_objOleXml);
					m_mthShowDBError();
					return lngRes;
				}

				if(blnIsDelete)
				{
					m_ctlRecord.m_blnSetXml(m_objOleXml);
					m_mthShowRecordDeleted(strChangedUserID,strChangedDate);
					return -9;
				}

				if(!blnIsLast)
				{
					if(m_bolShowRecordModified(strChangedUserID,strChangedDate))
					{
						m_mthSetPatientInfo(objPatient);
					}
					else
					{
						m_ctlRecord.m_blnSetXml(m_objOleXml);
					}						
					
					return -3;
				}				
			}

			string strServModifyDate = m_objPublic.m_strGetServerTime();
					
			#region Main
			clsThreeMeasureRecordInfo objNewInfo = new clsThreeMeasureRecordInfo();
			objNewInfo.m_objXmlValue = m_ctlRecord.m_objGetXml(m_dtpRecordDateTime.Value);
			objNewInfo.m_strOpenDate = strOpenDate;
			objNewInfo.m_strCreateDate = m_dtpRecordDateTime.Value.ToString("yyyy-MM-dd 00:00:00");
			objNewInfo.m_strInPatientDate = m_strInPatientDate;
			objNewInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			#endregion

			clsThreeMeasureNewValueInDate objDateValue = m_ctlRecord.m_objGetDateValue(m_dtpRecordDateTime.Value);

			#region Content
			clsThreeMeasureRecordContentInfo objNewContentInfo = new clsThreeMeasureRecordContentInfo();
			objNewContentInfo.m_strInPatientID = objPatient.m_StrInPatientID;
			objNewContentInfo.m_strInPatientDate = m_strInPatientDate;
			objNewContentInfo.m_strOpenDate = strOpenDate;
			objNewContentInfo.m_strModifyDate = strServModifyDate;
			objNewContentInfo.m_strModifyUserID = m_strUserID;
			if(objDateValue.m_objSpecialDate != null)
			{
				objNewContentInfo.m_strSpecialDateIsNew = objDateValue.m_objSpecialDate.m_blnIsNewStart?"1":"0";
			}
			if(objDateValue.m_objInputValue != null)
			{
				objNewContentInfo.m_strInputStreamValue = objDateValue.m_objInputValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objDejectaValue != null)
			{
				objNewContentInfo.m_strDejectaNeedWeight = objDateValue.m_objDejectaValue.m_blnNeedWeight?"1":"0";
				objNewContentInfo.m_strDejectaBeforeTimes = objDateValue.m_objDejectaValue.m_intBeforeTimes.ToString();
				objNewContentInfo.m_strDejectaAfterMoreTimes = objDateValue.m_objDejectaValue.m_blnAfterMoreTimes?"1":"0";
				objNewContentInfo.m_strDejectaAfterTimes = objDateValue.m_objDejectaValue.m_intAfterTimes.ToString();
				objNewContentInfo.m_strDejectaClysisTimes = objDateValue.m_objDejectaValue.m_intClysisTimes.ToString();
				objNewContentInfo.m_strDejectaWeight = objDateValue.m_objDejectaValue.m_fltWeight.ToString("0.00");
				objNewContentInfo.m_strCanDejecta = objDateValue.m_objDejectaValue.m_blnCanDejecta?"0":"1";
			}
			if(objDateValue.m_objPeeValue != null)
			{
				objNewContentInfo.m_strPeeIsIrretention = objDateValue.m_objPeeValue.m_blnIsIrretention?"1":"0";
				objNewContentInfo.m_strPeeValue = objDateValue.m_objPeeValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objOutStreamValue != null)
			{
				objNewContentInfo.m_strOutStreamValue = objDateValue.m_objOutStreamValue.m_fltValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue1 != null)
			{
				objNewContentInfo.m_strDiastolicValue1 = objDateValue.m_objPressureValue1.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue1 = objDateValue.m_objPressureValue1.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objPressureValue2 != null)
			{
				objNewContentInfo.m_strDiastolicValue2 = objDateValue.m_objPressureValue2.m_fltDiastolicValue.ToString("0.00");
				objNewContentInfo.m_strSystolicValue2 = objDateValue.m_objPressureValue2.m_fltSystolicValue.ToString("0.00");
			}
			if(objDateValue.m_objWeightValue != null)
			{
				objNewContentInfo.m_strWeightType = ((int)objDateValue.m_objWeightValue.m_enmWeightType).ToString();
				objNewContentInfo.m_strWeightValue = objDateValue.m_objWeightValue.m_fltValue.ToString("0.00");
			}
			#endregion
					
			#region ContentAccess
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12AMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.am12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj4PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm4h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj8PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm8h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PM,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);
			m_mthMakeContentAccessInfo(m_arlSaveTemp,objDateValue.m_obj12PMHalf,objPatient.m_StrInPatientID,m_strInPatientDate,m_dtpRecordDateTime.Value.Date.AddHours((int)enmParamTime.pm12h).ToString("yyyy-MM-dd HH:mm:ss"),strServModifyDate,m_strUserID,strOpenDate);

			for(int i=0;i<objDateValue.m_objEventArr.Length;i++)
			{
				clsThreeMeasureRecordContentAccessInfo objInfo = null;

				for(int j2=0;j2<m_arlSaveTemp.Count;j2++)
				{
					clsThreeMeasureRecordContentAccessInfo objTempInfo = (clsThreeMeasureRecordContentAccessInfo)m_arlSaveTemp[j2];

					if(objTempInfo.m_strCreateTime == objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss"))
					{
						objInfo = objTempInfo;
						break;
					}
				}

				if(objInfo == null)
				{
					objInfo = new clsThreeMeasureRecordContentAccessInfo();
						
					objInfo.m_strInPatientID = objPatient.m_StrInPatientID;
					objInfo.m_strInPatientDate = m_strInPatientDate;
					objInfo.m_strOpenDate = strOpenDate;
					objInfo.m_strCreateTime = objDateValue.m_objEventArr[i].m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss");
					objInfo.m_strModifyDate = strServModifyDate;
					objInfo.m_strModifyUserID = m_strUserID;

					m_arlSaveTemp.Add(objInfo);
				}

				objInfo.m_strEventFlag = ((int)objDateValue.m_objEventArr[i].m_enmEventType).ToString();
			}		
			clsThreeMeasureRecordContentAccessInfo [] objNewContentAccessArr = (clsThreeMeasureRecordContentAccessInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentAccessInfo));
			m_arlSaveTemp.Clear();
			#endregion

			#region ContentEvent
			for(int i=0;i<objDateValue.m_objSkinTestValueArr.Length;i++)
			{
				clsThreeMeasureSkinTestValue objSkinTestValue = (clsThreeMeasureSkinTestValue)objDateValue.m_objSkinTestValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strOpenDate = strOpenDate;
				objEventInfo.m_strCreateTime = objSkinTestValue.m_dtmSkinTestDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				m_cboSkinTestMedicine.Text = objSkinTestValue.m_strMedicineName;

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboSkinTestMedicine.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboSkinTestMedicine.Text;
				objEventInfo.m_strBeginEventDate = "2003-1-1 00:00:00";
				m_cboSkinTestMedicine.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objSkinTestValue.m_blnIsBad.ToString();

				m_arlSaveTemp.Add(objEventInfo);
			}

			for(int i=0;i<objDateValue.m_objOtherValueArr.Length;i++)
			{
				clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objDateValue.m_objOtherValueArr[i];

				clsThreeMeasureRecordContentEventInfo objEventInfo = new clsThreeMeasureRecordContentEventInfo();

				objEventInfo.m_strInPatientID = objPatient.m_StrInPatientID;
				objEventInfo.m_strInPatientDate = m_strInPatientDate;
				objEventInfo.m_strOpenDate = strOpenDate;
				objEventInfo.m_strCreateTime = objOtherValue.m_dtmOtherDate.ToString("yyyy-MM-dd HH:mm:ss");
				objEventInfo.m_strModifyDate = strServModifyDate;
						
				//				m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
				string strOtherName = m_ctlRecord.m_strGetOtherName();
				if(strOtherName != null && strOtherName != "")
				{
					m_cboOtherItem.Text = strOtherName;
					m_cboOtherItem.Enabled = false;
				}
				else
				{
					m_cboOtherItem.Text = objOtherValue.m_strOtherItem;
					m_cboOtherItem.SelectedIndex = -1;
				}

				//				clsThreeMeasureEventInfo objEvent = (clsThreeMeasureEventInfo)m_cboOtherItem.SelectedItem;
				objEventInfo.m_strThreeMeasureEventID = m_cboOtherItem.Text;
				objEventInfo.m_strBeginEventDate = "2003-1-1 00:00:00";
				//				m_cboOtherItem.SelectedIndex = -1;

				objEventInfo.m_strModifyUserID = m_strUserID;

				objEventInfo.m_strEventValue = objOtherValue.m_fltOtherValue.ToString("0.00");

				m_arlSaveTemp.Add(objEventInfo);
			}
			clsThreeMeasureRecordContentEventInfo [] objNewContentEventArr = (clsThreeMeasureRecordContentEventInfo [])m_arlSaveTemp.ToArray(typeof(clsThreeMeasureRecordContentEventInfo));
			m_arlSaveTemp.Clear();
			#endregion

			lngRes = m_objRecordDomain.m_lngAddNew(objNewInfo,objNewContentInfo,objNewContentAccessArr,objNewContentEventArr,false);			

			if(lngRes <= 0)
			{
				m_ctlRecord.m_blnSetXml(m_objOleXml);
				m_mthShowDBError();
				return lngRes;
			}

			m_hasLastModifyDateInRecord[m_dtpRecordDateTime.Value.Date] = objNewContentInfo.m_strModifyDate;

			//			m_dtpRecordDateTime.Value = DateTime.Now;

			m_mthClear();

			return 1;
		}
		#region �ⲿ��ӡ ����.	
						
//				System.Drawing.Printing.PrintDocument m_pdtPrintDocument;
				protected infPrintRecord objPrintTool;
				
				/// <summary>
				/// ��ӡ��¼
				/// </summary>
				/// <returns></returns>
				protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
				{				
					m_mthDemoPrint_FromDataSource();				
					return 1;
					
				}
				
				//clsIntensiveTendMainPrintTool objPrintTool;
				private void m_mthDemoPrint_FromDataSource()
				{	
					objPrintTool=new clsThreeMeasureRecordPrintTool_New();
					if(objPrintTool==null)
					{
						clsPublicFunction.ShowInformationMessageBox("������m_objGetPrintTool()������");
						return;
					}
					objPrintTool.m_mthInitPrintTool(null);
                    if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                        objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
                    else
                    {
                        m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                        m_objBaseCurrentPatient.m_DtmSelectedHISInDate = m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate;
                        objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objBaseCurrentPatient.m_DtmSelectedInDate, DateTime.MinValue);
                    }				
					objPrintTool.m_mthInitPrintContent();						
						
					m_mthStartPrint();
				}
		
				private void m_mthStartPrint()
				{			
					if(m_blnDirectPrint)
					{
						m_pdcPrintDocument.Print();
					}
					else
					{
						((clsThreeMeasureRecordPrintTool_New)objPrintTool).m_mthPrintPage();
	}
}

	
#endregion �ⲿ��ӡ.

		#region ���ⲿ���Ա���ӡ����ʾʵ��.	
				
		System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
		private void m_mthfrmLoad()
		{	
			this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
			this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
			this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
		}
		private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{			
			objPrintTool.m_mthPrintPage(e);

//			if(ppdPrintPreview != null)
//				while(!ppdPrintPreview.m_blnHandlePrint(e))
//					objPrintTool.m_mthPrintPage(e);
		}
		
		private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthBeginPrint(e);				
		}
		
		private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			objPrintTool.m_mthEndPrint(e);
		}
//		
//		clsThreeMeasureRecordPrintTool_New objPrintTool;
//		private void m_mthDemoPrint_FromDataSource()
//		{	
//			objPrintTool=new clsThreeMeasureRecordPrintTool_New();
//			objPrintTool.m_mthInitPrintTool(null);	
//			if(m_objBaseCurrentPatient==null)
//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
//			else 
//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
//																
//			objPrintTool.m_mthInitPrintContent();									
//																	
//			m_mthStartPrint();
//		}
//				
//		private PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
//		private void m_mthStartPrint()
//		{			
//			if(m_blnDirectPrint)
//			{
//				m_pdcPrintDocument.Print();
//			}
//			else
//			{
//				ppdPrintPreview.Document = m_pdcPrintDocument;
//				ppdPrintPreview.ShowDialog();
//			}
//		}
//				
//		protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
//		{
//			m_mthDemoPrint_FromDataSource();					
//			return 1;
//		}
		#endregion ���ⲿ���Ա���ӡ����ʾʵ��.


		private void frmThreeMeasureRecord_Load(object sender, System.EventArgs e)
		{
			m_mthfrmLoad();

			m_lsvInPatientID.Visible = false;	
		
			m_grbEvent.Visible = false;
			m_grbPulse.Visible = false;
			m_grbBreath.Visible = false;
			m_grbTemperature.Visible = false;
			m_grbDownTemperature.Visible = false;
			m_grbInput.Visible = false;
			m_grbDejecta.Visible = false;
			m_grbPee.Visible = false;
			m_grbOutStream.Visible = false;
			m_grbPressure.Visible = false;
			m_grbWeight.Visible = false;
			m_grbSkinTest.Visible = false;
			m_grbOther.Visible = false;

		}

		protected override bool m_BlnIsAddNew
		{
			get
			{
				return m_dtpRecordDateTime.Tag == null;
			}
		}

		protected override iCare.enmFormState m_EnmCurrentFormState
		{
			get
			{
				return enmFormState.NowUser;
			}
		}
		#endregion

		#region �������
		private void m_rdbBreathNormal_CheckedChanged(object sender, System.EventArgs e)
		{
			m_txtBreathValue.Enabled = m_rdbBreathNormal.Checked;
		}

		private void m_chkCannotDejecta_CheckedChanged(object sender, System.EventArgs e)
		{
			m_nmuDejectaBeforeTimes.Enabled = !m_chkCannotDejecta.Checked;
			m_nmuDejectaClysisTimes.Enabled = !m_chkCannotDejecta.Checked;

			m_chkDejectaAfterMoreTimes.Enabled = !m_chkCannotDejecta.Checked;
			m_nmuDejectaAfterTimes.Enabled = !m_chkCannotDejecta.Checked && !m_chkDejectaAfterMoreTimes.Checked;

			m_chkNeedWeight.Enabled = !m_chkCannotDejecta.Checked;
			m_txtDejectaWeightValue.Enabled = !m_chkCannotDejecta.Checked && m_chkNeedWeight.Checked;
		}

		private void m_chkDejectaAfterMoreTimes_CheckedChanged(object sender, System.EventArgs e)
		{
			m_nmuDejectaAfterTimes.Enabled = !m_chkDejectaAfterMoreTimes.Checked;
		}

		private void m_chkNeedWeight_CheckedChanged(object sender, System.EventArgs e)
		{
			m_txtDejectaWeightValue.Enabled = m_chkNeedWeight.Checked;
		}

		private void m_chkIsIrretention_CheckedChanged(object sender, System.EventArgs e)
		{
			m_txtPeeValue.Enabled = !m_chkIsIrretention.Checked;
		}

		private void m_rdbWeightNormal_CheckedChanged(object sender, System.EventArgs e)
		{
			m_txtWeightValue.Enabled = m_rdbWeightNormal.Checked;
		}
		#endregion

		#region �ӿں���
		public void Delete()
		{
			m_lngDelete();
		}

		public void Display()
		{
		
		}

		public void Display(string cardno, string sendcheckdate)
		{
		
		}

		public void Print()
		{
			m_lngPrint();
		}

		public void Save()
		{
			try
			{
				m_lngSave();
			}
			catch
			{
				clsPublicFunction.ShowInformationMessageBox("��������Ч����ֵ��");
				this.Cursor = Cursors.Default;
			}
		}

		public void Copy()
		{
			m_lngCopy();
		}
		public void Verify()
		{
			//long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}
		public void Cut()
		{
			m_lngCut();
		}

		public void Paste()
		{
			m_lngPaste();
		}

		public void Redo()
		{
		
		}

		public void Undo()
		{
		
		}	
		#endregion

		#region ���Ժ���
#if Debug
		private string m_strLastModifyDate;
		/// <summary>
		/// �������ܲ���
		/// </summary>
		/// <param name="p_objContentMaker">��������������</param>
		/// <param name="p_strInnerMessage">������Ϣ���</param>
		/// <returns>
		/// ִ�н��
		/// </returns>
		public enmAutoTestResult i_enmTestAddNew(com.digitalwave.Utility.clsTestContentMaker p_objContentMaker, out string p_strInnerMessage)
		{
			//ʹ��ģ��
			//		m_txt.Text = p_objContentMaker.m_strNextTextValue(m_txt,p_objContentMaker.m_Obj);
			//		m_cbo.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cbo);
			//		m_trv.SelectedNode = p_objContentMaker.m_trnNextTreeViewNode(m_trv);

			string strPatientID = p_objContentMaker.m_strNextTextValue(txtInPatientID,p_objContentMaker.m_ObjDigitalStringValueType,"0000000",2,1);

			clsPatient objPatient = new clsPatient(strPatientID);

			m_mthSetPatientInfo(objPatient);

			m_dtpRecordDateTime.Value = DateTime.Now.Date.AddDays(p_objContentMaker.m_intNextSelectIndex(grbRecordType,10000)%50);

			int intParamIndex = p_objContentMaker.m_intNextSelectIndex(grbRecordType,14*100)%14;

            switch(intParamIndex)
			{
				case 0:
		#region ��������
					m_rdbSpecialDateType.Checked = true;
		#endregion
					break;
				case 1:
		#region �¼�
					m_rdbEventType.Checked = true;
//					int intHours = p_objContentMaker.m_intNextSelectIndex(m_dtpEventTime,23);
//					int intMinutes = p_objContentMaker.m_intNextSelectIndex(m_dtpEventTime,59);
//					m_dtpEventTime.Value = DateTime.Now.Date.AddHours(intHours).AddMinutes(intMinutes);
					m_nmuEventHour.Value = p_objContentMaker.m_intNextSelectIndex(m_nmuEventHour,23);
					m_nmuEventMinute.Value = p_objContentMaker.m_intNextSelectIndex(m_nmuEventMinute,59);
					

					m_cboEventType.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboEventType,m_cboEventType.GetItemsCount()-1);
		#endregion
					break;
				case 2: 
		#region ����
					m_rdbPulse.Checked = true;
					m_cboPulseTimeFlag.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboPulseTimeFlag,m_cboPulseTimeFlag.GetItemsCount()-1);

					m_chkPulseIsHalf.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkPulseIsHalf,99)%2==0;

					if(p_objContentMaker.m_intNextSelectIndex(m_rdbPulseType,99)%2==0)
					{
						m_rdbPulse.Checked = true;
						m_rdbHeartRate.Checked = false;
					}
					else
					{
						m_rdbPulse.Checked = false;
						m_rdbHeartRate.Checked = true;
					}

					m_txtPulseValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPulseValue,p_objContentMaker.m_ObjDigitalValueType,140,40);

					m_chkPulseNotLineToPre.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkPulseNotLineToPre,99)%2==0;
			
		#endregion
					break;
				case 3:
		#region ����
					m_rdbBreathType.Checked = true;
					m_cboBreathTime.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboBreathTime,m_cboBreathTime.GetItemsCount()-1);

					switch(p_objContentMaker.m_intNextSelectIndex(m_rdbPulseType,98)%3)
					{
						case 0:
							m_rdbBreathNormal.Checked = true;
							m_rdbBreathStartAssistant.Checked = false;
							m_rdbBreathStopAssistant.Checked = false;

							m_txtBreathValue.Text = p_objContentMaker.m_strNextTextValue(m_txtBreathValue,p_objContentMaker.m_ObjDigitalValueType,100,2);
							break;
						case 1:
							m_rdbBreathNormal.Checked = false;
							m_rdbBreathStartAssistant.Checked = true;
							m_rdbBreathStopAssistant.Checked = false;

							m_txtBreathValue.Text = "";
							break;
						case 2:
							m_rdbBreathNormal.Checked = false;
							m_rdbBreathStartAssistant.Checked = false;
							m_rdbBreathStopAssistant.Checked = true;

							m_txtBreathValue.Text = "";
							break;
					}
		#endregion
					break;
				case 4:
		#region ����
					m_rdbTemperatureType.Checked = true;
					m_cboTemperatureTimeFlag.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboTemperatureTimeFlag,m_cboTemperatureTimeFlag.GetItemsCount()-1);

					m_chkTemperatureIsHalf.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkTemperatureIsHalf,99)%2==0;

					switch(p_objContentMaker.m_intNextSelectIndex(m_rdbTemperatureType,98)%3)
					{
						case 0:
							m_rdbArmpitTemperature.Checked = true;
							m_rdbAnusTemperature.Checked = false;
							m_rdbMouthTemperature.Checked = false;
							break;
						case 1:
							m_rdbArmpitTemperature.Checked = false;
							m_rdbAnusTemperature.Checked = true;
							m_rdbMouthTemperature.Checked = false;
							break;
						case 2:
							m_rdbArmpitTemperature.Checked = false;
							m_rdbAnusTemperature.Checked = false;
							m_rdbMouthTemperature.Checked = true;
							break;
					}

					m_txtTemperatureValue.Text = p_objContentMaker.m_strNextTextValue(m_txtTemperatureValue,p_objContentMaker.m_ObjDigitalValueType,40d,34d);

					m_chkTemperatureNotLineToPre.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkTemperatureNotLineToPre,99)%2==0;
			
		#endregion
					break;
				case 5:
		#region �����º������
					m_rdbDownTemperatureType.Checked = true;
					m_cboDownBaseTime.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboDownBaseTime,m_cboDownBaseTime.GetItemsCount()-1);

					m_chkDownBaseTimeIsHalf.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkDownBaseTimeIsHalf,99)%2==0;

					m_txtDownTemperatureValue.Text = p_objContentMaker.m_strNextTextValue(m_txtDownTemperatureValue,p_objContentMaker.m_ObjDigitalValueType,40d,34d);

		#endregion
					break;
				case 6:
		#region ����Һ��
					m_rdbInputType.Checked = true;
					
					m_txtInputValue.Text = p_objContentMaker.m_strNextTextValue(m_txtInputValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

		#endregion
					break;
				case 7:
		#region ���
					m_rdbDejectaType.Checked = true;

					if(p_objContentMaker.m_intNextSelectIndex(m_chkCannotDejecta,99)%2==0)
					{
						m_chkCannotDejecta.Checked = true;
					}
					else
					{
						m_chkCannotDejecta.Checked = false;

						int intBeforeTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaBeforeTimes,1000);
						m_nmuDejectaBeforeTimes.Value = intBeforeTimes;

						int intClysisTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaClysisTimes,1000);
						m_nmuDejectaClysisTimes.Value = intClysisTimes;

						if(p_objContentMaker.m_intNextSelectIndex(m_chkDejectaAfterMoreTimes,99)%2==0)
						{
							m_chkDejectaAfterMoreTimes.Checked = true;
						}
						else
						{
							m_chkDejectaAfterMoreTimes.Checked = false;
						
							int intAfterTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaAfterTimes,1000);
							m_nmuDejectaAfterTimes.Value = intAfterTimes;
						}

						if(p_objContentMaker.m_intNextSelectIndex(m_chkNeedWeight,99)%2==0)
						{
							m_chkNeedWeight.Checked = false;
						}
						else
						{
							m_chkNeedWeight.Checked = true;
							
							m_txtDejectaWeightValue.Text = p_objContentMaker.m_strNextTextValue(m_txtDejectaWeightValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
						}
					}					
		#endregion
					break;
				case 8:
		#region ����
					m_rdbPeeType.Checked = true;

					if(p_objContentMaker.m_intNextSelectIndex(m_chkIsIrretention,99)%2==0)
					{
						m_chkIsIrretention.Checked = true;
					}
					else
					{
						m_chkIsIrretention.Checked = false;
							
						m_txtPeeValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPeeValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
					}
		#endregion
					break;
				case 9:
		#region ������
					m_rdbOutStreamType.Checked = true;
					
					m_txtOutStreamValue.Text = p_objContentMaker.m_strNextTextValue(m_txtOutStreamValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

		#endregion
					break;
				case 10:
		#region Ѫѹ
					m_rdbPressureType.Checked = true;
					
					m_txtPressureDiastolicValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPressureDiastolicValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

					m_txtPressureSystolicValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPressureSystolicValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
		#endregion
					break;
				case 11:
		#region ����
					m_rdbWeightType.Checked = true;

					switch(p_objContentMaker.m_intNextSelectIndex(m_rdbWeightType,98)%3)
					{
						case 0:
							m_rdbWeightNormal.Checked = true;
							m_rdbWeightBed.Checked = false;
							m_rdbWeightCar.Checked = false;

							m_txtWeightValue.Text = p_objContentMaker.m_strNextTextValue(m_txtWeightValue,p_objContentMaker.m_ObjDigitalValueType,1000,0);
							break;
						case 1:
							m_rdbWeightNormal.Checked = false;
							m_rdbWeightBed.Checked = true;
							m_rdbWeightCar.Checked = false;

							m_txtWeightValue.Text = "";
							break;
						case 2:
							m_rdbWeightNormal.Checked = false;
							m_rdbWeightBed.Checked = false;
							m_rdbWeightCar.Checked = true;

							m_txtWeightValue.Text = "";
							break;
					}
		#endregion
					break;
				case 12:
		#region Ƥ��
					m_rdbSkinTestType.Checked = true;

					m_cboSkinTestMedicine.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboSkinTestMedicine,m_cboSkinTestMedicine.GetItemsCount()-1);

					if(p_objContentMaker.m_intNextSelectIndex(m_rdbSkinTestType,99)%2==0)
					{
						m_rdbSkinTestBad.Checked = true;
						m_rdbSkinTestGood.Checked = false;
					}
					else
					{
						m_rdbSkinTestBad.Checked = false;
						m_rdbSkinTestGood.Checked = true;
					}
		#endregion
					break;
				case 13:
		#region ����
					m_rdbOtherType.Checked = true;

					m_cboOtherItem.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboOtherItem,m_cboOtherItem.GetItemsCount()-1);

					m_cboOtherUnit.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboOtherUnit,m_cboOtherUnit.GetItemsCount()-1);

					m_txtOtherValue.Text = p_objContentMaker.m_strNextTextValue(m_txtOtherValue,p_objContentMaker.m_ObjDigitalValueType,1000d,-1000d);
		#endregion
					break;
			}

			long lngRes = m_lngSave();

			p_strInnerMessage = lngRes.ToString("0 ");

			p_strInnerMessage += intParamIndex.ToString("0 ");

			switch(lngRes)
			{
				case -3:
					p_strInnerMessage += "(!blnIsLast)\r\n";

					p_strInnerMessage += m_strLastModifyDate;
					break;
				case -4:
					p_strInnerMessage += "(strLastModifyDate == null)";
					break;
				case -5:
					p_strInnerMessage += "(������Ӽ�¼��)";
					return enmAutoTestResult.Succeed;
				case -6:
					p_strInnerMessage += "(����ѡ���ˡ�)";
					break;
			}

			if(lngRes > 0)
				return enmAutoTestResult.Succeed;
			else
				return enmAutoTestResult.Failure;
		}
	
		/// <summary>
		/// �޸Ĺ��ܲ���
		/// </summary>
		/// <param name="p_objContentMaker">��������������</param>
		/// <param name="p_strInnerMessage">������Ϣ���</param>
		/// <returns>
		/// ִ�н��
		/// </returns>
		public enmAutoTestResult i_enmTestModify(com.digitalwave.Utility.clsTestContentMaker p_objContentMaker, out string p_strInnerMessage)
		{	
			//ʹ��ģ��
			//		m_txt.Text = p_objContentMaker.m_strNextTextValue(m_txt,p_objContentMaker.m_Obj);	
			//		m_cbo.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cbo);
			//		m_trv.SelectedNode = p_objContentMaker.m_trnNextTreeViewNode(m_trv);

			string strPatientID = p_objContentMaker.m_strNextTextValue(txtInPatientID,p_objContentMaker.m_ObjDigitalStringValueType,"0000000",2,1);

			clsPatient objPatient = new clsPatient(strPatientID);

			m_mthSetPatientInfo(objPatient);

			m_dtpRecordDateTime.Value = DateTime.Now.Date.AddDays(p_objContentMaker.m_intNextSelectIndex(grbRecordType,10000)%50);

			clsThreeMeasureNewValueInDate objNewValue = m_ctlRecord.m_objGetDateValue(m_dtpRecordDateTime.Value);

			int intCount = 0//objNewValue.m_objBreathValueArr.Length
				+1//objNewValue.m_objDejectaValue
				+objNewValue.m_objEventArr.Length
				+1//objNewValue.m_objInputValue
				+objNewValue.m_objOtherValueArr.Length
				+1//objNewValue.m_objOutStreamValue
				+1//objNewValue.m_objPeeValue
				+1//objNewValue.m_objPressureValue
				+objNewValue.m_objPulseValueArr.Length
				+objNewValue.m_objSkinTestValueArr.Length
				+0//objNewValue.m_objSpecialDate
				+objNewValue.m_objTemperatureValueArr.Length
				+1;//objNewValue.m_objWeightValue
			for(int i=0;i<objNewValue.m_objTemperatureValueArr.Length;i++)
			{
				for(int j2=0;j2<objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue.Count;j2++)
				{
					if(((clsThreeMeasureTemperaturePhyscalDownValue)objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue[j2]).m_objDeleteInfo == null)
						intCount++;				
				}
			}

			int intStartIndex = p_objContentMaker.m_intNextSelectIndex(grbRecordType,intCount/3);
			int intIndex = intStartIndex+p_objContentMaker.m_intNextSelectIndex(grbRecordType,intCount-intStartIndex);

			intCount = 1;

			m_rdbDejectaType.Checked = true;

			m_dtpRecordDateTime.Tag = objNewValue.m_objDejectaValue;

			m_dtpRecordDateTime.Value = objNewValue.m_objDejectaValue.m_dtmDejectaDate;
				
		#region ���
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objDejectaValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objDejectaValue.m_dtmDejectaDate;
				
		#region ���
				m_rdbDejectaType.Checked = true;

				if(p_objContentMaker.m_intNextSelectIndex(m_chkCannotDejecta,99)%2==0)
				{
					m_chkCannotDejecta.Checked = true;
				}
				else
				{
					m_chkCannotDejecta.Checked = false;

					int intBeforeTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaBeforeTimes,1000);
					m_nmuDejectaBeforeTimes.Value = intBeforeTimes;

					int intClysisTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaClysisTimes,1000);
					m_nmuDejectaClysisTimes.Value = intClysisTimes;

					if(p_objContentMaker.m_intNextSelectIndex(m_chkDejectaAfterMoreTimes,99)%2==0)
					{
						m_chkDejectaAfterMoreTimes.Checked = true;
					}
					else
					{
						m_chkDejectaAfterMoreTimes.Checked = false;
						
						int intAfterTimes = p_objContentMaker.m_intNextSelectIndex(m_nmuDejectaAfterTimes,1000);
						m_nmuDejectaAfterTimes.Value = intAfterTimes;
					}

					if(p_objContentMaker.m_intNextSelectIndex(m_chkNeedWeight,99)%2==0)
					{
						m_chkNeedWeight.Checked = false;
					}
					else
					{
						m_chkNeedWeight.Checked = true;
							
						m_txtDejectaWeightValue.Text = p_objContentMaker.m_strNextTextValue(m_txtDejectaWeightValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
					}
				}					
		#endregion

				goto Modify;
			}
		#endregion
			intCount++;

		#region �¼�
			for(int i=0;i<objNewValue.m_objEventArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objEventArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objEventArr[i].m_dtmEventTime;
				
		#region �¼�
					m_rdbEventType.Checked = true;
//					int intHours = p_objContentMaker.m_intNextSelectIndex(m_dtpEventTime,23);
//					int intMinutes = p_objContentMaker.m_intNextSelectIndex(m_dtpEventTime,59);
//					m_dtpEventTime.Value = DateTime.Now.Date.AddHours(intHours).AddMinutes(intMinutes);
					m_nmuEventHour.Value = p_objContentMaker.m_intNextSelectIndex(m_nmuEventHour,23);
					m_nmuEventMinute.Value = p_objContentMaker.m_intNextSelectIndex(m_nmuEventMinute,59);
					
					m_cboEventType.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboEventType,m_cboEventType.GetItemsCount()-1);
		#endregion

					goto Modify;
				}				
			}
		#endregion

		#region ����Һ��
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objInputValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objInputValue.m_dtmInputDate;
				
		#region ����Һ��
				m_rdbInputType.Checked = true;
					
				m_txtInputValue.Text = p_objContentMaker.m_strNextTextValue(m_txtInputValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

		#endregion

				goto Modify;
			}
		#endregion
			intCount++;

		#region ����
			for(int i=0;i<objNewValue.m_objOtherValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objOtherValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objOtherValueArr[i].m_dtmOtherDate;
				
		#region ����
					m_rdbOtherType.Checked = true;

					m_cboOtherItem.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboOtherItem,m_cboOtherItem.GetItemsCount()-1);

					m_cboOtherUnit.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboOtherUnit,m_cboOtherUnit.GetItemsCount()-1);

					m_txtOtherValue.Text = p_objContentMaker.m_strNextTextValue(m_txtOtherValue,p_objContentMaker.m_ObjDigitalValueType,1000d,-1000d);
		#endregion
					
					goto Modify;
				}				
			}
		#endregion

		#region ������
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objOutStreamValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objOutStreamValue.m_dtmOutStreamDate;
				
		#region ������
				m_rdbOutStreamType.Checked = true;
					
				m_txtOutStreamValue.Text = p_objContentMaker.m_strNextTextValue(m_txtOutStreamValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

		#endregion

				goto Modify;
			}
		#endregion
			intCount++;

		#region ����
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objPeeValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objPeeValue.m_dtmPeeDate;
				
		#region ����
				m_rdbPeeType.Checked = true;

				if(p_objContentMaker.m_intNextSelectIndex(m_chkIsIrretention,99)%2==0)
				{
					m_chkIsIrretention.Checked = true;
				}
				else
				{
					m_chkIsIrretention.Checked = false;
							
					m_txtPeeValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPeeValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
				}
		#endregion

				goto Modify;
			}
		#endregion
			intCount++;

		#region Ѫѹ
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objPressureValue1;

				m_dtpRecordDateTime.Value = objNewValue.m_objPressureValue1.m_dtmPressureDate;
				
		#region Ѫѹ
				m_rdbPressureType.Checked = true;
					
				m_txtPressureDiastolicValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPressureDiastolicValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);

				m_txtPressureSystolicValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPressureSystolicValue,p_objContentMaker.m_ObjDigitalValueType,1000d,0d);
		#endregion

				goto Modify;
			}
		#endregion
			intCount++;

		#region ����
			for(int i=0;i<objNewValue.m_objPulseValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objPulseValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objPulseValueArr[i].m_dtmValueTime;
				
		#region ����
					m_rdbPulse.Checked = true;
					m_cboPulseTimeFlag.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboPulseTimeFlag,m_cboPulseTimeFlag.GetItemsCount()-1);

					m_chkPulseIsHalf.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkPulseIsHalf,99)%2==0;

					if(p_objContentMaker.m_intNextSelectIndex(m_rdbPulseType,99)%2==0)
					{
						m_rdbPulse.Checked = true;
						m_rdbHeartRate.Checked = false;
					}
					else
					{
						m_rdbPulse.Checked = false;
						m_rdbHeartRate.Checked = true;
					}

					m_txtPulseValue.Text = p_objContentMaker.m_strNextTextValue(m_txtPulseValue,p_objContentMaker.m_ObjDigitalValueType,140,40);

					m_chkPulseNotLineToPre.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkPulseNotLineToPre,99)%2==0;
			
		#endregion					
					
					goto Modify;
				}				
			}
		#endregion

		#region Ƥ��
			for(int i=0;i<objNewValue.m_objSkinTestValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objSkinTestValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objSkinTestValueArr[i].m_dtmSkinTestDate;
				
		#region Ƥ��
					m_rdbSkinTestType.Checked = true;

					m_cboSkinTestMedicine.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboSkinTestMedicine,m_cboSkinTestMedicine.GetItemsCount()-1);

					if(p_objContentMaker.m_intNextSelectIndex(m_rdbSkinTestType,99)%2==0)
					{
						m_rdbSkinTestBad.Checked = true;
						m_rdbSkinTestGood.Checked = false;
					}
					else
					{
						m_rdbSkinTestBad.Checked = false;
						m_rdbSkinTestGood.Checked = true;
					}
		#endregion
					
					goto Modify;
				}				
			}
		#endregion

		#region ����
			for(int i=0;i<objNewValue.m_objPulseValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objTemperatureValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objTemperatureValueArr[i].m_dtmValueTime;
				
		#region ����
					m_rdbTemperatureType.Checked = true;
					m_cboTemperatureTimeFlag.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cboTemperatureTimeFlag,m_cboTemperatureTimeFlag.GetItemsCount()-1);

					m_chkTemperatureIsHalf.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkTemperatureIsHalf,99)%2==0;

					switch(p_objContentMaker.m_intNextSelectIndex(m_rdbTemperatureType,98)%3)
					{
						case 0:
							m_rdbArmpitTemperature.Checked = true;
							m_rdbAnusTemperature.Checked = false;
							m_rdbMouthTemperature.Checked = false;
							break;
						case 1:
							m_rdbArmpitTemperature.Checked = false;
							m_rdbAnusTemperature.Checked = true;
							m_rdbMouthTemperature.Checked = false;
							break;
						case 2:
							m_rdbArmpitTemperature.Checked = false;
							m_rdbAnusTemperature.Checked = false;
							m_rdbMouthTemperature.Checked = true;
							break;
					}

					m_txtTemperatureValue.Text = p_objContentMaker.m_strNextTextValue(m_txtTemperatureValue,p_objContentMaker.m_ObjDigitalValueType,40d,34d);

					m_chkTemperatureNotLineToPre.Checked = p_objContentMaker.m_intNextSelectIndex(m_chkTemperatureNotLineToPre,99)%2==0;
			
		#endregion

					goto Modify;
				}			
				else
				{
					for(int j2=0;j2<objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue.Count;j2++,intCount++)
					{
						if(intStartIndex+1 == intCount)
						{
							clsThreeMeasureTemperaturePhyscalDownValue objDownValue = 
								(clsThreeMeasureTemperaturePhyscalDownValue)objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue[j2];

							m_dtpRecordDateTime.Tag = objDownValue;
							m_objBaseTemperature = objNewValue.m_objTemperatureValueArr[i];

							m_dtpRecordDateTime.Value = objDownValue.m_dtmValueTime.Date;

							switch(objNewValue.m_objTemperatureValueArr[i].m_enmParamTime)
							{
								case enmParamTime.am4:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 0;
									break;
								case enmParamTime.am8:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 1;
									break;
								case enmParamTime.am12:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 2;
									break;
								case enmParamTime.pm4:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 3;
									break;
								case enmParamTime.pm8:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 4;
									break;
								case enmParamTime.pm12:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 5;
									break;
								case enmParamTime.am4h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 0;
									break;
								case enmParamTime.am8h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 1;
									break;
								case enmParamTime.am12h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 2;
									break;
								case enmParamTime.pm4h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 3;
									break;
								case enmParamTime.pm8h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 4;
									break;
								case enmParamTime.pm12h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 5;
									break;
							}

		#region �����º������
							m_rdbDownTemperatureType.Checked = true;
							
							m_txtDownTemperatureValue.Text = p_objContentMaker.m_strNextTextValue(m_txtDownTemperatureValue,p_objContentMaker.m_ObjDigitalValueType,40d,34d);

		#endregion

							goto Modify;
						}
					}
				}
			}
			
		#endregion

		#region ����
			m_dtpRecordDateTime.Tag = objNewValue.m_objWeightValue;

			m_dtpRecordDateTime.Value = objNewValue.m_objWeightValue.m_dtmWeightDate;
			
		#region ����
			m_rdbWeightType.Checked = true;

			switch(p_objContentMaker.m_intNextSelectIndex(m_rdbWeightType,98)%3)
			{
				case 0:
					m_rdbWeightNormal.Checked = true;
					m_rdbWeightBed.Checked = false;
					m_rdbWeightCar.Checked = false;

					m_txtWeightValue.Text = p_objContentMaker.m_strNextTextValue(m_txtWeightValue,p_objContentMaker.m_ObjDigitalValueType,1000,0);
					break;
				case 1:
					m_rdbWeightNormal.Checked = false;
					m_rdbWeightBed.Checked = true;
					m_rdbWeightCar.Checked = false;

					m_txtWeightValue.Text = "";
					break;
				case 2:
					m_rdbWeightNormal.Checked = false;
					m_rdbWeightBed.Checked = false;
					m_rdbWeightCar.Checked = true;

					m_txtWeightValue.Text = "";
					break;
			}
		#endregion
			
		#endregion

			Modify:

			long lngRes = m_lngSave();

			p_strInnerMessage = lngRes.ToString("0 ");

			switch(lngRes)
			{
				case -3:
					p_strInnerMessage += "(!blnIsLast)\r\n";

					p_strInnerMessage += m_strLastModifyDate;
					break;
				case -4:
					p_strInnerMessage += "(strLastModifyDate == null)";
					break;
				case -5:
					p_strInnerMessage += "(�����޸ļ�¼��)";
					return enmAutoTestResult.Succeed;
				case -6:
					p_strInnerMessage += "(����ѡ���ˡ�)";
					break;
			}

			if(lngRes > 0)
				return enmAutoTestResult.Succeed;
			else
				return enmAutoTestResult.Failure;
		}

		/// <summary>
		/// ɾ�����ܲ���
		/// </summary>
		/// <param name="p_objContentMaker">��������������</param>
		/// <param name="p_strInnerMessage">������Ϣ���</param>
		/// <returns>
		/// ִ�н��
		/// </returns>
		public enmAutoTestResult i_enmTestDelete(com.digitalwave.Utility.clsTestContentMaker p_objContentMaker, out string p_strInnerMessage)
		{
			//ʹ��ģ��
			//		m_txt.Text = p_objContentMaker.m_strNextTextValue(m_txt,p_objContentMaker.m_Obj);
			//		m_cbo.SelectedIndex = p_objContentMaker.m_intNextSelectIndex(m_cbo);
			//		m_trv.SelectedNode = p_objContentMaker.m_trnNextTreeViewNode(m_trv);
		
			string strPatientID = p_objContentMaker.m_strNextTextValue(txtInPatientID,p_objContentMaker.m_ObjDigitalStringValueType,"0000000",2,1);

			clsPatient objPatient = new clsPatient(strPatientID);

			m_mthSetPatientInfo(objPatient);

			m_dtpRecordDateTime.Value = DateTime.Now.Date.AddDays(p_objContentMaker.m_intNextSelectIndex(grbRecordType,10000)%50);

			clsThreeMeasureNewValueInDate objNewValue = m_ctlRecord.m_objGetDateValue(m_dtpRecordDateTime.Value);

			int intCount = 0//objNewValue.m_objBreathValueArr.Length
				+1//objNewValue.m_objDejectaValue
				+objNewValue.m_objEventArr.Length
				+1//objNewValue.m_objInputValue
				+objNewValue.m_objOtherValueArr.Length
				+1//objNewValue.m_objOutStreamValue
				+1//objNewValue.m_objPeeValue
				+1//objNewValue.m_objPressureValue
				+objNewValue.m_objPulseValueArr.Length
				+objNewValue.m_objSkinTestValueArr.Length
				+0//objNewValue.m_objSpecialDate
				+objNewValue.m_objTemperatureValueArr.Length
				+1;//objNewValue.m_objWeightValue
			for(int i=0;i<objNewValue.m_objTemperatureValueArr.Length;i++)
			{
				for(int j2=0;j2<objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue.Count;j2++)
				{
					if(((clsThreeMeasureTemperaturePhyscalDownValue)objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue[j2]).m_objDeleteInfo == null)
						intCount++;				
				}
			}

			int intStartIndex = p_objContentMaker.m_intNextSelectIndex(grbRecordType,intCount/3);
			int intIndex = intStartIndex+p_objContentMaker.m_intNextSelectIndex(grbRecordType,intCount-intStartIndex);

			intCount = 1;

			m_rdbDejectaType.Checked = true;

			m_dtpRecordDateTime.Tag = objNewValue.m_objDejectaValue;

			m_dtpRecordDateTime.Value = objNewValue.m_objDejectaValue.m_dtmDejectaDate;
				
		#region ���
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objDejectaValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objDejectaValue.m_dtmDejectaDate;
				
				goto Delete;
			}
		#endregion
			intCount++;

		#region �¼�
			for(int i=0;i<objNewValue.m_objEventArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objEventArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objEventArr[i].m_dtmEventTime;
				
					goto Delete;
				}				
			}
		#endregion

		#region ����Һ��
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objInputValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objInputValue.m_dtmInputDate;
				
				goto Delete;
			}
		#endregion
			intCount++;

		#region ����
			for(int i=0;i<objNewValue.m_objOtherValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objOtherValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objOtherValueArr[i].m_dtmOtherDate;
				
					goto Delete;
				}				
			}
		#endregion

		#region ������
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objOutStreamValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objOutStreamValue.m_dtmOutStreamDate;
				
				goto Delete;
			}
		#endregion
			intCount++;

		#region ����
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objPeeValue;

				m_dtpRecordDateTime.Value = objNewValue.m_objPeeValue.m_dtmPeeDate;
				
				goto Delete;
			}
		#endregion
			intCount++;

		#region Ѫѹ
			if(intStartIndex+1 == intCount)
			{
				m_dtpRecordDateTime.Tag = objNewValue.m_objPressureValue1;

				m_dtpRecordDateTime.Value = objNewValue.m_objPressureValue1.m_dtmPressureDate;
				
				goto Delete;
			}
		#endregion
			intCount++;

		#region ����
			for(int i=0;i<objNewValue.m_objPulseValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objPulseValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objPulseValueArr[i].m_dtmValueTime;
				
					goto Delete;
				}				
			}
		#endregion

		#region Ƥ��
			for(int i=0;i<objNewValue.m_objSkinTestValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objSkinTestValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objSkinTestValueArr[i].m_dtmSkinTestDate;
				
					goto Delete;
				}				
			}
		#endregion

		#region ����
			for(int i=0;i<objNewValue.m_objPulseValueArr.Length;i++,intCount++)
			{
				if(intStartIndex+1 == intCount)
				{
					m_dtpRecordDateTime.Tag = objNewValue.m_objTemperatureValueArr[i];

					m_dtpRecordDateTime.Value = objNewValue.m_objTemperatureValueArr[i].m_dtmValueTime;
				
					goto Delete;
				}			
				else
				{
					for(int j2=0;j2<objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue.Count;j2++,intCount++)
					{
						if(intStartIndex+1 == intCount)
						{
							clsThreeMeasureTemperaturePhyscalDownValue objDownValue = 
								(clsThreeMeasureTemperaturePhyscalDownValue)objNewValue.m_objTemperatureValueArr[i].m_arlPhyscalDownValue[j2];

							m_dtpRecordDateTime.Tag = objDownValue;
							m_objBaseTemperature = objNewValue.m_objTemperatureValueArr[i];

							m_dtpRecordDateTime.Value = objDownValue.m_dtmValueTime.Date;

							switch(objNewValue.m_objTemperatureValueArr[i].m_enmParamTime)
							{
								case enmParamTime.am4:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 0;
									break;
								case enmParamTime.am8:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 1;
									break;
								case enmParamTime.am12:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 2;
									break;
								case enmParamTime.pm4:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 3;
									break;
								case enmParamTime.pm8:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 4;
									break;
								case enmParamTime.pm12:
									m_chkDownBaseTimeIsHalf.Checked = false;
									m_cboDownBaseTime.SelectedIndex = 5;
									break;
								case enmParamTime.am4h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 0;
									break;
								case enmParamTime.am8h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 1;
									break;
								case enmParamTime.am12h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 2;
									break;
								case enmParamTime.pm4h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 3;
									break;
								case enmParamTime.pm8h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 4;
									break;
								case enmParamTime.pm12h:
									m_chkDownBaseTimeIsHalf.Checked = true;
									m_cboDownBaseTime.SelectedIndex = 5;
									break;
							}

							goto Delete;
						}
					}
				}
			}
			
		#endregion

		#region ����
			m_dtpRecordDateTime.Tag = objNewValue.m_objWeightValue;

			m_dtpRecordDateTime.Value = objNewValue.m_objWeightValue.m_dtmWeightDate;
			
		#endregion

			Delete:

			long lngRes = m_lngDelete();

			p_strInnerMessage = lngRes.ToString("0 ");

			switch(lngRes)
			{
				case -3:
					p_strInnerMessage += "(!blnIsLast)\r\n";

					p_strInnerMessage += m_strLastModifyDate;
					break;
				case -4:
					p_strInnerMessage += "(strLastModifyDate == null)";
					break;
				case -5:
					p_strInnerMessage += "(��ѡ�����еļ�¼��)";
					return enmAutoTestResult.Succeed;
				case -6:
					p_strInnerMessage += "(����ѡ���ˡ�)";
					break;
				case -7:
					p_strInnerMessage += "(����ɾ����¼��)";
					return enmAutoTestResult.Succeed;
			}

			if(lngRes > 0)
				return enmAutoTestResult.Succeed;
			else
				return enmAutoTestResult.Failure;
		}

		/// <summary>
		/// ��ʾ���ܲ��ԣ����ṩ��
		/// </summary>
		/// <param name="p_objContentMaker">��������������</param>
		/// <param name="p_strInnerMessage">������Ϣ���</param>
		/// <returns>
		/// ִ�н��
		/// </returns>
		public enmAutoTestResult i_enmTestDisplay(com.digitalwave.Utility.clsTestContentMaker p_objContentMaker, out string p_strInnerMessage)
		{
			p_strInnerMessage = "";
			return enmAutoTestResult.Succeed;
		}
#endif
		#endregion

		private void m_nmuEventHour_ValueChanged(object sender, System.EventArgs e)
		{
			if(m_nmuEventHour.Value > 23 || m_nmuEventHour.Value < 0)
			{
				m_nmuEventHour.Value = DateTime.Now.Hour;
			}
		}

		private void m_nmuEventMinute_ValueChanged(object sender, System.EventArgs e)
		{
			if(m_nmuEventMinute.Value > 59 || m_nmuEventMinute.Value < 0)
			{
				m_nmuEventMinute.Value = DateTime.Now.Minute;
			}
		}


		private void m_rdbSkinTestBad_CheckedChanged(object sender, System.EventArgs e)
		{
			m_nmuSkinBadCount.Enabled = m_rdbSkinTestBad.Checked;
		}

		private void m_cboSkinTestMedicine_evtTextChanged(object sender, System.EventArgs e)
		{
			if(m_cboSkinTestMedicine.Text.ToLower() != "ppd")
			{
				m_nmuSkinBadCount.Value = 1;
			}
		}

		#region �жϵ�ǰ�û��Ƿ�����GE����
		private bool m_blnCurrApparatus()
		{
            if (m_ObjLastEmrPatientSession == null)
            {
                return false;
            }
			string strGENo="";
			bool blnIsExist=false;
            // 2019 - x
            //new clsBedGEMaintenanceDomain().m_mthGetBedGEinf(m_ObjLastEmrPatientSession.m_strBedID, ref strGENo, ref blnIsExist);	
			return blnIsExist;
		}
		#endregion �жϵ�ǰ�û��Ƿ�����GE����

		#region �Ӽ໤�ǻ�ȡ����
		private void GetData()
		{
			try
			{
				bool blnIsGE=m_blnCurrApparatus();

				clsCMSData objCMSData=null;
				clsVentilatorData objVentilatorData=null;

				//if(m_strInPatientID==null || m_strInPatientID=="" || m_strInPatientDate==null|| m_strInPatientDate=="")return;
				//��ȡ����������(��HEARTRATE�����ɣ���PULSERATE����������NPB���޴�Ѫѹ����NPBSYSTOLIC���޴�����ѹ����NPB_DIASTOLIC���޴�����ѹ����SPO21��Ѫ�����Ͷȣ���TEMP1�����£���RESPRATE������Ƶ�ʣ���O2CONCENTRATION������ENDEXPPRESSURE������EXPTIDALVOLUME������PEAKPRESSURE������BLOODNUM1��)
				string[] strTypeArry=new string[]{"PULSERATE","HEARTRATE","TEMP1","NPBSYSTOLIC","NPB_DIASTOLIC","RESPDETECTNUM"};//
				m_mthGetICUDataByTime(m_dtpGetDataTime.Value.ToString(),out objCMSData,out objVentilatorData,strTypeArry);

				if (!blnIsGE)
				{
					if (objCMSData != null)
					{
						if(m_cboPulseType.Text=="����:")
						{
							if(objCMSData.m_strPulseRate == null || objCMSData.m_strPulseRate.Trim().Length == 0)
								m_cboPulse.Text = "";
							else
								m_cboPulse.Text = objCMSData.m_strPulseRate.Trim().Substring(0,objCMSData.m_strPulseRate.Trim().IndexOf("."));
								//m_cboPulse.Text = objCMSData.m_strPulseRate.Substring(0,objCMSData.m_strPulseRate.Length-3);
						}
						else if(m_cboPulseType.Text=="����:")
						{
							if(objCMSData.m_strHeartRate == null || objCMSData.m_strHeartRate.Trim().Length == 0)
								m_cboPulse.Text = "";
							else
								m_cboPulse.Text = objCMSData.m_strHeartRate.Trim().Substring(0,objCMSData.m_strHeartRate.IndexOf("."));
						}

						//����
						if(objCMSData.m_strTemp1 == null || objCMSData.m_strTemp1.Trim().Length == 0)
							m_cboTemperature.Text="";
						else
							m_cboTemperature.Text=objCMSData.m_strTemp1.Trim();

						//����ѹ
						if(objCMSData.m_strNPBSYSTOLIC == null || objCMSData.m_strNPBSYSTOLIC.Trim().Length == 0)
							m_txtPressureSystolicValue.Text="";
						else
							m_txtPressureSystolicValue.Text=objCMSData.m_strNPBSYSTOLIC;

						//����ѹ
						if(objCMSData.m_strNPBDIASTOLIC == null || objCMSData.m_strNPBDIASTOLIC.Trim().Length == 0)
							m_txtPressureDiastolicValue.Text="";
						else
							m_txtPressureDiastolicValue.Text=objCMSData.m_strNPBDIASTOLIC;
						
						//����
						if(objCMSData.m_strRespDetectNum == null || objCMSData.m_strRespDetectNum.Trim().Length == 0)
							m_cboBreathValue.Text="";
						else
							m_cboBreathValue.Text=objCMSData.m_strRespDetectNum.Trim().Substring(0,objCMSData.m_strRespDetectNum.IndexOf("."));

					}
				}
				else
				{
					clsGECMSData objGECMSData=null;
					//objGECMSData=m_objICUGESimulateGetData.M_objNumericParam;
					if (objGECMSData==null)
						m_mthGetICUGEDataByTime(m_dtpGetDataTime.Value.ToString(),out objGECMSData);

					if (objGECMSData != null)
					{
						if(m_cboPulseType.Text=="����:")
						{
							if(objGECMSData.m_strPluse == null || objGECMSData.m_strPluse.Trim().Length == 0)
								m_cboPulse.Text = "";
							else
								m_cboPulse.Text = objGECMSData.m_strPluse;
						}
						else if(m_cboPulseType.Text=="����:")
						{
							if(objGECMSData.m_strHR  == null || objGECMSData.m_strHR.Trim().Length == 0)
								m_cboPulse.Text = "";
							else
								m_cboPulse.Text = objGECMSData.m_strHR;
						}

						//����
						if(objGECMSData.m_strTEMP1 == null || objGECMSData.m_strTEMP1.Trim().Length == 0)
							m_cboTemperature.Text="";
						else
							m_cboTemperature.Text=objGECMSData.m_strTEMP1;

						//����ѹ
						if(objGECMSData.m_strNBPSystolic == null || objGECMSData.m_strNBPSystolic.Trim().Length == 0)
							m_txtPressureSystolicValue.Text="";
						else
							m_txtPressureSystolicValue.Text=objGECMSData.m_strNBPSystolic;

						//����ѹ
						if(objGECMSData.m_strNBPDiastolic == null || objGECMSData.m_strNBPDiastolic.Trim().Length == 0)
							m_txtPressureDiastolicValue.Text="";
						else
							m_txtPressureDiastolicValue.Text=objGECMSData.m_strNBPDiastolic;
						
						//����
						if(objGECMSData.m_strRR == null || objGECMSData.m_strRR.Trim().Length == 0)
							m_cboBreathValue.Text="";
						else
							m_cboBreathValue.Text=objGECMSData.m_strRR;
					}
				}
			}
			catch
			{
			}
		}

		protected void m_mthGetICUDataByTime(string p_strStartTime,out clsCMSData p_objCMSData,out clsVentilatorData p_objVentilatorData,string[] p_strTypeArry)
		{
            p_objCMSData = null;
            p_objVentilatorData = null;
            if (m_ObjLastEmrPatientSession == null)
            {
                return;
            }
			//����ID�������λ����Ȼ�ᳬ��long�����Χ
			//string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4)+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            string strLongBedID = m_ObjLastEmrPatientSession.m_strAreaId + m_ObjLastEmrPatientSession.m_strBedID;
			strLongBedID=strLongBedID.PadRight(17,'0');
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUDataByTime("",p_dtmStart,out p_objCMSData,out p_objVentilatorData);
            // 2019 - x
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUNumericParmatByTime(p_strStartTime, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objCMSData, out p_objVentilatorData, p_strTypeArry);
		}

		#region ��ȡICU GE����
		private void m_mthGetICUGEDataByTime(string p_strStart,out clsGECMSData p_objGECMSData)
		{
            p_objGECMSData = null;
            if (m_ObjLastEmrPatientSession == null)
            {
                return;
            }

			p_objGECMSData=null;
            string strLongBedID = m_ObjLastEmrPatientSession.m_strAreaId + m_ObjLastEmrPatientSession.m_strBedID;
            strLongBedID = strLongBedID.PadRight(17, '0');
            //string strLongBedID = m_ObjLastEmrPatientSession.m_strDeptId + m_ObjLastEmrPatientSession.m_strAreaId.Substring(4) + MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID + m_ObjLastEmrPatientSession.m_strBedID;
            //			new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUGEDataByTime(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,p_dtmStart,out p_objGECMSData);
            //
            // 2019 - x
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUGEDataByTime(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, p_strStart, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objGECMSData);
		}
		#endregion ��ȡICU GE����

		#endregion �Ӽ໤�ǻ�ȡ����

		private void m_cmdGetDovueData_Click(object sender, System.EventArgs e)
		{
			GetData();

			#region old
//			if(m_objBaseCurrentPatient==null)return;
//
//			string [] strEMFC_IDArr = null;
//
//			if(m_rdbTemperatureType.Checked)
//			{
//				strEMFC_IDArr=new string[]{"100"};
//			}
//			else if(m_rdbDownTemperatureType.Checked)
//			{
//				strEMFC_IDArr=new string[]{"100"};
//			}
//			else if(m_rdbPulseType.Checked)
//			{
//				if(m_rdbPulse.Checked)
//				{
//					strEMFC_IDArr=new string[]{"44"};
//				}
//				else if(m_rdbHeartRate.Checked)
//				{
//					strEMFC_IDArr=new string[]{"40"};
//				}
//			}
//			else if(m_rdbPressureType.Checked)
//			{
//				strEMFC_IDArr=new string[]{"89","90","65","66"};
//			}
//
//			if(strEMFC_IDArr == null)
//				return;
//
//			string[] strResultArr;
//			long lngRes=m_objTrendDomain.m_lngGetDocvueResultArr(this.m_objBaseCurrentPatient.m_StrInPatientID,this.m_objBaseCurrentPatient.m_DtmLastInDate,strEMFC_IDArr,DateTime.Now,out strResultArr);
//			if(lngRes<=0)
//			{
//				switch(lngRes)
//				{
//					case (long)(iCareData.enmOperationResult.Not_permission) :
//						m_mthShowNotPermitted();break;
//					case (long)(iCareData.enmOperationResult.DB_Fail) :
//						m_mthShowDBError();break;
//				}
//			}
//			else 
//			{
//				if(m_rdbTemperatureType.Checked)
//				{
//					if(strResultArr[0] != null)
//					{
//						this.m_txtTemperatureValue.Text=strResultArr[0];
//					}
//				}
//				else if(m_rdbDownTemperatureType.Checked)
//				{
//					if(strResultArr[0] != null)
//					{
//						this.m_txtDownTemperatureValue.Text=strResultArr[0];
//					}
//				}
//				else if(m_rdbPulseType.Checked)
//				{
//					if(strResultArr[0] != null)
//					{
//						this.m_txtPulseValue.Text=strResultArr[0];			
//					}
//				}
//				else if(m_rdbPressureType.Checked)
//				{
//					if(strResultArr[0] != null)
//					{
//						this.m_txtPressureSystolicValue.Text=strResultArr[0];
//						this.m_txtPressureDiastolicValue.Text=strResultArr[1];
//					}
//					else if(strResultArr[3] != null)
//					{
//						this.m_txtPressureSystolicValue.Text=strResultArr[3];
//						this.m_txtPressureDiastolicValue.Text=strResultArr[4];
//					}
//				}						
//			}
			#endregion old

			#region Old Old
			//			this.m_txtPressureSystolicValue.Text="";
			//			this.m_txtPressureDiastolicValue.Text="";	
			//			this.m_txtDownTemperatureValue.Text="";		
			//			this.m_txtPulseValue.Text="";
			//
			//
			//			clsTrendDomain objDomain=new clsTrendDomain();
			//			string[] strEMFC_IDArr=new string[]{"89","90","100","40"};
			//				//new string[]{"100","40","40","89","90"};//���£����ʣ�����������ѹ������ѹ
			//			string[] strResultArr;
			//			long lngRes=objDomain.m_lngGetDocvueResultArr(this.m_objBaseCurrentPatient.m_StrInPatientID,this.m_objBaseCurrentPatient.m_DtmLastInDate,strEMFC_IDArr,DateTime.Now,out strResultArr);
			//			if(lngRes<=0)
			//			{
			//				switch(lngRes)
			//				{
			//					case (long)(iCareData.enmOperationResult.Not_permission) :
			//						m_mthShowNotPermitted();break;
			//					case (long)(iCareData.enmOperationResult.DB_Fail) :
			//						m_mthShowDBError();break;
			//				}
			//			}
			//			else 
			//			{
			//				this.m_txtPressureSystolicValue.Text=strResultArr[0];
			//				this.m_txtPressureDiastolicValue.Text=strResultArr[1];	
			//				this.m_txtDownTemperatureValue.Text=strResultArr[2];			
			//				this.m_txtPulseValue.Text=strResultArr[3];			
			//			}
			#endregion Old Old
		}		

		#region ���
		private string m_strCurrentOpenDate = "";



		private void m_cboOtherUnit_DropDown(object sender, System.EventArgs e)
		{
			m_cboOtherUnit.ClearItem();
	
			clsCommonUseValue[] objclsCommonUseValue=null;
			new clsCommonUseDomain().m_lngGetAllCommonUseValue(((int)enmCommonUseValue.ThreeMeasure_OtherUnit).ToString(),out objclsCommonUseValue);
			if(objclsCommonUseValue!=null && objclsCommonUseValue.Length>0)
			{
				for(int i=0;i<objclsCommonUseValue.Length;i++)
				{
					m_cboOtherUnit.AddItem(objclsCommonUseValue[i].m_strCommonUseValue);
				}				
			}
		}

		protected override string m_StrCurrentOpenDate
		{
			get
			{
				if(m_strCurrentOpenDate==null)
				{
					//					clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
					return "";
				}
				return m_strCurrentOpenDate;
			}
		}

		protected override bool m_BlnCanApprove
		{
			get
			{
				return false;
			}
		}		
		#endregion 

		/// <summary>
		/// ����Ҫ������ʾ
		/// </summary>
		protected override void m_mthAddFormStatusForClosingSave()
		{
		}


		/// <summary>
		/// �����������Լ�������ǰʱ��������һ��
		/// </summary>
		/// <param name="p_intIndex"></param>
		private void m_mthSyncPluseTime(int p_intIndex)
		{
			m_cboPulseTimeFlag.SelectedIndex = p_intIndex;
			m_cboBreathTime.SelectedIndex = p_intIndex;
			m_cboTemperatureTimeFlag.SelectedIndex = p_intIndex;
			m_cboDownBaseTime.SelectedIndex = p_intIndex;
		}

		/// <summary>
		/// ȡ���ʱ���
		/// </summary>
		/// <param name="p_dtmInput"></param>
		/// <returns></returns>
		private int m_intGetLastestTime(DateTime p_dtmInput)
		{
            //if(p_dtmInput.Hour < 2)
            //    return 5;
            //return ((p_dtmInput.Hour * 60 + p_dtmInput.Minute)-120) / (4*60);
            return (p_dtmInput.Hour * 60 + p_dtmInput.Minute) / (4 * 60);
		}

		private void m_ctlRecord_m_evtIsShortChanged(object sender, System.EventArgs e)
		{
			pnlRecordContain.AutoScroll = !m_ctlRecord.m_BlnIsShort;
		}
		
		private void m_picZoom_Click(object sender, System.EventArgs e)
		{
			if(m_ctlRecord.m_BlnIsShort)
			{
				m_picZoom.Image = m_imgZoom.Images[0];
				m_ctlRecord.m_BlnIsShort = false;
				m_tipPrompt.SetToolTip(m_picZoom,"��С");
			}
			else
			{
				m_picZoom.Image = m_imgZoom.Images[1];
				m_ctlRecord.m_BlnIsShort = true;
				m_tipPrompt.SetToolTip(m_picZoom,"�Ŵ�");
			}
		}

		private void pnlRecordContain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			pnlFocus.Location = new Point(e.X - 10,e.Y - 10);
			pnlFocus.Focus();
		}

		private void m_ctlRecord_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			pnlFocus.Location = new Point(e.X - 10,e.Y - 10);
			pnlFocus.Focus();
		}

		/// <summary>
		/// ��Ч�����ж�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_txtWhichWeek_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				int intNum = int.Parse(m_txtWhichWeek.Text);
				m_ctlRecord.m_IntWeekNum = intNum;
			}
			catch//(Exception ex)
			{
				//				clsPublicFunction.ShowInformationMessageBox("��������Ч��ֵ��");
				return;
			}
		}

		/// <summary>
		/// ֻ����������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_txtWhichWeek_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(!char.IsNumber(e.KeyChar) && ((int)e.KeyChar)!=8)
				e.Handled = true;				
		}

		private void m_cmdPreWeek_Click(object sender, System.EventArgs e)
		{
			m_ctlRecord.m_IntWeekNum--;
		}

		private void m_cmdNextWeek_Click(object sender, System.EventArgs e)
		{
			m_ctlRecord.m_IntWeekNum++;
		}

		private void m_cmdFirstWeek_Click(object sender, System.EventArgs e)
		{
			m_ctlRecord.m_IntWeekNum = 1;
		}

		private void m_cmdFinalWeek_Click(object sender, System.EventArgs e)
		{
			m_ctlRecord.m_IntWeekNum = m_ctlRecord.m_IntTotalWeek;
		}

		private void m_ctlRecord_m_evtWeekNumChanged(object sender, System.EventArgs e)
		{
			m_txtWhichWeek.Text = m_ctlRecord.m_IntWeekNum.ToString();
		}

		/// <summary>
		/// ����ĳЩ�ؼ��ɲ���
		/// </summary>
		/// <param name="p_ctlArr"></param>
		private void m_mthSetSomeControlEnable(Control[] p_ctlArr)
		{
			m_mthSetAllOperationDisable();

			for(int i = 0; i < p_ctlArr.Length; i++)
			{
				p_ctlArr[i].Enabled = true;
			}
		}

		/// <summary>
		/// ʹ���в���������
		/// </summary>
		private void m_mthSetAllOperationDisable()
		{
			for(int i = 0; i < m_grpOperation.Controls.Count; i++)
			{
				switch(m_grpOperation.Controls[i].GetType().Name)
				{
					case "Label" :
					case "CheckBox" :
						break;
					case "ctlComboBox" :
						ctlComboBox cbo = (ctlComboBox)m_grpOperation.Controls[i];
						cbo.Text = "";
						cbo.Enabled = false;
						break;
					case "NumericUpDown" : 
						NumericUpDown num = (NumericUpDown)m_grpOperation.Controls[i];
						num.Enabled = false;
						break;
					default :
						m_grpOperation.Controls[i].Text = "";
						m_grpOperation.Controls[i].Enabled = false;
						break;
				}		
			}
		}

		/// <summary>
		/// �����겡�˵�������Ϣ���궨λ
		/// </summary>
		protected override void m_mthSetFocusAfterSetPatientInfo()
		{
			this.m_cboEventType.Focus();
		}

		private void m_cmdGeneralNurse_Click(object sender, System.EventArgs e)
		{
			if(m_objBaseCurrentPatient == null)
			{
				clsPublicFunction.ShowInformationMessageBox("����ѡ���ˣ�");
				return;
			}
			m_mthSwitchToSpecialForm(m_objBaseCurrentPatient);
		}

		/// <summary>
		/// �л����ض���
		/// </summary>
		/// <param name="p_objPatient"></param>
		private void m_mthSwitchToSpecialForm(clsPatient p_objPatient)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainGeneralNurseRecord frmChild;
				for(int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
				{
					frmChild = this.MdiParent.MdiChildren[i] as frmMainGeneralNurseRecord;
					if(frmChild != null)
					{						
						frmChild.WindowState = FormWindowState.Normal;
						frmChild.WindowState = FormWindowState.Maximized;
						this.MdiParent.MdiChildren[i].Activate();
						this.Cursor=Cursors.Default;//Cursor���ر�ע��Ŷ
						return;
					}
				}
				frmChild = new frmMainGeneralNurseRecord();
				frmChild.MdiParent = this.MdiParent;
				frmChild.m_mthDisableSelectPatient(false);
				frmChild.Show(); 
				frmChild.m_mthSetPatient(p_objPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void m_ctlRecord_Click(object sender, System.EventArgs e)
		{
			pnlFocus.Location = new Point(50,50);
			pnlFocus.Focus();
		}

		/// <summary>
		/// ���δ�ӡǰ��ʾ����
		/// </summary>
		protected override DialogResult m_dlgHandleSaveBeforePrint()
		{
			return DialogResult.None;
		}

		private void frmThreeMeasureRecord_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//m_objICUGESimulateGetData.m_mthStopReceiveData();
		}
        /// <summary>
        /// �����ò��˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthOnlySetPatientInfo(clsPatient p_objSelectedPatient)
        {
            //lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(cboCreateDate.Items.Count-cboCreateDate.SelectedIndex-1).m_ObjPeopleInfo.m_StrSex;
            //lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(cboCreateDate.Items.Count - cboCreateDate.SelectedIndex-1).m_ObjPeopleInfo.m_StrAge;

         }
		/// <summary>
		/// �ı���Ժ�����¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCreateDate_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			try
			{
				//ֻ���޸ĵ�ǰ��Ժ��¼
                /*��Ҫ���������ô��
				if (cboCreateDate.SelectedIndex==0)
					m_grpOperation.Enabled=true;
				else
                    m_grpOperation.Enabled = false;
                */
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objBaseCurrentPatient);

                m_objBaseCurrentPatient.m_ObjPeopleInfo = m_objBaseCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(cboCreateDate.Items.Count - cboCreateDate.SelectedIndex - 1).m_ObjPeopleInfo;

                cboCreateDate.Tag = m_objBaseCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(cboCreateDate.Items.Count - cboCreateDate.SelectedIndex - 1).m_DtmEMRInDate;

                if (string.IsNullOrEmpty(m_objBaseCurrentPatient.m_StrRegisterId))
                {
                    string strRegisterID = string.Empty;
                    long lngRes = new clsPublicDomain().m_lngGetRegisterID(m_objBaseCurrentPatient.m_StrPatientID, Convert.ToDateTime(cboCreateDate.Text).ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                }
                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    m_mthResetAll();
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

				if (blnComboxEvent)
				{
					//���
					m_mthResetAll();
					//��ȡ��¼
					
					m_mthSetPatientFormInfo(objTempPatient);
				}

				
			}
			catch (Exception exp)
			{
				string strMsg=exp.Message;
			}
		}
		/// <summary>
		/// ���û���������Ϣ��override��
		/// </summary>
		/// <param name="p_objSelectedPatient"></param>
		protected override void m_mthSetPatientBaseInfo(clsPatient p_objSelectedPatient)
		{
			base.m_mthSetPatientBaseInfo (p_objSelectedPatient);
			//���
			cboCreateDate.Items.Clear();
			//��Ӳ�ѯ������Ժʱ�䵽ʱ������
			try
			{
				blnComboxEvent=false;
				objTempPatient=p_objSelectedPatient;
				for(int i=p_objSelectedPatient.m_ObjInBedInfo.m_intGetSessionCount()-1;i>=0;i--)
				{	
					cboCreateDate.Items.Add(p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmHISInDate.ToString("yyyy-MM-dd HH:mm:ss"));
				}
				if (cboCreateDate.Items.Count>0)
					cboCreateDate.SelectedIndex=0;
				
			}
			catch (Exception ex)
			{
				string strMsg=ex.Message;
			}
			finally
			{
				blnComboxEvent=true;
			}

		}
        private void m_cboOtherItem_evtAddItem(object sender, System.EventArgs e)
        {
            //��������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventAddItem(sender, e, "1");
        }

        private void m_cboOtherItem_evtDelItem(object sender, System.EventArgs e)
        {
            //ɾ�������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventDeleteItem(sender, e, "1");
        }

        private void m_cboOtherItem_evtModifyItem(object sender, System.EventArgs e)
        {
            //�޸������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventModifyItem(sender, e, "1");
        }

        private void m_cboSkinTestMedicine_evtAddItem(object sender, System.EventArgs e)
        {
            //��������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventAddItem(sender, e, "0");
        }

        private void m_cboSkinTestMedicine_evtDelItem(object sender, System.EventArgs e)
        {
            //ɾ�������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventDeleteItem(sender, e, "0");
        }

        private void m_cboSkinTestMedicine_evtModifyItem(object sender, System.EventArgs e)
        {
            //�޸������¼
            new clsThreeMeasureEventManagerDomainGN().m_mthEventModifyItem(sender, e, "0");
        }
        private void m_cboOtherItem_DropDown(object sender, System.EventArgs e)
        {
            new clsThreeMeasureEventManagerDomainGN().m_cmthEventItem_DropDown(sender, e, "1");
        }

        private void m_cboSkinTestMedicine_DropDown(object sender, System.EventArgs e)
        {
            new clsThreeMeasureEventManagerDomainGN().m_cmthEventItem_DropDown(sender, e, "0");
        }
        protected override void m_mthAssociateComboBoxItemEvent(Control p_ctlParent)
        { }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            try
            {
                
                m_mthClear();
                if (p_objSelectedSession == null)
                {
                    return;
                }

                //ֻ���޸ĵ�ǰ��Ժ��¼
                if (p_objSelectedSession.m_dtmEMRInpatientDate < m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate)
                {
                    m_grpOperation.Enabled = false;
                }
                else
                    m_grpOperation.Enabled = true;

                m_strInPatientDate = m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objBaseCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objBaseCurrentPatient);

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    m_mthResetAll();
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }
               
                //���
                m_mthResetAll();
                //��ȡ��¼

                m_mthSetPatientFormInfo(m_objBaseCurrentPatient);
            }
            catch (Exception exp)
            {
                string strMsg = exp.Message;
            }
        }

        private void m_cboTimeFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_cboTimeFlag.SelectedIndex)
            {
                case 0:
                    m_nmuEventHour.Value = 0;
                    break;
                case 1:
                    m_nmuEventHour.Value = 4;
                    break;
                case 2:
                    m_nmuEventHour.Value = 8;
                    break;
                case 3:
                    m_nmuEventHour.Value = 12;
                    break;
                case 4:
                    m_nmuEventHour.Value = 16;
                    break;
                case 5:
                    m_nmuEventHour.Value = 20;
                    break;
                default:
                    break;
            }
        }
	}
}

