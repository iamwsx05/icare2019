using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Xml; 
//using com.digitalwave.DepartmentService;
using System.Text;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;  
using weCare.Core.Entity;
//using com.digitalwave.iCare.middletier.HRPService;
//using Oracle.DataAccess.Client;
using System.Data;
using iCare.ICU.Evaluation;

namespace iCare    
{ 
	/// <summary>
	/// HRPExplorer ��ժҪ˵����
	/// </summary>
	public class Form_HRPExplorer : iCareBaseForm.frmBaseForm,PublicFunction
	{
		private DataTable dtbPatientMessages=new DataTable();

		#region Variable
		private System.Windows.Forms.TreeView trvExplorer;
		public  System.Windows.Forms.ListView lsvExplorerMid;
		private System.Windows.Forms.ImageList imgTreeView;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem miAddArea;
		private System.Windows.Forms.MenuItem miDelArea;
		private System.Windows.Forms.MenuItem miReArea;
		private System.Windows.Forms.MenuItem miAddRoom;
		private System.Windows.Forms.MenuItem miDelRoom;
		private System.Windows.Forms.MenuItem miMoveRoom;
		private System.Windows.Forms.MenuItem miReRoom;
		private System.Windows.Forms.MenuItem miAddBed;
		private System.Windows.Forms.MenuItem miMoveBed;
		private System.Windows.Forms.MenuItem miDelBed;
		private System.Windows.Forms.MenuItem miReBed;
		private System.Windows.Forms.ContextMenu ctmInPatient;
		private System.Windows.Forms.ContextMenu ctmHRPExplorer;
		private System.Windows.Forms.Panel pnlPatientInfo;
		private System.Windows.Forms.Label lblRoomTitle;
		private System.Windows.Forms.Label lblPatientTitle;
		private System.Windows.Forms.Label lblTitleDept;
		private System.Windows.Forms.Label lblTitleRoom;
		private System.Windows.Forms.Label lblTitleArea;
		private System.Windows.Forms.Label lblTitleBed;
		private System.Windows.Forms.Label lblDeptName;
		private System.Windows.Forms.Label lblRoomName;
		private System.Windows.Forms.Label lblAreaName;
		private System.Windows.Forms.Label lblBedName;
		private System.Windows.Forms.Label lblTitleCardNo;
		private System.Windows.Forms.Label lblCardNo;
		private System.Windows.Forms.Label lblInHospitalNo;
		private System.Windows.Forms.Label lblTitleInHospitalNo;
		private System.Windows.Forms.Label lblTitleName;
		private System.Windows.Forms.Label lblSex;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblTitleSex;
		private System.Windows.Forms.Label lblAge;
		private System.Windows.Forms.Label lblTitleAge;
		private System.Windows.Forms.Label lblTitleMarried;
		private System.Windows.Forms.Label lblMarried;
		private System.Windows.Forms.Label lblLinkManName;
		private System.Windows.Forms.Label lblTitleLinkManName;
		private System.Windows.Forms.Label lblTitleLinkManPhone;
		private System.Windows.Forms.Label lblInHospitalDate;
		private System.Windows.Forms.Label lblTitleInHospitalDate;
		private System.Windows.Forms.Label lblICUEquipments;
		private System.Windows.Forms.Label lblLinkManPhone;
		private System.Windows.Forms.Label lblTitleEquipInfo;
		private System.Windows.Forms.Timer timRefreshEmployee;
		private System.Windows.Forms.Label lblEmployeeInfo;
		private System.Windows.Forms.TextBox txtEmployeeDetailInfo;
		private System.Windows.Forms.MenuItem mniDoctorWorkStationForEar;
		private System.Windows.Forms.MenuItem mniNurseWorkStationForEar;
		private System.Windows.Forms.MenuItem mniMainRecord;
		private System.Windows.Forms.MenuItem mniPatientProcessRecord;
		private System.Windows.Forms.MenuItem mniOperationRecordDoct;
		private System.Windows.Forms.MenuItem mniOperationAgreed;
		private System.Windows.Forms.MenuItem mniOperationSummary;
		private System.Windows.Forms.MenuItem mniQC;
		private System.Windows.Forms.MenuItem mnimnuApplyRecord;
		private System.Windows.Forms.MenuItem mniSPECT;
		private System.Windows.Forms.MenuItem mniHighOxygen;
		private System.Windows.Forms.MenuItem mniBultransonic;
		private System.Windows.Forms.MenuItem mniCTCheckOrder;
		private System.Windows.Forms.MenuItem mniXRay;
		private System.Windows.Forms.MenuItem mniPathologyOrgCheckOrder;
		private System.Windows.Forms.MenuItem mniMRIOrder;
		private System.Windows.Forms.MenuItem mniThreeMeasureRecordNurse;
		private System.Windows.Forms.MenuItem mniEvaluateNurse;
		private System.Windows.Forms.MenuItem mniTendRecordNurse;
		private System.Windows.Forms.MenuItem mniOperationRecordNurse;
		private System.Windows.Forms.MenuItem mniOperationQtyNurser;
		private System.Windows.Forms.MenuItem mniGeneralTendRecordNurse;
		private System.Windows.Forms.MenuItem mniWatchItemNurse;
		private System.Windows.Forms.MenuItem mnifrmPICUShiftInForm;
		private System.Windows.Forms.MenuItem mnifrmPICUShiftOutForm;
		private System.Windows.Forms.MenuItem mniICUTendRecord;
		private System.Windows.Forms.ColumnHeader clmInPatientID;
		private System.Windows.Forms.ColumnHeader clmBedNO;
		private System.Windows.Forms.Panel m_pnlHeader;
		protected com.digitalwave.Utility.Controls.ctlComboBox m_cboArea;
		protected System.Windows.Forms.Label lblAreaTitle;
		protected System.Windows.Forms.Label lblDeptTitle;
		private System.Windows.Forms.Splitter m_sptLeft;
		private System.Windows.Forms.PictureBox m_picExpand;
		protected com.digitalwave.Utility.Controls.ctlComboBox m_cboDept;
		private System.Windows.Forms.MenuItem mnuInPatientCaseHistory;
		private System.Windows.Forms.MenuItem mnuInPatientCaseMode2;
		private System.Windows.Forms.MenuItem mniDirectionAnalisys;
		private System.Windows.Forms.MenuItem mnuGrade;
		private System.Windows.Forms.MenuItem mnuSIRS;
		private System.Windows.Forms.MenuItem mnuGlasgow;
		private System.Windows.Forms.MenuItem mnuLung;
		private System.Windows.Forms.MenuItem mnuNewBaby;
		private System.Windows.Forms.MenuItem mnuLittleBaby;
		private System.Windows.Forms.MenuItem mnuAPACHEII;
		private System.Windows.Forms.MenuItem mnuAPACHEIII;
		private System.Windows.Forms.MenuItem mnuTISS28;
		private System.Windows.Forms.MenuItem mniLabAnalysisOrder;
		private System.Windows.Forms.MenuItem mniLabCheckReport;
		private System.Windows.Forms.ImageList imgListView;

		/// <summary>
		/// ��ǰ����
		/// </summary>
		public string m_strCurrentDept = "";
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mniOutHospital;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mniPatientInfoManage;
		private System.Windows.Forms.MenuItem mniImageBookingSearch;
		private System.Windows.Forms.MenuItem mniImageReport;
		private System.Windows.Forms.MenuItem mniEKGOrder;
		private System.Windows.Forms.MenuItem mniNuclearOrder;
		private System.Windows.Forms.MenuItem mniPSGOrder;
		private System.Windows.Forms.MenuItem menuItem7;
 
		public TreeExpandTag stuTreeExpandTag;

		public struct TreeExpandTag
		{
			public int intclinic;//����
			public int intInHospital;
			public int intMedicineTreatment;//ҽ��
			public int intMedicineStorage;
			public int intTools;
		}
		/// <summary>
		/// ��ȡ������Ϣ�ķ�ʽ.0,��ȡȫ��;1,��ȡ��ǰ��½�����ڵĲ���.
		/// </summary>
		private int m_intLoadFlag;
		/// <summary>
		/// ���Ǻ��ID
		/// </summary>
		public const string c_strDeptID_ENTdepartment="1110000";
		/// <summary>
		/// ����ICU����ID
		/// </summary>
		public const string c_strDeptID_ICUCenterDepartment="1560000";
		/// <summary>
		/// �����м�����
		/// </summary>
		private clsDepartmentManager m_objDepartmentManager;
		/// <summary>
		/// Ȩ��ϵͳ���м��
		/// </summary>
		private clsRoleManager m_objRoleManager;
		private ToolTip m_ttpBed=new ToolTip();
		/// <summary>
		/// �Ƿ�չ��
		/// </summary>
		private bool m_blnIsExpand=true;
		private Bitmap imgUserclose;
		private Bitmap imgUseropen;	

		/// <summary>
		/// ѡ�нڵ�ʱ��ѯ�¼�Ҫ��Ҫ����
		/// </summary>
		private bool m_blnCanSeachEventTakePlace=false;
		public bool m_blnChangedDept = true;
		private System.Windows.Forms.MenuItem mnuCurrentPatient;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem6;
		private clsPrivilegeInfo[] objPIArr;
		#endregion

		public Form_HRPExplorer()
		{
			InitializeComponent();
			//			m_mthInit();
			trvExplorer.Visible=false;

			//Add By jli in 2004-11-29
			for(int i=0;i<15;i++)
			{
				DataColumn dc=new DataColumn();
				dc.ColumnName=i.ToString().Trim();
				dc.DataType=System.Type.GetType("System.String");
				this.dtbPatientMessages.Columns.Add(dc);
			}
			//Add End
		}
				
		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
	
		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form_HRPExplorer));
			this.trvExplorer = new System.Windows.Forms.TreeView();
			this.ctmInPatient = new System.Windows.Forms.ContextMenu();
			this.miAddArea = new System.Windows.Forms.MenuItem();
			this.miDelArea = new System.Windows.Forms.MenuItem();
			this.miReArea = new System.Windows.Forms.MenuItem();
			this.miAddRoom = new System.Windows.Forms.MenuItem();
			this.miDelRoom = new System.Windows.Forms.MenuItem();
			this.miMoveRoom = new System.Windows.Forms.MenuItem();
			this.miReRoom = new System.Windows.Forms.MenuItem();
			this.miAddBed = new System.Windows.Forms.MenuItem();
			this.miDelBed = new System.Windows.Forms.MenuItem();
			this.miMoveBed = new System.Windows.Forms.MenuItem();
			this.miReBed = new System.Windows.Forms.MenuItem();
			this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
			this.m_sptLeft = new System.Windows.Forms.Splitter();
			this.lsvExplorerMid = new System.Windows.Forms.ListView();
			this.clmBedNO = new System.Windows.Forms.ColumnHeader();
			this.clmInPatientID = new System.Windows.Forms.ColumnHeader();
			this.imgListView = new System.Windows.Forms.ImageList(this.components);
			this.ctmHRPExplorer = new System.Windows.Forms.ContextMenu();
			this.mnuCurrentPatient = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.mnuInPatientCaseHistory = new System.Windows.Forms.MenuItem();
			this.mniPatientProcessRecord = new System.Windows.Forms.MenuItem();
			this.mniOperationSummary = new System.Windows.Forms.MenuItem();
			this.mniOperationRecordDoct = new System.Windows.Forms.MenuItem();
			this.mniOutHospital = new System.Windows.Forms.MenuItem();
			this.mniEvaluateNurse = new System.Windows.Forms.MenuItem();
			this.mniThreeMeasureRecordNurse = new System.Windows.Forms.MenuItem();
			this.mniGeneralTendRecordNurse = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.mniDoctorWorkStationForEar = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuInPatientCaseMode2 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mniOperationAgreed = new System.Windows.Forms.MenuItem();
			this.mnifrmPICUShiftInForm = new System.Windows.Forms.MenuItem();
			this.mnifrmPICUShiftOutForm = new System.Windows.Forms.MenuItem();
			this.mniMainRecord = new System.Windows.Forms.MenuItem();
			this.mniQC = new System.Windows.Forms.MenuItem();
			this.mnimnuApplyRecord = new System.Windows.Forms.MenuItem();
			this.mniBultransonic = new System.Windows.Forms.MenuItem();
			this.mniCTCheckOrder = new System.Windows.Forms.MenuItem();
			this.mniXRay = new System.Windows.Forms.MenuItem();
			this.mniSPECT = new System.Windows.Forms.MenuItem();
			this.mniHighOxygen = new System.Windows.Forms.MenuItem();
			this.mniPathologyOrgCheckOrder = new System.Windows.Forms.MenuItem();
			this.mniMRIOrder = new System.Windows.Forms.MenuItem();
			this.mniEKGOrder = new System.Windows.Forms.MenuItem();
			this.mniNuclearOrder = new System.Windows.Forms.MenuItem();
			this.mniPSGOrder = new System.Windows.Forms.MenuItem();
			this.mniLabAnalysisOrder = new System.Windows.Forms.MenuItem();
			this.mniLabCheckReport = new System.Windows.Forms.MenuItem();
			this.mniImageReport = new System.Windows.Forms.MenuItem();
			this.mniImageBookingSearch = new System.Windows.Forms.MenuItem();
			this.mnuGrade = new System.Windows.Forms.MenuItem();
			this.mnuSIRS = new System.Windows.Forms.MenuItem();
			this.mnuGlasgow = new System.Windows.Forms.MenuItem();
			this.mnuLung = new System.Windows.Forms.MenuItem();
			this.mnuNewBaby = new System.Windows.Forms.MenuItem();
			this.mnuLittleBaby = new System.Windows.Forms.MenuItem();
			this.mnuAPACHEII = new System.Windows.Forms.MenuItem();
			this.mnuAPACHEIII = new System.Windows.Forms.MenuItem();
			this.mnuTISS28 = new System.Windows.Forms.MenuItem();
			this.mniDirectionAnalisys = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mniNurseWorkStationForEar = new System.Windows.Forms.MenuItem();
			this.mniPatientInfoManage = new System.Windows.Forms.MenuItem();
			this.mniWatchItemNurse = new System.Windows.Forms.MenuItem();
			this.mniTendRecordNurse = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mniICUTendRecord = new System.Windows.Forms.MenuItem();
			this.mniOperationRecordNurse = new System.Windows.Forms.MenuItem();
			this.mniOperationQtyNurser = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.pnlPatientInfo = new System.Windows.Forms.Panel();
			this.txtEmployeeDetailInfo = new System.Windows.Forms.TextBox();
			this.lblEmployeeInfo = new System.Windows.Forms.Label();
			this.lblInHospitalNo = new System.Windows.Forms.Label();
			this.lblAreaName = new System.Windows.Forms.Label();
			this.lblBedName = new System.Windows.Forms.Label();
			this.lblSex = new System.Windows.Forms.Label();
			this.lblMarried = new System.Windows.Forms.Label();
			this.lblLinkManPhone = new System.Windows.Forms.Label();
			this.lblInHospitalDate = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.lblCardNo = new System.Windows.Forms.Label();
			this.lblRoomName = new System.Windows.Forms.Label();
			this.lblDeptName = new System.Windows.Forms.Label();
			this.lblTitleDept = new System.Windows.Forms.Label();
			this.lblRoomTitle = new System.Windows.Forms.Label();
			this.lblPatientTitle = new System.Windows.Forms.Label();
			this.lblTitleEquipInfo = new System.Windows.Forms.Label();
			this.lblTitleRoom = new System.Windows.Forms.Label();
			this.lblTitleArea = new System.Windows.Forms.Label();
			this.lblTitleBed = new System.Windows.Forms.Label();
			this.lblTitleName = new System.Windows.Forms.Label();
			this.lblTitleInHospitalNo = new System.Windows.Forms.Label();
			this.lblTitleSex = new System.Windows.Forms.Label();
			this.lblTitleMarried = new System.Windows.Forms.Label();
			this.lblLinkManName = new System.Windows.Forms.Label();
			this.lblTitleLinkManName = new System.Windows.Forms.Label();
			this.lblAge = new System.Windows.Forms.Label();
			this.lblTitleAge = new System.Windows.Forms.Label();
			this.lblTitleLinkManPhone = new System.Windows.Forms.Label();
			this.lblTitleInHospitalDate = new System.Windows.Forms.Label();
			this.lblTitleCardNo = new System.Windows.Forms.Label();
			this.lblICUEquipments = new System.Windows.Forms.Label();
			this.timRefreshEmployee = new System.Windows.Forms.Timer(this.components);
			this.m_pnlHeader = new System.Windows.Forms.Panel();
			this.m_picExpand = new System.Windows.Forms.PictureBox();
			this.m_cboArea = new com.digitalwave.Utility.Controls.ctlComboBox();
			this.lblAreaTitle = new System.Windows.Forms.Label();
			this.m_cboDept = new com.digitalwave.Utility.Controls.ctlComboBox();
			this.lblDeptTitle = new System.Windows.Forms.Label();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.pnlPatientInfo.SuspendLayout();
			this.m_pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// trvExplorer
			// 
			this.trvExplorer.BackColor = System.Drawing.SystemColors.Control;
			this.trvExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.trvExplorer.ContextMenu = this.ctmInPatient;
			this.trvExplorer.Cursor = System.Windows.Forms.Cursors.Default;
			this.trvExplorer.Dock = System.Windows.Forms.DockStyle.Left;
			this.trvExplorer.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.trvExplorer.ForeColor = System.Drawing.Color.White;
			this.trvExplorer.HideSelection = false;
			this.trvExplorer.HotTracking = true;
			this.trvExplorer.ImageList = this.imgTreeView;
			this.trvExplorer.Indent = 22;
			this.trvExplorer.Location = new System.Drawing.Point(0, 0);
			this.trvExplorer.Name = "trvExplorer";
			this.trvExplorer.Size = new System.Drawing.Size(260, 733);
			this.trvExplorer.TabIndex = 0;
			this.trvExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvExplorer_AfterSelect);
			// 
			// ctmInPatient
			// 
			this.ctmInPatient.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.miAddArea,
																						 this.miDelArea,
																						 this.miReArea,
																						 this.miAddRoom,
																						 this.miDelRoom,
																						 this.miMoveRoom,
																						 this.miReRoom,
																						 this.miAddBed,
																						 this.miDelBed,
																						 this.miMoveBed,
																						 this.miReBed});
			// 
			// miAddArea
			// 
			this.miAddArea.Index = 0;
			this.miAddArea.Text = "��Ӳ���";
			// 
			// miDelArea
			// 
			this.miDelArea.Index = 1;
			this.miDelArea.Text = "ɾ������";
			// 
			// miReArea
			// 
			this.miReArea.Index = 2;
			this.miReArea.Text = "����������";
			// 
			// miAddRoom
			// 
			this.miAddRoom.Index = 3;
			this.miAddRoom.Text = "��Ӳ���";
			// 
			// miDelRoom
			// 
			this.miDelRoom.Index = 4;
			this.miDelRoom.Text = "ɾ������";
			// 
			// miMoveRoom
			// 
			this.miMoveRoom.Index = 5;
			this.miMoveRoom.Text = "�ƶ�����";
			// 
			// miReRoom
			// 
			this.miReRoom.Index = 6;
			this.miReRoom.Text = "����������";
			// 
			// miAddBed
			// 
			this.miAddBed.Index = 7;
			this.miAddBed.Text = "��Ӳ���";
			// 
			// miDelBed
			// 
			this.miDelBed.Index = 8;
			this.miDelBed.Text = "ɾ������";
			// 
			// miMoveBed
			// 
			this.miMoveBed.Index = 9;
			this.miMoveBed.Text = "�ƶ�����";
			// 
			// miReBed
			// 
			this.miReBed.Index = 10;
			this.miReBed.Text = "����������";
			// 
			// imgTreeView
			// 
			this.imgTreeView.ImageSize = new System.Drawing.Size(17, 17);
			this.imgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeView.ImageStream")));
			this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// m_sptLeft
			// 
			this.m_sptLeft.BackColor = System.Drawing.SystemColors.Control;
			this.m_sptLeft.Location = new System.Drawing.Point(260, 0);
			this.m_sptLeft.Name = "m_sptLeft";
			this.m_sptLeft.Size = new System.Drawing.Size(4, 733);
			this.m_sptLeft.TabIndex = 1;
			this.m_sptLeft.TabStop = false;
			// 
			// lsvExplorerMid
			// 
			this.lsvExplorerMid.BackColor = System.Drawing.SystemColors.Control;
			this.lsvExplorerMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lsvExplorerMid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.clmBedNO,
																							 this.clmInPatientID});
			this.lsvExplorerMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvExplorerMid.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lsvExplorerMid.ForeColor = System.Drawing.Color.Black;
			this.lsvExplorerMid.FullRowSelect = true;
			this.lsvExplorerMid.GridLines = true;
			this.lsvExplorerMid.HoverSelection = true;
			this.lsvExplorerMid.LargeImageList = this.imgListView;
			this.lsvExplorerMid.Location = new System.Drawing.Point(264, 0);
			this.lsvExplorerMid.MultiSelect = false;
			this.lsvExplorerMid.Name = "lsvExplorerMid";
			this.lsvExplorerMid.Size = new System.Drawing.Size(764, 733);
			this.lsvExplorerMid.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lsvExplorerMid.TabIndex = 0;
			this.lsvExplorerMid.Click += new System.EventHandler(this.lsvExplorerMid_Click);
			this.lsvExplorerMid.DoubleClick += new System.EventHandler(this.lsvExplorerMid_DoubleClick);
			this.lsvExplorerMid.MouseHover += new System.EventHandler(this.lsvExplorerMid_MouseHover);
			this.lsvExplorerMid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lsvExplorerMid_MouseMove);
			// 
			// clmInPatientID
			// 
			this.clmInPatientID.Text = "סԺ��";
			this.clmInPatientID.Width = 150;
			// 
			// imgListView
			// 
			this.imgListView.ImageSize = new System.Drawing.Size(65, 55);
			this.imgListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListView.ImageStream")));
			this.imgListView.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ctmHRPExplorer
			// 
			this.ctmHRPExplorer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuCurrentPatient,
																						   this.menuItem8,
																						   this.mnuInPatientCaseHistory,
																						   this.mniPatientProcessRecord,
																						   this.mniOperationSummary,
																						   this.mniOperationRecordDoct,
																						   this.mniOutHospital,
																						   this.menuItem9,
																						   this.mniEvaluateNurse,
																						   this.mniThreeMeasureRecordNurse,
																						   this.mniGeneralTendRecordNurse,
																						   this.menuItem7,
																						   this.mniDoctorWorkStationForEar,
																						   this.mniNurseWorkStationForEar});
			this.ctmHRPExplorer.Popup += new System.EventHandler(this.ctmHRPExplorer_Popup);
			// 
			// mnuCurrentPatient
			// 
			this.mnuCurrentPatient.Index = 0;
			this.mnuCurrentPatient.Text = "��Ϊ��ǰ����";
			this.mnuCurrentPatient.Click += new System.EventHandler(this.mnuCurrentPatient_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "-";
			this.menuItem8.Visible = false;
			// 
			// mnuInPatientCaseHistory
			// 
			this.mnuInPatientCaseHistory.Index = 2;
			this.mnuInPatientCaseHistory.Text = "סԺ����";
			this.mnuInPatientCaseHistory.Visible = false;
			this.mnuInPatientCaseHistory.Click += new System.EventHandler(this.mnuInPatientCaseHistory_Click);
			// 
			// mniPatientProcessRecord
			// 
			this.mniPatientProcessRecord.Index = 3;
			this.mniPatientProcessRecord.Text = "���̼�¼";
			this.mniPatientProcessRecord.Visible = false;
			this.mniPatientProcessRecord.Click += new System.EventHandler(this.mniPatientProcessRecord_Click);
			// 
			// mniOperationSummary
			// 
			this.mniOperationSummary.Index = 4;
			this.mniOperationSummary.Text = "��ǰС��";
			this.mniOperationSummary.Visible = false;
			this.mniOperationSummary.Click += new System.EventHandler(this.mniOperationSummary_Click);
			// 
			// mniOperationRecordDoct
			// 
			this.mniOperationRecordDoct.Index = 5;
			this.mniOperationRecordDoct.Text = "������¼��";
			this.mniOperationRecordDoct.Visible = false;
			this.mniOperationRecordDoct.Click += new System.EventHandler(this.mniOperationRecordDoct_Click);
			// 
			// mniOutHospital
			// 
			this.mniOutHospital.Index = 6;
			this.mniOutHospital.Text = "��Ժ��¼";
			this.mniOutHospital.Visible = false;
			this.mniOutHospital.Click += new System.EventHandler(this.mniOutHospital_Click);
			// 
			// mniEvaluateNurse
			// 
			this.mniEvaluateNurse.Index = 8;
			this.mniEvaluateNurse.Text = "��Ժ��������";
			this.mniEvaluateNurse.Visible = false;
			this.mniEvaluateNurse.Click += new System.EventHandler(this.mniEvaluateNurse_Click);
			// 
			// mniThreeMeasureRecordNurse
			// 
			this.mniThreeMeasureRecordNurse.Index = 9;
			this.mniThreeMeasureRecordNurse.Text = "�� �� ��";
			this.mniThreeMeasureRecordNurse.Visible = false;
			this.mniThreeMeasureRecordNurse.Click += new System.EventHandler(this.mniThreeMeasureRecordNurse_Click);
			// 
			// mniGeneralTendRecordNurse
			// 
			this.mniGeneralTendRecordNurse.Index = 10;
			this.mniGeneralTendRecordNurse.Text = "һ�㻤���¼";
			this.mniGeneralTendRecordNurse.Visible = false;
			this.mniGeneralTendRecordNurse.Click += new System.EventHandler(this.mniGeneralTendRecordNurse_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 11;
			this.menuItem7.Text = "-";
			this.menuItem7.Visible = false;
			// 
			// mniDoctorWorkStationForEar
			// 
			this.mniDoctorWorkStationForEar.Index = 12;
			this.mniDoctorWorkStationForEar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									   this.menuItem1,
																									   this.mnimnuApplyRecord,
																									   this.mnuGrade,
																									   this.mniDirectionAnalisys,
																									   this.menuItem3});
			this.mniDoctorWorkStationForEar.Text = "ҽ������վ";
			this.mniDoctorWorkStationForEar.Visible = false;
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuInPatientCaseMode2,
																					  this.menuItem2,
																					  this.mniOperationAgreed,
																					  this.mnifrmPICUShiftInForm,
																					  this.mnifrmPICUShiftOutForm,
																					  this.mniMainRecord,
																					  this.mniQC});
			this.menuItem1.Text = "��������";
			// 
			// mnuInPatientCaseMode2
			// 
			this.mnuInPatientCaseMode2.Index = 0;
			this.mnuInPatientCaseMode2.Text = "סԺ����ģʽ2";
			this.mnuInPatientCaseMode2.Visible = false;
			this.mnuInPatientCaseMode2.Click += new System.EventHandler(this.mnuInPatientCaseMode2_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "�����¼";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// mniOperationAgreed
			// 
			this.mniOperationAgreed.Index = 2;
			this.mniOperationAgreed.Text = "����֪��ͬ����";
			this.mniOperationAgreed.Click += new System.EventHandler(this.mniOperationAgreed_Click);
			// 
			// mnifrmPICUShiftInForm
			// 
			this.mnifrmPICUShiftInForm.Index = 3;
			this.mnifrmPICUShiftInForm.Text = "ICUת���¼";
			this.mnifrmPICUShiftInForm.Click += new System.EventHandler(this.mnifrmPICUShiftInForm_Click);
			// 
			// mnifrmPICUShiftOutForm
			// 
			this.mnifrmPICUShiftOutForm.Index = 4;
			this.mnifrmPICUShiftOutForm.Text = "ICUת����¼";
			this.mnifrmPICUShiftOutForm.Click += new System.EventHandler(this.mnifrmPICUShiftOutForm_Click);
			// 
			// mniMainRecord
			// 
			this.mniMainRecord.Index = 5;
			this.mniMainRecord.Text = "סԺ������ҳ";
			this.mniMainRecord.Click += new System.EventHandler(this.mniMainRecord_Click);
			// 
			// mniQC
			// 
			this.mniQC.Index = 6;
			this.mniQC.Text = "�����������ֱ�";
			this.mniQC.Click += new System.EventHandler(this.mniQC_Click);
			// 
			// mnimnuApplyRecord
			// 
			this.mnimnuApplyRecord.Index = 1;
			this.mnimnuApplyRecord.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.mniBultransonic,
																							  this.mniCTCheckOrder,
																							  this.mniXRay,
																							  this.mniSPECT,
																							  this.mniHighOxygen,
																							  this.mniPathologyOrgCheckOrder,
																							  this.mniMRIOrder,
																							  this.mniEKGOrder,
																							  this.mniNuclearOrder,
																							  this.mniPSGOrder,
																							  this.mniLabAnalysisOrder,
																							  this.mniLabCheckReport,
																							  this.mniImageReport,
																							  this.mniImageBookingSearch});
			this.mnimnuApplyRecord.Text = "��  ��  ��";
			// 
			// mniBultransonic
			// 
			this.mniBultransonic.Index = 0;
			this.mniBultransonic.Text = "B�ͳ������������뵥";
			this.mniBultransonic.Click += new System.EventHandler(this.mniBultransonic_Click);
			// 
			// mniCTCheckOrder
			// 
			this.mniCTCheckOrder.Index = 1;
			this.mniCTCheckOrder.Text = "CT������뵥";
			this.mniCTCheckOrder.Click += new System.EventHandler(this.mniCTCheckOrder_Click);
			// 
			// mniXRay
			// 
			this.mniXRay.Index = 2;
			this.mniXRay.Text = "X�����뵥";
			this.mniXRay.Click += new System.EventHandler(this.mniXRay_Click);
			// 
			// mniSPECT
			// 
			this.mniSPECT.Index = 3;
			this.mniSPECT.Text = "SPECT������뵥";
			this.mniSPECT.Click += new System.EventHandler(this.mniSPECT_Click);
			// 
			// mniHighOxygen
			// 
			this.mniHighOxygen.Index = 4;
			this.mniHighOxygen.Text = "��ѹ���������뵥";
			this.mniHighOxygen.Click += new System.EventHandler(this.mniHighOxygen_Click);
			// 
			// mniPathologyOrgCheckOrder
			// 
			this.mniPathologyOrgCheckOrder.Index = 5;
			this.mniPathologyOrgCheckOrder.Text = "���������֯�ͼ쵥";
			this.mniPathologyOrgCheckOrder.Click += new System.EventHandler(this.mniPathologyOrgCheckOrder_Click);
			// 
			// mniMRIOrder
			// 
			this.mniMRIOrder.Index = 6;
			this.mniMRIOrder.Text = "MRI���뵥";
			this.mniMRIOrder.Click += new System.EventHandler(this.mniMRIOrder_Click);
			// 
			// mniEKGOrder
			// 
			this.mniEKGOrder.Index = 7;
			this.mniEKGOrder.Text = "�ĵ�ͼ���뵥";
			this.mniEKGOrder.Click += new System.EventHandler(this.mniEKGOrder_Click);
			// 
			// mniNuclearOrder
			// 
			this.mniNuclearOrder.Index = 8;
			this.mniNuclearOrder.Text = "���Զർ˯��ͼ������뵥";
			this.mniNuclearOrder.Click += new System.EventHandler(this.mniNuclearOrder_Click);
			// 
			// mniPSGOrder
			// 
			this.mniPSGOrder.Index = 9;
			this.mniPSGOrder.Text = "��ҽѧ������뵥";
			this.mniPSGOrder.Click += new System.EventHandler(this.mniPSGOrder_Click);
			// 
			// mniLabAnalysisOrder
			// 
			this.mniLabAnalysisOrder.Index = 10;
			this.mniLabAnalysisOrder.Text = "ʵ���Ҽ������뵥";
			this.mniLabAnalysisOrder.Visible = false;
			this.mniLabAnalysisOrder.Click += new System.EventHandler(this.mniLabAnalysisOrder_Click);
			// 
			// mniLabCheckReport
			// 
			this.mniLabCheckReport.Index = 11;
			this.mniLabCheckReport.Text = "ʵ���Ҽ��鱨�浥";
			this.mniLabCheckReport.Click += new System.EventHandler(this.mniLabCheckReport_Click);
			// 
			// mniImageReport
			// 
			this.mniImageReport.Index = 12;
			this.mniImageReport.Text = "Ӱ�񱨸浥";
			this.mniImageReport.Click += new System.EventHandler(this.mniImageReport_Click);
			// 
			// mniImageBookingSearch
			// 
			this.mniImageBookingSearch.Index = 13;
			this.mniImageBookingSearch.Text = "Ӱ��ԤԼ��ѯ";
			this.mniImageBookingSearch.Click += new System.EventHandler(this.mniImageBookingSearch_Click);
			// 
			// mnuGrade
			// 
			this.mnuGrade.Index = 2;
			this.mnuGrade.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuSIRS,
																					 this.mnuGlasgow,
																					 this.mnuLung,
																					 this.mnuNewBaby,
																					 this.mnuLittleBaby,
																					 this.mnuAPACHEII,
																					 this.mnuAPACHEIII,
																					 this.mnuTISS28});
			this.mnuGrade.Text = "��������ϵͳ";
			// 
			// mnuSIRS
			// 
			this.mnuSIRS.Index = 0;
			this.mnuSIRS.Text = "SIRS�������";
			this.mnuSIRS.Click += new System.EventHandler(this.mnuSIRS_Click);
			// 
			// mnuGlasgow
			// 
			this.mnuGlasgow.Index = 1;
			this.mnuGlasgow.Text = "����Glasgow��������";
			this.mnuGlasgow.Click += new System.EventHandler(this.mnuGlasgow_Click);
			// 
			// mnuLung
			// 
			this.mnuLung.Index = 2;
			this.mnuLung.Text = "���Է���������";
			this.mnuLung.Click += new System.EventHandler(this.mnuLung_Click);
			// 
			// mnuNewBaby
			// 
			this.mnuNewBaby.Index = 3;
			this.mnuNewBaby.Text = "������Σ�ز�������";
			this.mnuNewBaby.Click += new System.EventHandler(this.mnuNewBaby_Click);
			// 
			// mnuLittleBaby
			// 
			this.mnuLittleBaby.Index = 4;
			this.mnuLittleBaby.Text = "С��Σ�ز�������";
			this.mnuLittleBaby.Click += new System.EventHandler(this.mnuLittleBaby_Click);
			// 
			// mnuAPACHEII
			// 
			this.mnuAPACHEII.Index = 5;
			this.mnuAPACHEII.Text = "APACHEII ����";
			this.mnuAPACHEII.Click += new System.EventHandler(this.mnuAPACHEII_Click);
			// 
			// mnuAPACHEIII
			// 
			this.mnuAPACHEIII.Index = 6;
			this.mnuAPACHEIII.Text = "APACHEIII ����";
			this.mnuAPACHEIII.Click += new System.EventHandler(this.mnuAPACHEIII_Click);
			// 
			// mnuTISS28
			// 
			this.mnuTISS28.Index = 7;
			this.mnuTISS28.Text = "TISS-28����";
			this.mnuTISS28.Click += new System.EventHandler(this.mnuTISS28_Click);
			// 
			// mniDirectionAnalisys
			// 
			this.mniDirectionAnalisys.Index = 3;
			this.mniDirectionAnalisys.Text = "���Ʒ���";
			this.mniDirectionAnalisys.Click += new System.EventHandler(this.mniDirectionAnalisys_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.Text = "ȫ�ײ���";
			this.menuItem3.Visible = false;
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// mniNurseWorkStationForEar
			// 
			this.mniNurseWorkStationForEar.Index = 13;
			this.mniNurseWorkStationForEar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									  this.menuItem6,
																									  this.mniPatientInfoManage,
																									  this.mniWatchItemNurse,
																									  this.mniTendRecordNurse,
																									  this.menuItem4,
																									  this.mniICUTendRecord,
																									  this.mniOperationRecordNurse,
																									  this.mniOperationQtyNurser,
																									  this.menuItem5});
			this.mniNurseWorkStationForEar.Text = "��ʿ����վ";
			this.mniNurseWorkStationForEar.Visible = false;
			// 
			// mniPatientInfoManage
			// 
			this.mniPatientInfoManage.Index = 1;
			this.mniPatientInfoManage.Text = "���˻�������ά��";
			this.mniPatientInfoManage.Click += new System.EventHandler(this.mniPatientInfoManage_Click);
			// 
			// mniWatchItemNurse
			// 
			this.mniWatchItemNurse.Index = 2;
			this.mniWatchItemNurse.Text = "�۲���Ŀ��¼��";
			this.mniWatchItemNurse.Click += new System.EventHandler(this.mniWatchItemNurse_Click);
			// 
			// mniTendRecordNurse
			// 
			this.mniTendRecordNurse.Index = 3;
			this.mniTendRecordNurse.Text = "Σ�ػ��߻����¼";
			this.mniTendRecordNurse.Click += new System.EventHandler(this.mniTendRecordNurse_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "Σ��֢�໤�����ػ���¼��";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// mniICUTendRecord
			// 
			this.mniICUTendRecord.Index = 5;
			this.mniICUTendRecord.Text = "ICUΣ�ػ��߻����¼";
			this.mniICUTendRecord.Visible = false;
			this.mniICUTendRecord.Click += new System.EventHandler(this.mniICUTendRecord_Click);
			// 
			// mniOperationRecordNurse
			// 
			this.mniOperationRecordNurse.Index = 6;
			this.mniOperationRecordNurse.Text = "���������¼";
			this.mniOperationRecordNurse.Click += new System.EventHandler(this.mniOperationRecordNurse_Click);
			// 
			// mniOperationQtyNurser
			// 
			this.mniOperationQtyNurser.Index = 7;
			this.mniOperationQtyNurser.Text = "������е�����ϵ�����";
			this.mniOperationQtyNurser.Click += new System.EventHandler(this.mniOperationQtyNurser_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 8;
			this.menuItem5.Text = "����ICU���������Ƽ໤��¼��";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// pnlPatientInfo
			// 
			this.pnlPatientInfo.AutoScroll = true;
			this.pnlPatientInfo.ContextMenu = this.ctmHRPExplorer;
			this.pnlPatientInfo.Controls.Add(this.txtEmployeeDetailInfo);
			this.pnlPatientInfo.Controls.Add(this.lblEmployeeInfo);
			this.pnlPatientInfo.Controls.Add(this.lblInHospitalNo);
			this.pnlPatientInfo.Controls.Add(this.lblAreaName);
			this.pnlPatientInfo.Controls.Add(this.lblBedName);
			this.pnlPatientInfo.Controls.Add(this.lblSex);
			this.pnlPatientInfo.Controls.Add(this.lblMarried);
			this.pnlPatientInfo.Controls.Add(this.lblLinkManPhone);
			this.pnlPatientInfo.Controls.Add(this.lblInHospitalDate);
			this.pnlPatientInfo.Controls.Add(this.lblName);
			this.pnlPatientInfo.Controls.Add(this.lblCardNo);
			this.pnlPatientInfo.Controls.Add(this.lblRoomName);
			this.pnlPatientInfo.Controls.Add(this.lblDeptName);
			this.pnlPatientInfo.Controls.Add(this.lblTitleDept);
			this.pnlPatientInfo.Controls.Add(this.lblRoomTitle);
			this.pnlPatientInfo.Controls.Add(this.lblPatientTitle);
			this.pnlPatientInfo.Controls.Add(this.lblTitleEquipInfo);
			this.pnlPatientInfo.Controls.Add(this.lblTitleRoom);
			this.pnlPatientInfo.Controls.Add(this.lblTitleArea);
			this.pnlPatientInfo.Controls.Add(this.lblTitleBed);
			this.pnlPatientInfo.Controls.Add(this.lblTitleName);
			this.pnlPatientInfo.Controls.Add(this.lblTitleInHospitalNo);
			this.pnlPatientInfo.Controls.Add(this.lblTitleSex);
			this.pnlPatientInfo.Controls.Add(this.lblTitleMarried);
			this.pnlPatientInfo.Controls.Add(this.lblLinkManName);
			this.pnlPatientInfo.Controls.Add(this.lblTitleLinkManName);
			this.pnlPatientInfo.Controls.Add(this.lblAge);
			this.pnlPatientInfo.Controls.Add(this.lblTitleAge);
			this.pnlPatientInfo.Controls.Add(this.lblTitleLinkManPhone);
			this.pnlPatientInfo.Controls.Add(this.lblTitleInHospitalDate);
			this.pnlPatientInfo.Controls.Add(this.lblTitleCardNo);
			this.pnlPatientInfo.Controls.Add(this.lblICUEquipments);
			this.pnlPatientInfo.Location = new System.Drawing.Point(356, 100);
			this.pnlPatientInfo.Name = "pnlPatientInfo";
			this.pnlPatientInfo.Size = new System.Drawing.Size(712, 724);
			this.pnlPatientInfo.TabIndex = 3;
			this.pnlPatientInfo.Visible = false;
			// 
			// txtEmployeeDetailInfo
			// 
			this.txtEmployeeDetailInfo.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.txtEmployeeDetailInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtEmployeeDetailInfo.ForeColor = System.Drawing.Color.White;
			this.txtEmployeeDetailInfo.Location = new System.Drawing.Point(52, 516);
			this.txtEmployeeDetailInfo.Multiline = true;
			this.txtEmployeeDetailInfo.Name = "txtEmployeeDetailInfo";
			this.txtEmployeeDetailInfo.Size = new System.Drawing.Size(600, 108);
			this.txtEmployeeDetailInfo.TabIndex = 22;
			this.txtEmployeeDetailInfo.Text = "";
			// 
			// lblEmployeeInfo
			// 
			this.lblEmployeeInfo.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblEmployeeInfo.Location = new System.Drawing.Point(28, 488);
			this.lblEmployeeInfo.Name = "lblEmployeeInfo";
			this.lblEmployeeInfo.Size = new System.Drawing.Size(156, 20);
			this.lblEmployeeInfo.TabIndex = 3;
			this.lblEmployeeInfo.Text = "ҽ����Ա���ϣ�";
			// 
			// lblInHospitalNo
			// 
			this.lblInHospitalNo.AutoSize = true;
			this.lblInHospitalNo.Location = new System.Drawing.Point(428, 156);
			this.lblInHospitalNo.Name = "lblInHospitalNo";
			this.lblInHospitalNo.Size = new System.Drawing.Size(0, 19);
			this.lblInHospitalNo.TabIndex = 2;
			// 
			// lblAreaName
			// 
			this.lblAreaName.AutoSize = true;
			this.lblAreaName.Location = new System.Drawing.Point(428, 48);
			this.lblAreaName.Name = "lblAreaName";
			this.lblAreaName.Size = new System.Drawing.Size(0, 19);
			this.lblAreaName.TabIndex = 2;
			// 
			// lblBedName
			// 
			this.lblBedName.AutoSize = true;
			this.lblBedName.Location = new System.Drawing.Point(428, 88);
			this.lblBedName.Name = "lblBedName";
			this.lblBedName.Size = new System.Drawing.Size(0, 19);
			this.lblBedName.TabIndex = 2;
			// 
			// lblSex
			// 
			this.lblSex.AutoSize = true;
			this.lblSex.Location = new System.Drawing.Point(428, 196);
			this.lblSex.Name = "lblSex";
			this.lblSex.Size = new System.Drawing.Size(0, 19);
			this.lblSex.TabIndex = 2;
			// 
			// lblMarried
			// 
			this.lblMarried.AutoSize = true;
			this.lblMarried.Location = new System.Drawing.Point(428, 228);
			this.lblMarried.Name = "lblMarried";
			this.lblMarried.Size = new System.Drawing.Size(0, 19);
			this.lblMarried.TabIndex = 2;
			// 
			// lblLinkManPhone
			// 
			this.lblLinkManPhone.AutoSize = true;
			this.lblLinkManPhone.Location = new System.Drawing.Point(428, 264);
			this.lblLinkManPhone.Name = "lblLinkManPhone";
			this.lblLinkManPhone.Size = new System.Drawing.Size(0, 19);
			this.lblLinkManPhone.TabIndex = 2;
			// 
			// lblInHospitalDate
			// 
			this.lblInHospitalDate.AutoSize = true;
			this.lblInHospitalDate.Location = new System.Drawing.Point(128, 304);
			this.lblInHospitalDate.Name = "lblInHospitalDate";
			this.lblInHospitalDate.Size = new System.Drawing.Size(0, 19);
			this.lblInHospitalDate.TabIndex = 2;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(128, 196);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(0, 19);
			this.lblName.TabIndex = 2;
			// 
			// lblCardNo
			// 
			this.lblCardNo.AutoSize = true;
			this.lblCardNo.Location = new System.Drawing.Point(128, 156);
			this.lblCardNo.Name = "lblCardNo";
			this.lblCardNo.Size = new System.Drawing.Size(0, 19);
			this.lblCardNo.TabIndex = 2;
			// 
			// lblRoomName
			// 
			this.lblRoomName.AutoSize = true;
			this.lblRoomName.Location = new System.Drawing.Point(128, 88);
			this.lblRoomName.Name = "lblRoomName";
			this.lblRoomName.Size = new System.Drawing.Size(0, 19);
			this.lblRoomName.TabIndex = 2;
			// 
			// lblDeptName
			// 
			this.lblDeptName.AutoSize = true;
			this.lblDeptName.Location = new System.Drawing.Point(128, 48);
			this.lblDeptName.Name = "lblDeptName";
			this.lblDeptName.Size = new System.Drawing.Size(0, 19);
			this.lblDeptName.TabIndex = 2;
			// 
			// lblTitleDept
			// 
			this.lblTitleDept.Location = new System.Drawing.Point(56, 48);
			this.lblTitleDept.Name = "lblTitleDept";
			this.lblTitleDept.Size = new System.Drawing.Size(52, 23);
			this.lblTitleDept.TabIndex = 1;
			this.lblTitleDept.Text = "���ң�";
			// 
			// lblRoomTitle
			// 
			this.lblRoomTitle.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblRoomTitle.Location = new System.Drawing.Point(28, 16);
			this.lblRoomTitle.Name = "lblRoomTitle";
			this.lblRoomTitle.Size = new System.Drawing.Size(104, 20);
			this.lblRoomTitle.TabIndex = 0;
			this.lblRoomTitle.Text = "��������:";
			// 
			// lblPatientTitle
			// 
			this.lblPatientTitle.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblPatientTitle.Location = new System.Drawing.Point(28, 120);
			this.lblPatientTitle.Name = "lblPatientTitle";
			this.lblPatientTitle.Size = new System.Drawing.Size(104, 20);
			this.lblPatientTitle.TabIndex = 0;
			this.lblPatientTitle.Text = "��������:";
			// 
			// lblTitleEquipInfo
			// 
			this.lblTitleEquipInfo.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblTitleEquipInfo.Location = new System.Drawing.Point(28, 336);
			this.lblTitleEquipInfo.Name = "lblTitleEquipInfo";
			this.lblTitleEquipInfo.Size = new System.Drawing.Size(104, 20);
			this.lblTitleEquipInfo.TabIndex = 0;
			this.lblTitleEquipInfo.Text = "��������:";
			// 
			// lblTitleRoom
			// 
			this.lblTitleRoom.Location = new System.Drawing.Point(56, 88);
			this.lblTitleRoom.Name = "lblTitleRoom";
			this.lblTitleRoom.Size = new System.Drawing.Size(52, 23);
			this.lblTitleRoom.TabIndex = 1;
			this.lblTitleRoom.Text = "������";
			// 
			// lblTitleArea
			// 
			this.lblTitleArea.Location = new System.Drawing.Point(344, 48);
			this.lblTitleArea.Name = "lblTitleArea";
			this.lblTitleArea.Size = new System.Drawing.Size(48, 23);
			this.lblTitleArea.TabIndex = 1;
			this.lblTitleArea.Text = "������";
			// 
			// lblTitleBed
			// 
			this.lblTitleBed.Location = new System.Drawing.Point(344, 88);
			this.lblTitleBed.Name = "lblTitleBed";
			this.lblTitleBed.Size = new System.Drawing.Size(48, 23);
			this.lblTitleBed.TabIndex = 1;
			this.lblTitleBed.Text = "������";
			// 
			// lblTitleName
			// 
			this.lblTitleName.Location = new System.Drawing.Point(56, 196);
			this.lblTitleName.Name = "lblTitleName";
			this.lblTitleName.Size = new System.Drawing.Size(52, 23);
			this.lblTitleName.TabIndex = 1;
			this.lblTitleName.Text = "������";
			// 
			// lblTitleInHospitalNo
			// 
			this.lblTitleInHospitalNo.Location = new System.Drawing.Point(344, 156);
			this.lblTitleInHospitalNo.Name = "lblTitleInHospitalNo";
			this.lblTitleInHospitalNo.Size = new System.Drawing.Size(68, 23);
			this.lblTitleInHospitalNo.TabIndex = 1;
			this.lblTitleInHospitalNo.Text = "סԺ�ţ�";
			// 
			// lblTitleSex
			// 
			this.lblTitleSex.Location = new System.Drawing.Point(344, 196);
			this.lblTitleSex.Name = "lblTitleSex";
			this.lblTitleSex.Size = new System.Drawing.Size(48, 23);
			this.lblTitleSex.TabIndex = 1;
			this.lblTitleSex.Text = "�Ա�";
			// 
			// lblTitleMarried
			// 
			this.lblTitleMarried.Location = new System.Drawing.Point(344, 228);
			this.lblTitleMarried.Name = "lblTitleMarried";
			this.lblTitleMarried.Size = new System.Drawing.Size(48, 23);
			this.lblTitleMarried.TabIndex = 1;
			this.lblTitleMarried.Text = "���";
			// 
			// lblLinkManName
			// 
			this.lblLinkManName.AutoSize = true;
			this.lblLinkManName.Location = new System.Drawing.Point(128, 264);
			this.lblLinkManName.Name = "lblLinkManName";
			this.lblLinkManName.Size = new System.Drawing.Size(0, 19);
			this.lblLinkManName.TabIndex = 2;
			// 
			// lblTitleLinkManName
			// 
			this.lblTitleLinkManName.Location = new System.Drawing.Point(56, 264);
			this.lblTitleLinkManName.Name = "lblTitleLinkManName";
			this.lblTitleLinkManName.Size = new System.Drawing.Size(68, 23);
			this.lblTitleLinkManName.TabIndex = 1;
			this.lblTitleLinkManName.Text = "��ϵ�ˣ�";
			// 
			// lblAge
			// 
			this.lblAge.AutoSize = true;
			this.lblAge.Location = new System.Drawing.Point(128, 228);
			this.lblAge.Name = "lblAge";
			this.lblAge.Size = new System.Drawing.Size(0, 19);
			this.lblAge.TabIndex = 2;
			// 
			// lblTitleAge
			// 
			this.lblTitleAge.Location = new System.Drawing.Point(56, 228);
			this.lblTitleAge.Name = "lblTitleAge";
			this.lblTitleAge.Size = new System.Drawing.Size(52, 23);
			this.lblTitleAge.TabIndex = 1;
			this.lblTitleAge.Text = "���䣺";
			// 
			// lblTitleLinkManPhone
			// 
			this.lblTitleLinkManPhone.Location = new System.Drawing.Point(344, 264);
			this.lblTitleLinkManPhone.Name = "lblTitleLinkManPhone";
			this.lblTitleLinkManPhone.Size = new System.Drawing.Size(92, 23);
			this.lblTitleLinkManPhone.TabIndex = 1;
			this.lblTitleLinkManPhone.Text = "��ϵ�˵绰��";
			// 
			// lblTitleInHospitalDate
			// 
			this.lblTitleInHospitalDate.Location = new System.Drawing.Point(56, 304);
			this.lblTitleInHospitalDate.Name = "lblTitleInHospitalDate";
			this.lblTitleInHospitalDate.Size = new System.Drawing.Size(80, 23);
			this.lblTitleInHospitalDate.TabIndex = 1;
			this.lblTitleInHospitalDate.Text = "��Ժ���ڣ�";
			// 
			// lblTitleCardNo
			// 
			this.lblTitleCardNo.Location = new System.Drawing.Point(56, 156);
			this.lblTitleCardNo.Name = "lblTitleCardNo";
			this.lblTitleCardNo.Size = new System.Drawing.Size(80, 23);
			this.lblTitleCardNo.TabIndex = 1;
			this.lblTitleCardNo.Text = "���￨�ţ�";
			// 
			// lblICUEquipments
			// 
			this.lblICUEquipments.Location = new System.Drawing.Point(56, 376);
			this.lblICUEquipments.Name = "lblICUEquipments";
			this.lblICUEquipments.Size = new System.Drawing.Size(596, 108);
			this.lblICUEquipments.TabIndex = 2;
			// 
			// timRefreshEmployee
			// 
			this.timRefreshEmployee.Enabled = true;
			this.timRefreshEmployee.Interval = 1000;
			this.timRefreshEmployee.Tick += new System.EventHandler(this.timRefreshEmployee_Tick);
			// 
			// m_pnlHeader
			// 
			this.m_pnlHeader.BackColor = System.Drawing.Color.Gainsboro;
			this.m_pnlHeader.Controls.Add(this.m_picExpand);
			this.m_pnlHeader.Controls.Add(this.m_cboArea);
			this.m_pnlHeader.Controls.Add(this.lblAreaTitle);
			this.m_pnlHeader.Controls.Add(this.m_cboDept);
			this.m_pnlHeader.Controls.Add(this.lblDeptTitle);
			this.m_pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_pnlHeader.Location = new System.Drawing.Point(264, 0);
			this.m_pnlHeader.Name = "m_pnlHeader";
			this.m_pnlHeader.Size = new System.Drawing.Size(764, 48);
			this.m_pnlHeader.TabIndex = 4;
			// 
			// m_picExpand
			// 
			this.m_picExpand.Image = ((System.Drawing.Image)(resources.GetObject("m_picExpand.Image")));
			this.m_picExpand.Location = new System.Drawing.Point(4, 16);
			this.m_picExpand.Name = "m_picExpand";
			this.m_picExpand.Size = new System.Drawing.Size(16, 16);
			this.m_picExpand.TabIndex = 5003;
			this.m_picExpand.TabStop = false;
			this.m_picExpand.Tag = "1";
			this.m_picExpand.Visible = false;
			this.m_picExpand.Click += new System.EventHandler(this.m_picExpand_Click);
			// 
			// m_cboArea
			// 
			this.m_cboArea.BackColor = System.Drawing.Color.White;
			this.m_cboArea.BorderColor = System.Drawing.Color.Black;
			this.m_cboArea.DropButtonBackColor = System.Drawing.Color.Gainsboro;
			this.m_cboArea.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
			this.m_cboArea.DropButtonForeColor = System.Drawing.Color.Black;
			this.m_cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboArea.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cboArea.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cboArea.ForeColor = System.Drawing.Color.Black;
			this.m_cboArea.ListBackColor = System.Drawing.Color.White;
			this.m_cboArea.ListForeColor = System.Drawing.Color.Black;
			this.m_cboArea.ListSelectedBackColor = System.Drawing.Color.Blue;
			this.m_cboArea.ListSelectedForeColor = System.Drawing.Color.White;
			this.m_cboArea.Location = new System.Drawing.Point(368, 12);
			this.m_cboArea.m_BlnEnableItemEventMenu = false;
			this.m_cboArea.Name = "m_cboArea";
			this.m_cboArea.SelectedIndex = -1;
			this.m_cboArea.SelectedItem = null;
			this.m_cboArea.SelectionStart = -1;
			this.m_cboArea.Size = new System.Drawing.Size(227, 23);
			this.m_cboArea.TabIndex = 491;
			this.m_cboArea.TextBackColor = System.Drawing.Color.White;
			this.m_cboArea.TextForeColor = System.Drawing.Color.Black;
			this.m_cboArea.SelectedIndexChanged += new System.EventHandler(this.m_cboArea_SelectedIndexChanged);
			// 
			// lblAreaTitle
			// 
			this.lblAreaTitle.AutoSize = true;
			this.lblAreaTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblAreaTitle.ForeColor = System.Drawing.Color.Black;
			this.lblAreaTitle.Location = new System.Drawing.Point(328, 16);
			this.lblAreaTitle.Name = "lblAreaTitle";
			this.lblAreaTitle.Size = new System.Drawing.Size(41, 19);
			this.lblAreaTitle.TabIndex = 492;
			this.lblAreaTitle.Text = "����:";
			// 
			// m_cboDept
			// 
			this.m_cboDept.BackColor = System.Drawing.Color.White;
			this.m_cboDept.BorderColor = System.Drawing.Color.Black;
			this.m_cboDept.DropButtonBackColor = System.Drawing.Color.Gainsboro;
			this.m_cboDept.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
			this.m_cboDept.DropButtonForeColor = System.Drawing.Color.Black;
			this.m_cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboDept.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cboDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cboDept.ForeColor = System.Drawing.Color.Black;
			this.m_cboDept.ListBackColor = System.Drawing.Color.White;
			this.m_cboDept.ListForeColor = System.Drawing.Color.Black;
			this.m_cboDept.ListSelectedBackColor = System.Drawing.Color.Blue;
			this.m_cboDept.ListSelectedForeColor = System.Drawing.Color.White;
			this.m_cboDept.Location = new System.Drawing.Point(80, 12);
			this.m_cboDept.m_BlnEnableItemEventMenu = false;
			this.m_cboDept.Name = "m_cboDept";
			this.m_cboDept.SelectedIndex = -1;
			this.m_cboDept.SelectedItem = null;
			this.m_cboDept.SelectionStart = -1;
			this.m_cboDept.Size = new System.Drawing.Size(227, 23);
			this.m_cboDept.TabIndex = 491;
			this.m_cboDept.TextBackColor = System.Drawing.Color.White;
			this.m_cboDept.TextForeColor = System.Drawing.Color.Black;
			this.m_cboDept.SelectedIndexChanged += new System.EventHandler(this.m_cboDept_SelectedIndexChanged);
			// 
			// lblDeptTitle
			// 
			this.lblDeptTitle.AutoSize = true;
			this.lblDeptTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblDeptTitle.ForeColor = System.Drawing.Color.Black;
			this.lblDeptTitle.Location = new System.Drawing.Point(40, 16);
			this.lblDeptTitle.Name = "lblDeptTitle";
			this.lblDeptTitle.Size = new System.Drawing.Size(41, 19);
			this.lblDeptTitle.TabIndex = 493;
			this.lblDeptTitle.Text = "����:";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "������Ժ������";
			this.menuItem6.Visible = false;
			// 
			// Form_HRPExplorer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1028, 733);
			this.Controls.Add(this.m_pnlHeader);
			this.Controls.Add(this.lsvExplorerMid);
			this.Controls.Add(this.pnlPatientInfo);
			this.Controls.Add(this.m_sptLeft);
			this.Controls.Add(this.trvExplorer);
			this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ForeColor = System.Drawing.Color.White;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form_HRPExplorer";
			this.Text = "iCare��Դ������";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form_HRPExplorer_Load);
			this.pnlPatientInfo.ResumeLayout(false);
			this.m_pnlHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		//��ʼ��
		private void m_mthInit()
		{
			//EmployeeIDΪ9999999����Admin
			m_intLoadFlag = clsLoginContext.s_ObjLoginContext.m_StrEmployeeID == "9999999"?0:1;

			stuTreeExpandTag =new TreeExpandTag(); 
			m_objDepartmentManager = new clsDepartmentManager();
			m_objRoleManager = new clsRoleManager();

			clsHRPColor.s_mthChangeInputBackColor(m_cboArea);
			clsHRPColor.s_mthChangeInputBackColor(m_cboDept);
			clsHRPColor.s_mthChangeInputForeColor(m_cboArea);
			clsHRPColor.s_mthChangeInputForeColor(m_cboDept);

			//			m_mthLoadCustomForms();
		}

		#region  Form Load
		private void Form_HRPExplorer_Load(object sender, System.EventArgs e)
		{   
			try
			{
				m_mthInit();

				lsvExplorerMid.Dock=DockStyle.Fill;
				m_sptLeft.Enabled =true;
				lsvExplorerMid.Visible=false; 
				lsvExplorerMid.View=View.LargeIcon ;  
				this.lsvExplorerMid.BringToFront();
				this.lsvExplorerMid.HoverSelection=true;
				//**********************************************************************
				imgUserclose = new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "CLSDFOLD.ICO");
				imgUseropen= new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "OPENFOLD.ICO");			
				m_blnIsExpand=true;
				this.m_picExpand.Image=imgUseropen;	
				//**********************************************************************

				m_blnCanSeachEventTakePlace=false;

				#region ��ȡ���Ҽ������б�
				clsDeptAndAreaInfo[] objDeptAndAreaInfoArr=null;
				long lngRes=m_objDepartmentManager.m_lngGetAllDeptAndAreaInfoArr(out objDeptAndAreaInfoArr);
				if(lngRes<=0)
				{
					if(lngRes==(long) enmOperationResult.Not_permission)
						clsPublicFunction.ShowInformationMessageBox("Ȩ�޲���!");
					else
						clsPublicFunction.ShowInformationMessageBox("���ݿ�����ʧ��!");
					return;
				}
				#endregion ��ȡ���Ҽ������б�
				
				#region ��ȡ���TreeView�����в�����������Ϣ,Jacky-2003-5-28
				TreeNode  TopNode=new  TreeNode("iCare-�嫿Ƽ�ҽ����Ϣ��Դƽ̨",0,0);
				trvExplorer.Nodes.Add(TopNode) ;
				if(objDeptAndAreaInfoArr  !=null ) 
				{
					m_mthGetAllDept(TopNode,objDeptAndAreaInfoArr);
				}

				#endregion ��ȡ���TreeView�����в�����������Ϣ,Jacky-2003-5-28
					
				#region ��ȡ�ұ�Panel�����в�����������Ϣ,Jacky-2003-5-28
					
				if(objDeptAndAreaInfoArr !=null)
				{						
					for(int i1=0;i1<objDeptAndAreaInfoArr.Length;i1++)
					{
						for(int i2=0;i2<m_objOISFArr.Length;i2++)
						{
							if(objDeptAndAreaInfoArr[i1].m_objDept.m_StrDeptID==m_objOISFArr[i2].m_strBaseID)
							{
								m_cboDept.AddItem(objDeptAndAreaInfoArr[i1].m_objDept);

								if(objDeptAndAreaInfoArr[i1].m_objDept.m_StrDeptID==clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID)
								{
									if(m_blnCanSeachEventTakePlace !=false)
										m_blnCanSeachEventTakePlace=false;
									m_cboDept.SelectedItem=objDeptAndAreaInfoArr[i1].m_objDept;	
								
									#region ��ȡѡ�еĿ����е����в���
									this.m_cboArea.ClearItem();
									clsInPatientArea[] objAreaArr=objDeptAndAreaInfoArr[i1].m_objAreaArr;
									if(objAreaArr !=null && objAreaArr.Length>0)
									{
										m_cboArea.AddRangeItems(objAreaArr);
					
										if(m_blnCanSeachEventTakePlace !=false)
											m_blnCanSeachEventTakePlace=false;
										m_cboArea.SelectedIndex=0;						
					
										#region ����
										lsvExplorerMid.Items.Clear();
										clsInPatientBed[] objBedArr;
										clsPatient []  objPatientArr;
										m_objDepartmentManager.m_lngGetAllBedAndPatientInArea(objAreaArr[0].m_StrAreaID,out objBedArr,out objPatientArr);
										if(objBedArr !=null)
										{
											for(int j2=0;j2<objBedArr.Length;j2++)
											{
												ListViewItem lviNewItem;
												if(objPatientArr[j2] !=null)
												{
													if(objPatientArr[j2].m_StrSex=="��")
													{
														lviNewItem = new ListViewItem(new string[]{
																									  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "��",
																									  objPatientArr[j2].m_StrInPatientID
																								  });
														lviNewItem.ForeColor = Color.Black;
														//															if(objBedArr[j2].m_StrBed_Status=="") 
														//																lviNewItem.ForeColor = Color.FromArgb(6,231,236);
														//															else lviNewItem.ForeColor = Color.Black;
									
													}
													else if(objPatientArr[j2].m_StrSex=="Ů")
													{
														lviNewItem = new ListViewItem(new string[]{
																									  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "Ů",
																									  objPatientArr[j2].m_StrInPatientID
																								  });

														//															if(objBedArr[j2].m_StrBed_Status=="") lviNewItem.ForeColor = Color.Orange;
														//															else lviNewItem.ForeColor = Color.Black;
														lviNewItem.ForeColor = Color.Red;
													}
													else
													{			
														lviNewItem = new ListViewItem(new string[]{
																									  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "����",
																									  objPatientArr[j2].m_StrInPatientID
																								  });
														//															if(objBedArr[j2].m_StrBed_Status=="") lviNewItem.ForeColor = Color.White;
														//															else lviNewItem.ForeColor = Color.Black;
														lviNewItem.ForeColor = Color.Green;
													}
													lviNewItem.ImageIndex = 1;	
												}
												else 
												{
													lviNewItem = new ListViewItem(new string[]{
																								  objBedArr[j2].m_StrBedName ,
																								  ""
																							  });

													lviNewItem.ImageIndex = 0;
												}
												lviNewItem.Tag = objPatientArr[j2];
												lsvExplorerMid.Items.Add(lviNewItem);

												//Add by jli 2004-11-29
												//////													m_mthReadMessage();
												//////													m_mthViewMessage();
												//Add End

											}
											//Add by DSL 2004-12-15
											//��ʱ���ν���SQL server�汾���ԣ���д�ô洢���̺��� tfzhang 2005-6-21 17:36:09
											m_mthReadMessage();
											m_mthViewMessage();
											//Add End

										}
										#endregion	
					
										this.lsvExplorerMid.Visible=true;					
									}								
								}
								#endregion ��ȡѡ�еĿ����е����в���
							}
						}//end for
					}//end for

				}			
					
				#endregion ��ȡ�ұ�Panel�����в�����������Ϣ,Jacky-2003-5-28

				#region �ı���Ӧ��ѡ�����ڵ���ʾ
				foreach(TreeNode node in this.trvExplorer.Nodes[0].Nodes )
				{
					if(node.Tag !=null && node.Tag.ToString().IndexOf("Dept:")==0 && m_cboDept.SelectedItem!=null && node.Tag.ToString().Substring(5)==((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID)
					{
						if(m_blnCanSeachEventTakePlace !=false)
							m_blnCanSeachEventTakePlace=false;//�ı���Ӧ��ѡ�����ڵ���ʾ
						this.trvExplorer.SelectedNode = node;

						foreach(TreeNode node2 in node.Nodes )
						{
							if(node2.Tag !=null && node2.Tag.ToString().IndexOf("Area:")==0 && m_cboArea.SelectedItem!=null && node2.Tag.ToString().Substring(5)==((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_StrAreaID)
							{
								this.trvExplorer.SelectedNode=node2;
								break;
							}
						}
						break;
					}		
				}
				
				if(trvExplorer.SelectedNode == null)
					trvExplorer.SelectedNode = trvExplorer.Nodes[0];
				#endregion �ı���Ӧ��ѡ�����ڵ���ʾ
				
				m_blnCanSeachEventTakePlace=true;	
		
				#region ��ȡ��ǰԱ��������Ȩ��
				objPIArr = clsLoginContext.s_ObjLoginContext.m_ObjPIArr;
				#endregion

				if (MDIParent.s_ObjCurrDepartment!=null)
				{
					m_cboDept.SelectedItem=MDIParent.s_ObjCurrDepartment;
				}
				else
				{
					if(m_cboDept.GetItemsCount()>0)
					{
						m_cboDept.SelectedIndex=0;
						MDIParent.s_ObjCurrDepartment=(clsDepartment)m_cboDept.GetItem(0);
					}
				}

				if (MDIParent.s_ObjCurrInPatientArea!=null)
				{
					m_cboArea.SelectedItem=MDIParent.s_ObjCurrInPatientArea;
				}
				else
				{
					if(m_cboArea.GetItemsCount()>0)
					{
						m_cboArea.SelectedIndex=0;
						MDIParent.s_ObjCurrInPatientArea=(clsInPatientArea)m_cboArea.GetItem(0);
					}
				}

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message+"\n"+ex.TargetSite.ReflectedType.Name+"\n"+ex.TargetSite.Name);
			}
		}

		/// <summary>
		/// ��ȡ�ļ��ľ���·�����ϼ�Ŀ¼·��
		/// </summary>
		/// <returns></returns>
		public string m_strGetFilePathHeader() 
		{
			string [] strFilePathAll =  Application.ExecutablePath.Split('\\') ;
			string strFilePathHeader="";
			if(strFilePathAll!=null)
				for(int i=0;i<strFilePathAll.Length-3;i++)
					strFilePathHeader+=strFilePathAll[i]+"\\\\";
			return strFilePathHeader;
		}

		private clsOISF[] m_objOISFArr;

		/// <summary>
		/// �������У���ȡ���в��ż��¼�������Jacky-2003-5-28
		/// </summary>
		/// <param name="p_objParentNode"></param>
		/// <param name="p_strDeptID"></param>
		private void m_mthGetAllDept(TreeNode p_objParentNode,clsDeptAndAreaInfo[] p_objDeptAndAreaInfoArr)
		{
			if(p_objParentNode ==null || p_objDeptAndAreaInfoArr==null)
				return;

			int intDeptCount = 0;	
			try
			{
				string strEmployeeID = clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID;
				//			
				m_objRoleManager.m_lngGetDeptByEmployeeID(strEmployeeID,out m_objOISFArr);
			}
			catch(Exception ex)
			{
				MessageBox.Show("m_mthGetAllDept�ڲ�1��" + "\n" + ex.Message+"\n"+ex.TargetSite.ReflectedType.Name+"\n"+ex.TargetSite.Name);
			}
			try
			{
				for(int i=0;i<m_objOISFArr.Length;i++)
				{
					//				if(!clsPublicFunction.s_blnCheckCurrentPrivilege(p_objDeptAndAreaInfoArr[i].m_objDept.m_StrDeptID,PrivilegeData.enmPrivilegeSF.HRPExplorer,PrivilegeData.enmPrivilegeOperation.Read))
					//					continue;
					TreeNode trnNewNode=new TreeNode(m_objOISFArr[i].m_strBaseName,23,23);
					trnNewNode.Tag="Dept:"+m_objOISFArr[i].m_strBaseID;
					p_objParentNode.Nodes.Add(trnNewNode);
					intDeptCount++;
                
					//���ɼ��Ĳ���ID��ȫ������ID�Ƚϣ��ó������Ĳ���
					for(int j=0;j<p_objDeptAndAreaInfoArr.Length;j++)
					{
						//					TreeNode trnDept=new TreeNode(p_objDeptAndAreaInfoArr[j].m_objDept.m_StrDeptName,23,23);
						//					trnDept.Tag="Dept:"+p_objDeptAndAreaInfoArr[j].m_objDept.m_StrDeptID;
						//					p_objParentNode.Nodes.Add(trnDept);
						//					m_mthGetAllAreaInDept(trnDept,p_objDeptAndAreaInfoArr[j].m_objAreaArr);

						if(m_objOISFArr[i].m_strBaseID==p_objDeptAndAreaInfoArr[j].m_objDept.m_StrDeptID)
						{
							m_mthGetAllAreaInDept(p_objParentNode.Nodes[intDeptCount-1],p_objDeptAndAreaInfoArr[j].m_objAreaArr);
						}
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("m_mthGetAllDept�ڲ�2��" + "\n" + ex.Message+"\n"+ex.TargetSite.ReflectedType.Name+"\n"+ex.TargetSite.Name);
			}
			
		}
		
		/// <summary>
		/// �������У���ȡ�����е����в������¼�������Jacky-2003-5-27
		/// </summary>
		/// <param name="p_objParentNode"></param>
		/// <param name="p_strDeptID"></param>
		private void m_mthGetAllAreaInDept(TreeNode p_objParentNode,clsInPatientArea[] p_objAreaArr)
		{
			if(p_objParentNode ==null || p_objAreaArr==null )
				return;			
			
			if(p_objAreaArr !=null)
			{
				for(int i=0;i<p_objAreaArr.Length;i++)
				{
					TreeNode trnNewNode=new TreeNode(p_objAreaArr[i].m_StrAreaName,2,2);
					trnNewNode.Tag="Area:"+p_objAreaArr[i].m_StrAreaID;
					p_objParentNode.Nodes.Add(trnNewNode);
					
					// ����,�ָ�Ϊȡ����������,ֱ�ӴӲ�����ȡ����
					//m_mthGetAllRoomInArea(p_objParentNode.Nodes[i],objAreaArr[i].m_StrAreaID);

				}
			}
		}

		//		/// <summary>
		//		/// �������У���ȡ�����е����в�����Jacky-2003-5-27
		//		/// </summary>
		//		/// <param name="p_objParentNode"></param>
		//		/// <param name="p_strAreaID"></param>
		//		private void m_mthGetAllRoomInArea(TreeNode p_objParentNode,string p_strAreaID)
		//		{
		//			#region ����
		//			clsInPatientRoom[] objRoomArr;
		//			m_objDepartmentManager.m_lngGetAllRoomInArea(p_strAreaID,out objRoomArr);
		//			if(objRoomArr !=null)
		//			{
		//				for(int j2=0;j2<objRoomArr.Length;j2++)
		//				{
		//					TreeNode trnNewNode_Room=new TreeNode(objRoomArr[j2].m_StrRoomName,2,2);
		//					trnNewNode_Room.Tag="Room:"+ objRoomArr[j2].m_StrRoomID;
		//					p_objParentNode.Nodes.Add(trnNewNode_Room);
		//				}
		//			}
		//			#endregion
		//		}		
		#endregion		
	
		#region Control Pulbic Funtion

		public void Copy()
		{
			m_lngCopy();
		}

		public void Cut()
		{m_lngCut();}

		public void Paste()
		{m_lngPaste();}

		public void Redo()
		{}
		public void Verify()
		{
			////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}
		public void Undo()
		{}

		public void Save()
		{
		}

		public void Delete()
		{
		}


		public void Display(string cardno, string sendcheckdate)
		{
		
		}
		
		public void Display()
		{
				
		}
		#endregion		

		public void Print()
		{
		}

		#region  MenuItem_Click
		private void mniMainRecord_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInHospitalMainRecord frminhospitalmainrecord=new frmInHospitalMainRecord();
				frminhospitalmainrecord.MdiParent = this.MdiParent;
				frminhospitalmainrecord.Show(); 
				frminhospitalmainrecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniPatientProcessRecord_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient =  (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSubDiseaseTrack frmsubdiseasetrack=new frmSubDiseaseTrack();
				frmsubdiseasetrack.MdiParent =this.MdiParent;
				frmsubdiseasetrack.Show(); 
				frmsubdiseasetrack.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		
		}
		private void mniOperationRecordDoct_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationRecordDoctor frmoperationrecorddoctor=new frmOperationRecordDoctor();
				frmoperationrecorddoctor.MdiParent =this.MdiParent;
				frmoperationrecorddoctor.Show(); 
				frmoperationrecorddoctor.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniOperationAgreed_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationAgreedRecord frmoperationagreedrecord=new frmOperationAgreedRecord();
				frmoperationagreedrecord.MdiParent =this.MdiParent;
				frmoperationagreedrecord.Show(); 
				frmoperationagreedrecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		
		}
		private void mniOperationSummary_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBeforeOperationSummary frmbeforeoperationsummary=new frmBeforeOperationSummary();
				frmbeforeoperationsummary.MdiParent =this.MdiParent;
				frmbeforeoperationsummary.Show(); 
				frmbeforeoperationsummary.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniQC_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmQCRecord frmqcrecord=new frmQCRecord();
				frmqcrecord.MdiParent =this.MdiParent;
				frmqcrecord.Show(); 
				frmqcrecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniSPECT_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSPECT frmspect=new frmSPECT();
				frmspect.MdiParent =this.MdiParent;
				frmspect.Show(); 
				frmspect.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniHighOxygen_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmHighOxygen frmhighoxygen=new frmHighOxygen();
				frmhighoxygen.MdiParent =this.MdiParent;
				frmhighoxygen.Show();
				frmhighoxygen.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniBultransonic_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBUltrasonicCheckOrder frmbultrasoniccheckorder=new frmBUltrasonicCheckOrder();
				frmbultrasoniccheckorder.MdiParent =this.MdiParent;
				frmbultrasoniccheckorder.Show(); 
				frmbultrasoniccheckorder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniCTCheckOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				//return;
			{
				try
				{
					this.Cursor=Cursors.WaitCursor;
					frmCTCheckOrder frmctcheckorder=new frmCTCheckOrder("1111111","��ʱ����","����������������ɳ��");
					frmctcheckorder.MdiParent =this.MdiParent;
					frmctcheckorder.Show(); 
					this.Cursor=Cursors.Default;
				}
				catch
				{}
				return;

			}

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				if(objSelectPatient.m_StrInPatientID==""||objSelectPatient.m_StrInPatientID.Trim()=="")
				{
					MessageBox.Show("����ID����Ϊ��");
					return;
				}
				this.Cursor=Cursors.WaitCursor;
				frmCTCheckOrder frmctcheckorder=new frmCTCheckOrder(objSelectPatient.m_StrInPatientID,objSelectPatient.m_StrName,"");
				frmctcheckorder.MdiParent =this.MdiParent;
				frmctcheckorder.Show(); 
				frmctcheckorder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		
		}

		private void mniXRay_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmXRayCheckOrder frmXraycheckorder=new frmXRayCheckOrder();
				frmXraycheckorder.MdiParent =this.MdiParent;
				frmXraycheckorder.Show(); 
				frmXraycheckorder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniPathologyOrgCheckOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPathologyOrgCheckOrder frmpathologyorgcheckorder=new frmPathologyOrgCheckOrder();
				frmpathologyorgcheckorder.MdiParent =this.MdiParent;
				frmpathologyorgcheckorder.Show(); 
				frmpathologyorgcheckorder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniMRIOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMRIApply frmmriApplyOrder=new frmMRIApply();
				frmmriApplyOrder.MdiParent =this.MdiParent;
				frmmriApplyOrder.Show(); 
				frmmriApplyOrder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniImageReport_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmImageReport frmimagereport=new frmImageReport();
				frmimagereport.MdiParent =this.MdiParent;
				frmimagereport.Show(); 
				frmimagereport.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniImageBookingSearch_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmImageBookingSearch frmimagebookingsearch=new frmImageBookingSearch();
				frmimagebookingsearch.MdiParent =this.MdiParent;
				frmimagebookingsearch.Show(); 
				frmimagebookingsearch.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		private void mniLabAnalysisOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLabAnalysisOrder frmlabanalysisorder=new frmLabAnalysisOrder();
				frmlabanalysisorder.MdiParent =this.MdiParent;
				frmlabanalysisorder.Show(); 
				frmlabanalysisorder.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniLabCheckReport_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLabCheckReport frmlabcheckreport=new frmLabCheckReport();
				frmlabcheckreport.MdiParent =this.MdiParent;
				frmlabcheckreport.Show(); 
				frmlabcheckreport.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void mniThreeMeasureRecordNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmThreeMeasureRecord frmthreemeasurerecord=new frmThreeMeasureRecord();
				frmthreemeasurerecord.MdiParent =this.MdiParent;
				frmthreemeasurerecord.m_mthDisableSelectPatient(false);
				frmthreemeasurerecord.Show(); 
				frmthreemeasurerecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniEvaluateNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientEvaluate frminwardevalute;
				if(c_strDeptID_ICUCenterDepartment !=((clsDepartment)(m_cboDept.SelectedItem)).m_StrDeptID)
					frminwardevalute=new frmInPatientEvaluate(false);//�˴�Ϊ��ICU��ʹ��
				else
					frminwardevalute=new frmInPatientEvaluate(true);//�˴�ΪICU��ʹ��
				frminwardevalute.MdiParent =this.MdiParent;
				frminwardevalute.m_mthDisableSelectPatient(false);
				frminwardevalute.Show(); 
				frminwardevalute.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}			
		}

		private void mniTendRecordNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmIntensiveTendMain frmintensivetendmain=new frmIntensiveTendMain();
				frmintensivetendmain.MdiParent =this.MdiParent;
				frmintensivetendmain.m_mthDisableSelectPatient(false);
				frmintensivetendmain.Show(); 
				frmintensivetendmain.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniOperationRecordNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationRecord frmoperationrecord=new frmOperationRecord();
				frmoperationrecord.MdiParent =this.MdiParent;
				frmoperationrecord.m_mthDisableSelectPatient(false);
				frmoperationrecord.Show(); 
				frmoperationrecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniOperationQtyNurser_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationEquipmentQty frmoperationequipmentqty=new frmOperationEquipmentQty();
				frmoperationequipmentqty.MdiParent =this.MdiParent;
				frmoperationequipmentqty.m_mthDisableSelectPatient(false);
				frmoperationequipmentqty.Show(); 
				frmoperationequipmentqty.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}			
		}

		private void mniGeneralTendRecordNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainGeneralNurseRecord frmmaingeneralnurserecord=new frmMainGeneralNurseRecord();
				frmmaingeneralnurserecord.MdiParent =this.MdiParent;
				frmmaingeneralnurserecord.m_mthDisableSelectPatient(false);
				frmmaingeneralnurserecord.Show(); 
				frmmaingeneralnurserecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniWatchItemNurse_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmWatchItemTrack frmwatchitemtrack=new frmWatchItemTrack();
				frmwatchitemtrack.MdiParent =this.MdiParent;
				frmwatchitemtrack.m_mthDisableSelectPatient(false);
				frmwatchitemtrack.Show(); 
				frmwatchitemtrack.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		

		private void mnifrmPICUShiftInForm_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPICUShiftInForm frmpicuShiftInForm=new frmPICUShiftInForm();
				frmpicuShiftInForm.MdiParent =this.MdiParent;
				frmpicuShiftInForm.Show(); 
				frmpicuShiftInForm.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnifrmPICUShiftOutForm_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPICUShiftOutForm frmpicuShiftOutForm=new frmPICUShiftOutForm();
				frmpicuShiftOutForm.MdiParent =this.MdiParent;
				frmpicuShiftOutForm.Show(); 
				frmpicuShiftOutForm.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		
		private void mniICUTendRecord_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmICUIntensiveTendRecord frmicuintensivetendrecord=new frmICUIntensiveTendRecord();
				frmicuintensivetendrecord.MdiParent =this.MdiParent;
				frmicuintensivetendrecord.Show(); 
				frmicuintensivetendrecord.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		
	
		private void mnuInPatientCaseHistory_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				//				switch(objSelectPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID)
				//				{	
				//					//����ʹ�õĿ�����ʱ�����µĽṹ
				//					case "1110000"://���Ǻ��
				//					case "1040300"://�ǿ�
				//					case "1030200"://�����ڿ�
				//					case "1040700"://�ε����
				//						frmInPatientCaseHistory frmChild = new frmInPatientCaseHistory(objSelectPatient);
				//						frmChild.MdiParent =this.MdiParent;
				//						frmChild.Show();
				//						frmChild.m_mthSetPatient(objSelectPatient);
				//						break;
				//					default:
				//						string strFormName = c_strGetInpatMedRecType(objSelectPatient);
				//						if (strFormName == null)  break;
				//						System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(".\\iCareComponent.dll");
				//						object obj = asm.CreateInstance("iCare." + strFormName);
				//						frmHRPBaseForm frm = (frmHRPBaseForm)obj;
				//						frm.MdiParent =this.MdiParent;
				//						frm.Show();
				//						frm.m_mthSetPatient(objSelectPatient);
				//						break;
				//				}
				//				frmInPatientCaseHistory frmChild = new frmInPatientCaseHistory();
				//				frmChild.MdiParent =this.MdiParent;
				//				frmChild.Show();
				//				frmChild.m_mthSetPatient(objSelectPatient);
				frmInPatMedRecChoose frmChoose = new frmInPatMedRecChoose();
				frmChoose.ShowDialog();
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void mnuInPatientCaseMode2_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientCaseHistoryMode1  frminpatientcaseHistorymode1=new frmInPatientCaseHistoryMode1();
				frminpatientcaseHistorymode1.MdiParent =this.MdiParent;
				frminpatientcaseHistorymode1.Show();
				frminpatientcaseHistorymode1.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniDirectionAnalisys_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmICUTrend  frmicutrend=new frmICUTrend();
				frmicutrend.MdiParent =this.MdiParent;
				frmicutrend.Show();
				frmicutrend.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void mnuSIRS_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSIRSEvaluation frmsirsevaluation=new frmSIRSEvaluation();
				frmsirsevaluation.MdiParent =this.MdiParent;
				frmsirsevaluation.Show();
				//frmsirsevaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuGlasgow_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmImproveGlasgowComaEvaluation frmimproveglasgowcomaevaluation=new frmImproveGlasgowComaEvaluation();
				frmimproveglasgowcomaevaluation.MdiParent =this.MdiParent;
				frmimproveglasgowcomaevaluation.Show();
				//frmimproveglasgowcomaevaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuLung_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLungInjuryEvaluation frmlunginjuryevaluation=new frmLungInjuryEvaluation();
				frmlunginjuryevaluation.MdiParent =this.MdiParent;
				frmlunginjuryevaluation.Show();
				//frmlunginjuryevaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuNewBaby_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmNewBabyInjuryCaseEvaluation frmnewbabyInjurycaseevaluation=new frmNewBabyInjuryCaseEvaluation();
				frmnewbabyInjurycaseevaluation.MdiParent =this.MdiParent;
				frmnewbabyInjurycaseevaluation.Show();
				//frmnewbabyInjurycaseevaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuLittleBaby_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBabyInjuryCaseEvaluation frmbabyinjurycaseevaluation=new frmBabyInjuryCaseEvaluation();
				frmbabyinjurycaseevaluation.MdiParent =this.MdiParent;
				frmbabyinjurycaseevaluation.Show();
				//frmbabyinjurycaseevaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuAPACHEII_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmAPACHEIIValuation apacheIIValuation=new frmAPACHEIIValuation();
				apacheIIValuation.MdiParent =this.MdiParent;
				apacheIIValuation.Show(); 
				//apacheIIValuation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuAPACHEIII_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmAPACHEIIIValuation apacheIIIValuation=new frmAPACHEIIIValuation();
				apacheIIIValuation.MdiParent =this.MdiParent;
				apacheIIIValuation.Show(); 
				//apacheIIIValuation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuTISS28_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmTISSValuation frmtissvaluation=new frmTISSValuation();
				frmtissvaluation.MdiParent =this.MdiParent;
				frmtissvaluation.Show();
				//frmtissvaluation.m_mthSetPatient(objSelectPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniOutHospital_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOutHospital frmouthospital=new frmOutHospital();
				frmouthospital.MdiParent =this.MdiParent;
				frmouthospital.Show(); 
				frmouthospital.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmConsultation frmconsultation=new frmConsultation();
				frmconsultation.MdiParent =this.MdiParent;
				frmconsultation.Show(); 
				frmconsultation.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientCaseHistory_SetForm frmForm=new frmInPatientCaseHistory_SetForm();
				frmForm.m_mthSetPatient(objSelectPatient);
				frmForm.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch
			{
				
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainICUIntensiveTend  frmmainicuintensivetend=new frmMainICUIntensiveTend();
				frmmainicuintensivetend.MdiParent =this.MdiParent;
				frmmainicuintensivetend.m_mthDisableSelectPatient(false);
				frmmainicuintensivetend.Show(); 
				frmmainicuintensivetend.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;//new clsPatient(lsvExplorerMid.SelectedItems[0].SubItems[1].Text);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainICUBreath  frmmainicubreath=new frmMainICUBreath();
				frmmainicubreath.MdiParent =this.MdiParent;
				frmmainicubreath.m_mthDisableSelectPatient(false);
				frmmainicubreath.Show(); 
				frmmainicubreath.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniPatientInfoManage_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)lsvExplorerMid.SelectedItems[0].Tag;
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPatientInfoManage frm = new frmPatientInfoManage(lsvExplorerMid.SelectedItems[0]);
				frm.m_mthSetPatientBaseInfo(objSelectPatient);
				frm.MdiParent = this.MdiParent;
				frm.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}		
		}

		private void mniEKGOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.frmEKGOrder frmChild = new iCare.frmEKGOrder();
				frmChild.MdiParent =this.MdiParent;
				frmChild.Show(); 
				frmChild.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniNuclearOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.frmNuclearOrder frmChild = new iCare.frmNuclearOrder();
				frmChild.MdiParent =this.MdiParent;
				frmChild.Show(); 
				frmChild.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniPSGOrder_Click(object sender, System.EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.frmPSGOrder frmChild = new iCare.frmPSGOrder();
				frmChild.MdiParent =this.MdiParent;
				frmChild.Show(); 
				frmChild.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		
		#endregion

		#region �ı�ѡ�нڵ�(���š������ĸı��¼�)
		private void trvExplorer_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			string strTemp = "";
			if(this.trvExplorer.SelectedNode!=this.trvExplorer.Nodes[0] && this.trvExplorer.SelectedNode.Parent.Tag != null)
				strTemp = this.trvExplorer.SelectedNode.Parent.Tag.ToString().Substring(5);
			if(m_strCurrentDept!="" && strTemp == m_strCurrentDept)
			{				
			}
			else
			{
				m_strCurrentDept = strTemp;
				m_blnChangedDept = true;

				for(int i=0;i<ctmHRPExplorer.MenuItems.Count;i++)
				{
					m_mthSetMenuItemsEnable(ctmHRPExplorer.MenuItems[i]);
				}
				
			}

			if(m_blnCanSeachEventTakePlace==false)
				return;			

			this.Cursor=Cursors.WaitCursor;
			if(this.trvExplorer.SelectedNode.Tag !=null && this.trvExplorer.SelectedNode.Tag.ToString().IndexOf("Area:")==0)
			{							
				#region ����,�ָ�Ϊȡ����������,ֱ�ӴӲ�����ȡ����,Jacky-2003-5-29
				lsvExplorerMid.Items.Clear();
				clsInPatientBed[] objBedArr;
				clsPatient []  objPatientArr;
				m_objDepartmentManager.m_lngGetAllBedAndPatientInArea(this.trvExplorer.SelectedNode.Tag.ToString().Substring(5),out objBedArr,out objPatientArr);
				if(objBedArr !=null)
				{
					for(int j2=0;j2<objBedArr.Length;j2++)
					{
						ListViewItem lviNewItem;
						if(objPatientArr[j2] !=null)
						{
							if(objPatientArr[j2].m_StrSex=="��")
							{
								lviNewItem = new ListViewItem(new string[]{
																			  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "��",
																			  objPatientArr[j2].m_StrInPatientID
																		  });
								lviNewItem.ForeColor = Color.Black;
								//								if(objBedArr[j2].m_StrBed_Status=="") lviNewItem.ForeColor = Color.FromArgb(6,231,236);
								//								else lviNewItem.ForeColor = Color.Black;
									
							}
							else if(objPatientArr[j2].m_StrSex=="Ů")
							{
								lviNewItem = new ListViewItem(new string[]{
																			  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "Ů",
																			  objPatientArr[j2].m_StrInPatientID
																		  });
								lviNewItem.ForeColor = Color.Red;
								//								if(objBedArr[j2].m_StrBed_Status=="") lviNewItem.ForeColor = Color.Orange;
								//								else lviNewItem.ForeColor = Color.Black;
							}
							else
							{			
								lviNewItem = new ListViewItem(new string[]{
																			  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "����",
																			  objPatientArr[j2].m_StrInPatientID
																		  });
								lviNewItem.ForeColor = Color.Green;
								//								if(objBedArr[j2].m_StrBed_Status=="") lviNewItem.ForeColor = Color.White;
								//								else lviNewItem.ForeColor = Color.Black;
							}

							lviNewItem.ImageIndex = 1;
						}
						else 
						{
							lviNewItem = new ListViewItem(new string[]{
																		  objBedArr[j2].m_StrBedName ,
																		  ""
																	  });

							lviNewItem.ImageIndex = 0;
						}
						lviNewItem.Tag = objPatientArr[j2];
						lsvExplorerMid.Items.Add(lviNewItem);

						//Add by jli 2004-11-29
						//////						m_mthReadMessage();
						//////						m_mthViewMessage();
						//Add End
					}
					//Add by DSL 2004-12-15
					//��ʱ���ν���SQL server�汾���ԣ���д�ô洢���̺��� tfzhang 2005-6-21 17:36:09

					m_mthReadMessage();
					m_mthViewMessage();
					//Add End

				}
				#endregion				
				
				this.lsvExplorerMid.Visible=true;

				#region �ı���Ӧ�Ĳ�����ʾ
				m_blnCanSeachEventTakePlace=false;
				this.m_cboDept.Text=this.trvExplorer.SelectedNode.Parent.Text;
				this.m_cboArea.ClearItem();
				clsInPatientArea[] objAreaArr;
				m_objDepartmentManager.m_lngGetAllAreaInDept(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,out objAreaArr);
				if(objAreaArr !=null)
				{	
					this.m_cboArea.AddRangeItems(objAreaArr);
				}

				this.m_cboArea.Text=this.trvExplorer.SelectedNode.Text;
				m_blnCanSeachEventTakePlace=true;
				#endregion �ı���Ӧ�Ĳ�����ʾ
				
			}
			else
			{
				this.lsvExplorerMid.Visible =false;
				if(this.trvExplorer.SelectedNode.Tag !=null && this.trvExplorer.SelectedNode.Tag.ToString().IndexOf("Dept:")==0)
				{								
					this.m_cboArea.ClearItem();
					clsInPatientArea[] objAreaArr;
					m_objDepartmentManager.m_lngGetAllAreaInDept(this.trvExplorer.SelectedNode.Tag.ToString().Substring(5),out objAreaArr);
					if(objAreaArr !=null)
					{	
						this.m_cboArea.AddRangeItems(objAreaArr);
					}

					#region �ı���Ӧ�Ĳ�����ʾ
					m_blnCanSeachEventTakePlace=false;
					m_cboDept.Text=this.trvExplorer.SelectedNode.Text;
					m_blnCanSeachEventTakePlace=true;
					#endregion �ı���Ӧ�Ĳ�����ʾ
				}
			}
			this.Cursor=Cursors.Default;
		}

		private void m_cboDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_blnCanSeachEventTakePlace==false)
				return;					
				
			this.Cursor=Cursors.WaitCursor;
			this.lsvExplorerMid.Visible=false;
			this.m_cboArea.ClearItem();
			clsInPatientArea[] objAreaArr;
			MDIParent.s_ObjCurrDepartment=(clsDepartment)m_cboDept.SelectedItem;
			MDIParent.s_ObjCurrInPatientArea=null;
			m_objDepartmentManager.m_lngGetAllAreaInDept(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,out objAreaArr);
			if(objAreaArr !=null)
			{	
				this.m_cboArea.AddRangeItems(objAreaArr);
			}

			#region �ı���Ӧ��ѡ�����ڵ���ʾ
			m_blnCanSeachEventTakePlace=false;
			foreach(TreeNode node in this.trvExplorer.Nodes[0].Nodes )
				if(node.Tag !=null && node.Tag.ToString().IndexOf("Dept:")==0 && node.Tag.ToString().Substring(5)==((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID)
				{
					this.trvExplorer.SelectedNode=node;
					break;
				}
			m_blnCanSeachEventTakePlace=true;
			#endregion �ı���Ӧ��ѡ�����ڵ���ʾ
			this.Cursor=Cursors.Default;
		}

		private void m_cboArea_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_blnCanSeachEventTakePlace==false)
				return;

			this.Cursor=Cursors.WaitCursor;

			MDIParent.s_ObjCurrInPatientArea=(clsInPatientArea)m_cboArea.SelectedItem;

			#region ����,�ָ�Ϊȡ����������,ֱ�ӴӲ�����ȡ����,Jacky-2003-5-29
			lsvExplorerMid.Items.Clear();
			clsInPatientBed[] objBedArr;
			clsPatient []  objPatientArr;
			m_objDepartmentManager.m_lngGetAllBedAndPatientInArea(((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_StrAreaID,out objBedArr,out objPatientArr);
			if(objBedArr !=null)
			{
				for(int j2=0;j2<objBedArr.Length;j2++)
				{
					ListViewItem lviNewItem;
					if(objPatientArr[j2] !=null)
					{
						//if(objPatientArr[j2].m_ObjPeopleInfo.m_StrSex=="��")
						if(objPatientArr[j2].m_StrSex=="��")
						{
							lviNewItem = new ListViewItem(new string[]{
																		  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "��",
																		  objPatientArr[j2].m_StrInPatientID
																	  });
							lviNewItem.ForeColor = Color.Black;
						}
							//else if(objPatientArr[j2].m_ObjPeopleInfo.m_StrSex=="Ů")
						else if(objPatientArr[j2].m_StrSex=="Ů")
						{
							lviNewItem = new ListViewItem(new string[]{
																		  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "Ů",
																		  objPatientArr[j2].m_StrInPatientID
																	  });

							lviNewItem.ForeColor = Color.Red;
						}
						else
						{
								
	
							lviNewItem = new ListViewItem(new string[]{
																		  objBedArr[j2].m_StrBedName + "\n"+objPatientArr[j2].m_StrName + " " + "����",
																		  objPatientArr[j2].m_StrInPatientID
																	  });
							lviNewItem.ForeColor = Color.Green;
						}

						lviNewItem.ImageIndex = 1;							
					}
					else 
					{
						lviNewItem = new ListViewItem(new string[]{
																	  objBedArr[j2].m_StrBedName ,
																	  ""
																  });

						lviNewItem.ImageIndex = 0;
					}
					lviNewItem.Tag = objPatientArr[j2];
					lsvExplorerMid.Items.Add(lviNewItem);	

					//Add by jli 2004-11-29
					//////					m_mthReadMessage();
					//////					m_mthViewMessage();
					//Add End
				}
				//Add by DSL 2004-12-15
				//��ʱ���ν���SQL server�汾���ԣ���д�ô洢���̺��� tfzhang 2005-6-21 17:36:09

				m_mthReadMessage();
				m_mthViewMessage();
				//Add End

			}
			#endregion				
				
			this.lsvExplorerMid.Visible=true;

			#region �ı���Ӧ��ѡ�����ڵ���ʾ
			m_blnCanSeachEventTakePlace=false;
			TreeNode trnParentNode=null;
			if(trvExplorer.SelectedNode.Tag !=null && trvExplorer.SelectedNode.Tag.ToString().IndexOf("Area:")==0)
				trnParentNode=this.trvExplorer.SelectedNode.Parent;
			else if(trvExplorer.SelectedNode.Tag !=null && trvExplorer.SelectedNode.Tag.ToString().IndexOf("Dept:")==0)
				trnParentNode=this.trvExplorer.SelectedNode;
			else 
				foreach(TreeNode node in this.trvExplorer.Nodes[0].Nodes )
					if(node.Tag !=null && node.Tag.ToString().IndexOf("Dept:")==0 && node.Tag.ToString().Substring(5)==((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID)
					{
						trnParentNode=node;
						break;
					}
			if(trnParentNode==null)
				return;

			foreach(TreeNode node in trnParentNode.Nodes )
				if(node.Tag !=null && node.Tag.ToString().IndexOf("Area:")==0 && node.Tag.ToString().Substring(5)==((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_StrAreaID)
				{
					this.trvExplorer.SelectedNode=node;
					break;
				}
			MDIParent.s_blnRevisitBegin = true;
			m_blnCanSeachEventTakePlace=true;
			#endregion �ı���Ӧ��ѡ�����ڵ���ʾ
			this.Cursor=Cursors.Default;
		}	
		
		#endregion �ı�ѡ�нڵ�(���š������ĸı��¼�)

		#region Copy,Cut,Paste
		/// <summary>
		/// ���Ʋ���
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngCopy()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;
			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						if(((ctlRichTextBox)ctlControl).Text != "")
						{
							((ctlRichTextBox)ctlControl).Copy();
							return 1;
						}
						break;
					
					case "RichTextBox":
						if(((RichTextBox)ctlControl).Text != "")
						{
							((RichTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "TextBox":
						if(((TextBox)ctlControl).Text != "")
						{
							((TextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "ctlBorderTextBox":
						if(((ctlBorderTextBox)ctlControl).Text != "")
						{
							((ctlBorderTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					case "DataGridTextBox":
						if(((DataGridTextBox)ctlControl).Text != "")
						{
							((DataGridTextBox)ctlControl).Copy();
							return 1;
						}
						break;

					default:
						Clipboard.SetDataObject("");
						break;
				}
			}

			return 0;
		}

		/// <summary>
		/// ���в���
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngCut()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;
			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						if(((ctlRichTextBox)ctlControl).Text != "")
						{
							((ctlRichTextBox)ctlControl).Cut();
							return 1;
						}
						break;
					
					case "RichTextBox":
						if(((RichTextBox)ctlControl).Text != "")
						{
							((RichTextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "TextBox":
						if(((TextBox)ctlControl).Text != "")
						{
							((TextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "ctlBorderTextBox":
						if(((ctlBorderTextBox)ctlControl).Text != "")
						{
							((ctlBorderTextBox)ctlControl).Cut();
							return 1;
						}
						break;

					case "DataGridTextBox":
						if(((DataGridTextBox)ctlControl).Text != "")
						{
							((DataGridTextBox)ctlControl).Cut();
							return 1;
						}
						break;
				}
			}

			return 0;
		}

		/// <summary>
		/// ճ������
		/// </summary>
		/// <returns>�������</returns>
		public long m_lngPaste()
		{
			Control ctlControl = this.ActiveControl;
			string strTypeName = ctlControl.GetType().Name;

			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
			{
				switch(strTypeName)
				{
					case "ctlRichTextBox":
						((ctlRichTextBox)ctlControl).Paste();
						break;

					case "RichTextBox":
						((RichTextBox)ctlControl).Paste();
						break;

					case "TextBox":
						((TextBox)ctlControl).Paste();
						break;

					case "ctlBorderTextBox":
						((ctlBorderTextBox)ctlControl).Paste();
						break;

					case "DataGridTextBox":
						((DataGridTextBox)ctlControl).Paste();
						break;
				}
				return 1;
			}

			return 0;
		}
		#endregion

		#region �����ұ�ListView�в����ϵ�ToolTip��ʾ��Ϣ��Jacky-2003-5-28
		private void lsvExplorerMid_MouseHover(object sender, System.EventArgs e)
		{			
            //string strInpatientStatic="";
			
            //if(lsvExplorerMid.SelectedItems.Count>0 && lsvExplorerMid.SelectedItems[0].Focused && lsvExplorerMid.SelectedItems[0].Tag !=null && lsvExplorerMid.SelectedItems[0].Tag.GetType().Name=="clsPatient")
            //{
            //    clsPatient objPatient=(clsPatient)lsvExplorerMid.SelectedItems[0].Tag;

            //    string strMessages="";
            //    if(dtbPatientMessages.Rows.Count>0)
            //    {
            //        //for(int i=0;i<dtbPatientMessages.Rows.Count;i++)
            //        //{
            //        dtbPatientMessages.PrimaryKey=new DataColumn[]{dtbPatientMessages.Columns[1]};
            //        DataRow drRes=dtbPatientMessages.Rows.Find(objPatient.m_StrInPatientID.Trim());
            //        if (drRes!=null)
            //        {
            //            strMessages="\n\r\n\r������д��ʾ:"+"\n\r"+drRes[5].ToString().Trim();
            //            //strInpatientStatic="\n\r"+drRes[10].ToString().Trim();
            //            strInpatientStatic="\n\r" + "����״̬ : " + ((clsPatient)lsvExplorerMid.SelectedItems[0].Tag).m_ObjPeopleInfo.m_StrBedClipState;
            //        }
            //        //}
            //    }

            //    m_ttpBed.SetToolTip(lsvExplorerMid,"���˻�����Ϣ:\n\r"+objPatient.m_ObjPeopleInfo.m_StrFirstName+","+objPatient.m_ObjPeopleInfo.m_StrSex 
            //        +",סԺ��:"+objPatient.m_StrInPatientID+"\n��Ժ����:"+objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToShortDateString()+strInpatientStatic+strMessages);

            //    m_ttpBed.AutoPopDelay=30000;
				
            //}	
            //else 
            //    m_ttpBed.AutoPopDelay=1000;
		}

		private void lsvExplorerMid_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count>0)
			{
				lsvExplorerMid.SelectedItems[0].Focused=false;	
			}
		}
		#endregion �����ұ�ListView�в����ϵ�ToolTip��ʾ��Ϣ��Jacky-2003-5-28

		#region ��ʾ����Explorer
		private void m_picExpand_Click(object sender, System.EventArgs e)
		{			
			m_mthShowExplorer(ref m_blnIsExpand);			
		}

		private void m_mthShowExplorer(ref bool p_blnIsExpand)
		{
			if(p_blnIsExpand)
			{
				p_blnIsExpand=false;
				this.m_picExpand.Image=imgUserclose;				
				trvExplorer.Visible=false;
			}
			else 
			{
				p_blnIsExpand=true;
				this.m_picExpand.Image=imgUseropen;		
				trvExplorer.Visible=true;				
			}		
		}

		#endregion ��ʾ����Explorer

		private void ctmHRPExplorer_Popup(object sender, System.EventArgs e)
		{
			//			string strDeptID = null;
			//
			////			if(this.trvExplorer.SelectedNode.Parent = this.trvExplorer.Nodes[0])
			//			strDeptID = this.trvExplorer.SelectedNode.Tag.ToString().Substring(5);
			//
			//			if(m_blnChangedDept)
			//			{
			//				for(int i=0;i<ctmHRPExplorer.MenuItems.Count;i++)
			//				{
			//					m_mthSetMenuItemsUsable(ctmHRPExplorer.MenuItems[i]);
			//				}
			//
			//				m_blnChangedDept = false;
			//			}
		}

		/// <summary>
		/// �����������ʱ��MenuItem��ԭΪEnable
		/// </summary>
		/// <param name="p_mniItem"></param>
		private void m_mthSetMenuItemsEnable(MenuItem p_mniItem)
		{
			if(p_mniItem.MenuItems.Count<=0)
				p_mniItem.Enabled = true;

			for(int i=0; i<p_mniItem.MenuItems.Count; i++)
			{
				m_mthSetMenuItemsEnable(p_mniItem.MenuItems[i]);
			}
		}

		/// <summary>
		/// ����Ȩ�޽�MenuItem��ΪDisabled���ݹ���ǲ��ϵص��Լ���
		/// </summary>
		/// <param name="p_mniItem"></param>
		private void m_mthSetMenuItemsUsable(MenuItem p_mniItem)
		{
			enmPrivilegeSF enmSF = m_enmGetSFbyMenuItem(p_mniItem);

			if(enmSF != enmPrivilegeSF.HRPExplorer)
			{
				//				if(!clsPublicFunction.s_blnCheckCurrentPrivilege(m_strCurrentDept,enmSF,PrivilegeData.enmPrivilegeOperation.Read))
				//				p_mniItem.Enabled = false;
				if(objPIArr == null)
					return;

				for(int i1=0;i1<objPIArr.Length;i1++)
				{
					if(objPIArr[i1] == null)
						continue;
					
					if(objPIArr[i1].m_objGetOISF(m_strCurrentDept,(int)enmSF,(int)enmPrivilegeOperation.Read) != null)
					{
						p_mniItem.Enabled = true;
						break;
					}
					else
						p_mniItem.Enabled = false;
				}
					
			}

			for(int i=0;i<p_mniItem.MenuItems.Count;i++)
			{
				m_mthSetMenuItemsUsable(p_mniItem.MenuItems[i]);
			}
		}

		private enmPrivilegeSF m_enmGetSFbyMenuItem(MenuItem p_mniItem) 
		{
			#region �Ҽ��˵�ȫ������

			switch(p_mniItem.Text) 
			{
				case "סԺ����":
					return enmPrivilegeSF.frmInPatientCaseHistory;					
				case "סԺ����ģʽ2":
					return enmPrivilegeSF.frmInPatientCaseHistoryMode1;					
				case "���̼�¼":
					return enmPrivilegeSF.frmSubDiseaseTrack;					
				case "SPECT������뵥":
					return enmPrivilegeSF.frmSPECT;					
				case "��ѹ���������뵥":
					return enmPrivilegeSF.frmHighOxygen;					
				case "B�ͳ������������뵥":
					return enmPrivilegeSF.frmBUltrasonicCheckOrder;
				case "CT������뵥":
					return enmPrivilegeSF.frmCTCheckOrder;
				case "X�����뵥":
					return enmPrivilegeSF.frmXRayCheckOrder;
				case "���������֯�ͼ쵥":
					return enmPrivilegeSF.frmPathologyOrgCheckOrder;
				case "MRI���뵥":
					return enmPrivilegeSF.frmMRIApply;
				case "ʵ���Ҽ������뵥":
					return enmPrivilegeSF.frmLabAnalysisOrder;
				case "ʵ���Ҽ��鱨�浥":
					return enmPrivilegeSF.frmLabCheckReport;
				case "����֪��ͬ����":
					return enmPrivilegeSF.frmOperationAgreedRecord;
				case "��ǰС��":
					return enmPrivilegeSF.frmBeforeOperationSummary;
				case "������¼��":
					return enmPrivilegeSF.frmOperationRecordDoctor;
				case "ICUת���¼":
					return enmPrivilegeSF.frmPICUShiftInForm;
				case "ICUת����¼":
					return enmPrivilegeSF.frmPICUShiftOutForm;
				case "SIRS�������":
					return enmPrivilegeSF.SIRSEvaluation;
				case "����Glasgow��������":
					return enmPrivilegeSF.ImproveGlasgowComaEvaluation;
				case "���Է���������":
					return enmPrivilegeSF.LungInjuryEvaluation;
				case "������Σ�ز�������":
					return enmPrivilegeSF.NewBabyInjuryCaseEvaluation;
				case "С��Σ�ز�������":
					return enmPrivilegeSF.BabyInjuryCaseEvaluation;
				case "APACHEII ����":
					return enmPrivilegeSF.APACHEIIValuation;
				case "APACHEIII ����":
					return enmPrivilegeSF.APACHEIIIValuation;
				case "TISS-28����":
					return enmPrivilegeSF.frmTISSValuation;
				case "���Ʒ���":
					return enmPrivilegeSF.frmICUTrend;
				case "סԺ������ҳ":
					return enmPrivilegeSF.frmInHospitalMainRecord;
				case "�����������ֱ�":
					return enmPrivilegeSF.frmQCRecord;
				case "��Ժ��������":
					return enmPrivilegeSF.frmInPatientEvaluate;
				case "�� �� ��":
					return enmPrivilegeSF.frmThreeMeasureRecord;
				case "һ�㻤���¼":
					return enmPrivilegeSF.frmMainGeneralNurseRecord;
				case "�۲���Ŀ��¼��":
					return enmPrivilegeSF.frmWatchItemTrack;
				case "Σ�ػ��߻����¼":
					return enmPrivilegeSF.frmIntensiveTendMain;
				case "ICUΣ�ػ��߻����¼":
					return enmPrivilegeSF.frmICUIntensiveTendRecord;
				case "���������¼":
					return enmPrivilegeSF.frmOperationRecord;
				case "������е�����ϵ�����":
					return enmPrivilegeSF.frmOperationEquipmentQty;
				case "��Ժ��¼":
					return enmPrivilegeSF.frmOutHospital;
				case "�����¼":
					return enmPrivilegeSF.frmConsultation;
				case "Σ��֢�໤�����ػ���¼��":
					return enmPrivilegeSF.frmMainICUIntensiveTend;
				case "����ICU���������Ƽ໤��¼��":
					return enmPrivilegeSF.frmMainICUBreath;
				case "Ӱ�񱨸浥":
					return enmPrivilegeSF.frmImageReport;
				case "Ӱ��ԤԼ��ѯ":
					return enmPrivilegeSF.frmImageBookingSearch;
				case "�ĵ�ͼ���뵥":
					return enmPrivilegeSF.frmEKGOrder;
				case "���Զർ˯��ͼ������뵥":
					return enmPrivilegeSF.frmNuclearOrder;
				case "��ҽѧ������뵥":
					return enmPrivilegeSF.frmPSGOrder;
			}
			#endregion �Ҽ��˵�ȫ������
			return enmPrivilegeSF.HRPExplorer;
		}

		#region �Զ����
		private clsCustom_SubmitValue[] m_objCustomForms;
		/// <summary>
		/// Load���Զ����
		/// </summary>
		private void m_mthLoadCustomForms()
		{
			long lngRes = new iCare.CustomForm.clsCustomFormDomain().m_lngGetSubmitForms(MDIParent.OperatorID,clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID,out m_objCustomForms);
			if(lngRes <= 0 || m_objCustomForms == null || m_objCustomForms.Length == 0)
				return;
			MenuItem mniRoot = new MenuItem("�Զ����");
			for(int i = 0; i < m_objCustomForms.Length; i++)
			{
				MenuItem mniCustormForm = mniRoot.MenuItems.Add(m_objCustomForms[i].m_strFormName,new EventHandler(m_mthShowCustomForm));				
			}
			ctmHRPExplorer.MenuItems.Add(mniRoot);
		}

		/// <summary>
		/// ���Զ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_mthShowCustomForm(object sender,EventArgs e)
		{
			if(lsvExplorerMid.SelectedItems.Count == 0 || lsvExplorerMid.SelectedItems[0].Tag==null)
				return;

			clsPatient objSelectPatient = (clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.CustomForm.frmCustomFormBase frmChild = new iCare.CustomForm.frmCustomFormBase(m_objCustomForms[((MenuItem)sender).Index]);
				frmChild.MdiParent =this.MdiParent;
				frmChild.Show(); 
				frmChild.m_mthSetPatient(objSelectPatient);
				MDIParent.s_ObjCurrentPatient = objSelectPatient;
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		#endregion

		/// <summary>
		/// ��ȡר��סԺ��������
		/// </summary>
		public static string c_strGetInpatMedRecType(clsPatient p_objPatient)
		{
			//			if (p_objPatient == null)
			return null;
			//			//������������,��һ��ΪDeptID,�ڶ���ΪAreaID,������ΪTypeID,������ΪTypeName
			//			clsInpatMedRec_Type_Dept[] objConentArr = null;
			//			long lngRes=new clsDepartmentManager().m_lngGetCaseType(p_objPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjDept.m_ObjDept.m_StrDeptID,
			//				p_objPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID,out objConentArr);
			//			if(lngRes<=0)
			//				return null;
			//			if(objConentArr.Length > 1)
			//			{
			//				using(frmInPatMedRecChoose frmChoose = new frmInPatMedRecChoose(objConentArr))
			//				{
			//					if(frmChoose.ShowDialog() == DialogResult.OK)
			//						return frmChoose.m_StrResult;
			//				}
			//				return null;
			//			}
			//			else
			//				return objConentArr[0].m_strTypeID;

		}

		//		/// <summary>
		//		/// ˢ��ListView����ʾЧ�� Add By jli 2004-11-29
		//		/// </summary>
		//		/// <param name="IndexOfListViewItem">ListViewѡ��</param>
		//		/// <param name="Type">ˢ������</param>
		//		private void m_mthRefreshImage(int IndexOfListViewItem,int Type)
		//		{
		//			switch(Type)
		//			{
		//				case 0:
		//					break;
		//				case 1:
		//					timRefreshEmployee.Enabled=true;
		//					break;
		//				case 2:
		//					lsvExplorerMid.Items[IndexOfListViewItem].ImageIndex=3;
		//					break;
		//			}
		//		}

		/// <summary>
		/// Timer���� Add By jli 2004-11-29
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timRefreshEmployee_Tick(object sender, System.EventArgs e)
		{
			//			GC.Collect();
			//			for(int i=0;i<lsvExplorerMid.Items.Count;i++)
			//			{
			//				if(lsvExplorerMid.Items[i].ImageIndex==3)
			//				{
			//					lsvExplorerMid.Items[i].ImageIndex=4;
			//				}
			//				else if (lsvExplorerMid.Items[i].ImageIndex==4)
			//				{
			//					lsvExplorerMid.Items[i].ImageIndex=3;
			//				}
			//			}
		}

		/// <summary>
		/// ��ȡ���˵���Ϣ����
		/// </summary>
		private void m_mthReadMessage()
		{
			
			
            //DataTable dtbMessages=new DataTable();
            //string strDeptID="";
            //long lngRes;

            //dtbPatientMessages.Clear();

            //#region ������������������
            //if(m_cboDept.SelectedItem!=null)
            //{
            //    strDeptID=((clsDepartment)m_cboDept.SelectedItem).m_StrDeptID.Trim();
            //}
            //else
            //{
            //    return;
            //}
            //lngRes = new com.digitalwave.emr.HospitalManagerService.clsCaseMessageServ().m_lngProcessMessage(strDeptID,ref dtbMessages);

            //string strMsgs="";
            //string strINPATIENTID="";
            //string strInPatientStatic="";
            //if (dtbMessages.Rows.Count>0)
            //{
            //    strINPATIENTID=dtbMessages.Rows[0]["INPATIENTID"].ToString().Trim();
            //}

            //int iNum=0;

            //for(int i=0;i<dtbMessages.Rows.Count;i++)
            //{	
            //    if (dtbMessages.Rows[i]["INPATIENTID"].ToString().Trim()==strINPATIENTID.Trim())
            //    {
            //        iNum++;
            //        if(i==0)
            //        {

            //            strMsgs=((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //        }
            //        else
            //        {
            //            strMsgs=strMsgs+"\r\n"+((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //        }
            //    }
            //    else
            //    {
            //        string[] strPatientInfoArr=new string[dtbMessages.Columns.Count+3];
						
            //        strPatientInfoArr[8]=dtbMessages.Rows[i-1]["BEDNAME"].ToString().Trim();
            //        strPatientInfoArr[3]=dtbMessages.Rows[i-1]["PATIENTNAME"].ToString().Trim();
            //        strPatientInfoArr[4]=dtbMessages.Rows[i-1]["DOCTORNAME"].ToString().Trim();

            //        strPatientInfoArr[5]=strMsgs;

            //        strPatientInfoArr[0]=dtbMessages.Rows[i-1]["ID"].ToString().Trim();
            //        strPatientInfoArr[1]=dtbMessages.Rows[i-1]["INPATIENTID"].ToString().Trim();
            //        strPatientInfoArr[2]=dtbMessages.Rows[i-1]["INPATIENTDATE"].ToString().Trim();
            //        strPatientInfoArr[6]=dtbMessages.Rows[i-1]["DOCTORID"].ToString().Trim();
            //        strPatientInfoArr[7]=dtbMessages.Rows[i-1]["BEDID"].ToString().Trim();
					
            //        //�����и�Ϊ0��ʾ����������
            //        strPatientInfoArr[9]="0";
            //        //					clsBedCardValue objBedCardValue = new clsBedCardValue();
            //        //					clsBedCardManageDomain objBedCardDomain=new clsBedCardManageDomain();
            //        //					objBedCardValue.m_strInPatientID = strPatientInfoArr[1];
            //        //					objBedCardValue.m_strInPatientDate = strPatientInfoArr[2];
            //        //					lngRes = objBedCardDomain.m_lngGetBedCardValue(ref objBedCardValue);
            //        //					if(lngRes <= 0 || objBedCardValue.m_strDoc_ManageBed == null)
            //        //					{
            //        //						strInPatientStatic="����״̬ : ��";
            //        //					}
            //        //					else
            //        //					{
            //        //						switch (objBedCardValue.m_intState)
            //        //						{
            //        //							case 0:
            //        //								strInPatientStatic="����״̬ : �ȶ�";
            //        //								break;
            //        //							case 1:
            //        //								strInPatientStatic="����״̬ : ����";
            //        //								break;
            //        //							case 2:
            //        //								strInPatientStatic="����״̬ : ����";
            //        //								break;
            //        //							case 3:
            //        //								strInPatientStatic="����״̬ : ��Σ";
            //        //								break;
            //        //							default:
            //        //								strInPatientStatic="����״̬ : ��";
            //        //								break;
            //        //						}
            //        //								
            //        //					}
            //        //					strPatientInfoArr[10]=strInPatientStatic;

            //        dtbPatientMessages.Rows.Add(strPatientInfoArr);
            //        iNum=1;
            //        strMsgs=((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //    }
            //    strINPATIENTID=dtbMessages.Rows[i]["INPATIENTID"].ToString().Trim();
            //}

            //#region �����һ��

            //if (dtbMessages.Rows.Count>0)
            //{

            //    string[] strPatientInfoAr=new string[dtbMessages.Columns.Count+3];
						
            //    strPatientInfoAr[8]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["BEDNAME"].ToString().Trim();
            //    strPatientInfoAr[3]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["PATIENTNAME"].ToString().Trim();
            //    strPatientInfoAr[4]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["DOCTORNAME"].ToString().Trim();
            //    strPatientInfoAr[5]=strMsgs;

            //    strPatientInfoAr[0]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["ID"].ToString().Trim();
            //    strPatientInfoAr[1]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["INPATIENTID"].ToString().Trim();
            //    strPatientInfoAr[2]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["INPATIENTDATE"].ToString().Trim();
            //    strPatientInfoAr[6]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["DOCTORID"].ToString().Trim();
            //    strPatientInfoAr[7]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["BEDID"].ToString().Trim();
            //    //�����и�Ϊ0��ʾ����������
            //    strPatientInfoAr[9]="0";	

            //    //				clsBedCardValue objBedCardValue = new clsBedCardValue();
            //    //				clsBedCardManageDomain objBedCardDomain=new clsBedCardManageDomain();
            //    //				objBedCardValue.m_strInPatientID = strPatientInfoAr[1];
            //    //				objBedCardValue.m_strInPatientDate = strPatientInfoAr[2];
            //    //				lngRes = objBedCardDomain.m_lngGetBedCardValue(ref objBedCardValue);
            //    //				if(lngRes <= 0 || objBedCardValue.m_strDoc_ManageBed == null)
            //    //				{
            //    //					strInPatientStatic="����״̬ : ��";
            //    //				}
            //    //				else
            //    //				{
            //    //					switch (objBedCardValue.m_intState)
            //    //					{
            //    //						case 0:
            //    //							strInPatientStatic="����״̬ : �ȶ�";
            //    //							break;
            //    //						case 1:
            //    //							strInPatientStatic="����״̬ : ����";
            //    //							break;
            //    //						case 2:
            //    //							strInPatientStatic="����״̬ : ����";
            //    //							break;
            //    //						case 3:
            //    //							strInPatientStatic="����״̬ : ��Σ";
            //    //							break;
            //    //						default:
            //    //							strInPatientStatic="����״̬ : ��";
            //    //							break;
            //    //					}
            //    //								
            //    //				}
            //    //				strPatientInfoAr[10]=strInPatientStatic;

            //    DataRow dtrAdd=dtbPatientMessages.Rows.Add(strPatientInfoAr);
            //}
            //#endregion

            //#endregion

            //#region ����Ԥ��һ��Сʱ������
            //if(m_cboDept.SelectedItem!=null)
            //{
            //    strDeptID=((clsDepartment)m_cboDept.SelectedItem).m_StrDeptID.Trim();
            //}
            //else
            //{
            //    return;
            //}
			
            //lngRes = new com.digitalwave.emr.HospitalManagerService.clsCaseMessageServ().m_lngProcessMessageForHour(strDeptID,ref dtbMessages);

            //strMsgs="";
            //strINPATIENTID="";

            //if (dtbMessages.Rows.Count>0)
            //{
            //    strINPATIENTID=dtbMessages.Rows[0]["INPATIENTID"].ToString().Trim();
            //}

            //iNum=0;

            //for(int i=0;i<dtbMessages.Rows.Count;i++)
            //{	
            //    if(dtbPatientMessages.Rows.Count>0)
            //    {
            //        dtbPatientMessages.PrimaryKey=new DataColumn[]{dtbPatientMessages.Columns[1]};
            //        DataRow drRes=dtbPatientMessages.Rows.Find(dtbMessages.Rows[i]["INPATIENTID"].ToString().Trim());
            //        if(drRes!=null)
            //            continue;
            //    }
            //    if (dtbMessages.Rows[i]["INPATIENTID"].ToString().Trim()==strINPATIENTID.Trim())
            //    {
            //        iNum++;
            //        if(i==0)
            //        {
            //            strMsgs=((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //        }
            //        else
            //        {
            //            strMsgs=strMsgs+"\r\n"+((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //        }
            //    }
            //    else
            //    {
            //        string[] strPatientInfoArr=new string[dtbMessages.Columns.Count+3];
						
            //        strPatientInfoArr[8]=dtbMessages.Rows[i-1]["BEDNAME"].ToString().Trim();
            //        strPatientInfoArr[3]=dtbMessages.Rows[i-1]["PATIENTNAME"].ToString().Trim();
            //        strPatientInfoArr[4]=dtbMessages.Rows[i-1]["DOCTORNAME"].ToString().Trim();
            //        strPatientInfoArr[5]=strMsgs;

            //        strPatientInfoArr[0]=dtbMessages.Rows[i-1]["ID"].ToString().Trim();
            //        strPatientInfoArr[1]=dtbMessages.Rows[i-1]["INPATIENTID"].ToString().Trim();
            //        strPatientInfoArr[2]=dtbMessages.Rows[i-1]["INPATIENTDATE"].ToString().Trim();
            //        strPatientInfoArr[6]=dtbMessages.Rows[i-1]["DOCTORID"].ToString().Trim();
            //        strPatientInfoArr[7]=dtbMessages.Rows[i-1]["BEDID"].ToString().Trim();
						
            //        //�����и�Ϊ1��ʾ��Ԥ������
            //        strPatientInfoArr[9]="1";

            //        dtbPatientMessages.Rows.Add(strPatientInfoArr);
            //        iNum=1;
            //        strMsgs=((int)(iNum)).ToString().Trim()+"."+dtbMessages.Rows[i]["TODAYMESSAGE"].ToString().Trim();
            //    }
            //    strINPATIENTID=dtbMessages.Rows[i]["INPATIENTID"].ToString().Trim();
            //}

            //#region �����һ��

            //if (dtbMessages.Rows.Count>0)
            //{

            //    if(dtbPatientMessages.Rows.Count>0)
            //    {
            //        dtbPatientMessages.PrimaryKey=new DataColumn[]{dtbPatientMessages.Columns[1]};
            //        DataRow drRes=dtbPatientMessages.Rows.Find(dtbMessages.Rows[dtbMessages.Rows.Count-1]["INPATIENTID"].ToString().Trim());
            //        if(drRes!=null)
            //            return;
            //    }
            //    string[] strPatientInfoAr=new string[dtbMessages.Columns.Count];
						
            //    strPatientInfoAr[8]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["BEDNAME"].ToString().Trim();
            //    strPatientInfoAr[3]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["PATIENTNAME"].ToString().Trim();
            //    strPatientInfoAr[4]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["DOCTORNAME"].ToString().Trim();
            //    strPatientInfoAr[5]=strMsgs;

            //    strPatientInfoAr[0]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["ID"].ToString().Trim();
            //    strPatientInfoAr[1]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["INPATIENTID"].ToString().Trim();
            //    strPatientInfoAr[2]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["INPATIENTDATE"].ToString().Trim();
            //    strPatientInfoAr[6]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["DOCTORID"].ToString().Trim();
            //    strPatientInfoAr[7]=dtbMessages.Rows[dtbMessages.Rows.Count-1]["BEDID"].ToString().Trim();

            //    //�����и�Ϊ1��ʾ��Ԥ������
            //    strPatientInfoAr[9]="1";	
            //    DataRow dtrAdd=dtbPatientMessages.Rows.Add(strPatientInfoAr);
            //}
            //#endregion
            //#endregion
		}

		/// <summary>
		/// ��ʾ���˵�����
		/// </summary>
		private void m_mthViewMessage()
		{
            //if (lsvExplorerMid.Items.Count>0 && dtbPatientMessages.Rows.Count>0)
            //{
            //    for(int i=0;i<lsvExplorerMid.Items.Count;i++)
            //    {
            //        DataRow dr=null;
            //        if (lsvExplorerMid.Items[i].Tag!=null)
            //        {
            //            dtbPatientMessages.PrimaryKey=new DataColumn[]{dtbPatientMessages.Columns[1]};
            //            dr=dtbPatientMessages.Rows.Find(((clsPatient)lsvExplorerMid.Items[i].Tag).m_StrInPatientID.Trim());
            //        }
            //        if(dr!=null)
            //        {
            //            if(dr[9].ToString().Trim()=="0")
            //            {
            //                //lsvExplorerMid.Items[i].ImageIndex=3;
            //                lsvExplorerMid.Items[i].ImageIndex=4;
            //            }
            //            else if(dr[9].ToString().Trim()=="1")
            //            {
            //                lsvExplorerMid.Items[i].ImageIndex=2;
            //            }
            //        }
            //    }
            //}
		}

		private void mnuCurrentPatient_Click(object sender, System.EventArgs e)
		{
			MDIParent.s_ObjCurrentPatient =(clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
		}

		private void lsvExplorerMid_DoubleClick(object sender, System.EventArgs e)
		{
			MDIParent.s_ObjCurrentPatient =(clsPatient)(lsvExplorerMid.SelectedItems[0].Tag);
		}

		private void lsvExplorerMid_Click(object sender, System.EventArgs e)
		{
			lsvExplorerMid_MouseHover(null,null);	
		}
		
	}
}