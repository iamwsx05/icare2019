//#define FunctionPrivilege
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing; 
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP; 


namespace iCare
{
	public class frmWatchItemTrack : iCare.frmRecordsBase
	{
		#region Define
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcDate;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcTime;
		private cltDataGridDSTRichTextBox m_dtcTemperature;
		private cltDataGridDSTRichTextBox m_dtcHeartRhythm;
		private cltDataGridDSTRichTextBox m_dtcHeartFrequency;
		private cltDataGridDSTRichTextBox m_dtcPulse;
		private cltDataGridDSTRichTextBox m_dtcBreath;
		private cltDataGridDSTRichTextBox m_dtcEchoLeft;
		private cltDataGridDSTRichTextBox m_dtcEchoRight;
		private cltDataGridDSTRichTextBox m_dtcPupilLeft;
		private cltDataGridDSTRichTextBox m_dtcPupilRight;
		private cltDataGridDSTRichTextBox m_dtcBloodOxygenSaturation;
		private cltDataGridDSTRichTextBox m_dtcBedsideBloodSugar;
		private cltDataGridDSTRichTextBox m_dtcBloodPressureS;
		private cltDataGridDSTRichTextBox m_dtcBloodPressureA;
		private cltDataGridDSTRichTextBox m_dtcOutE;
		private cltDataGridDSTRichTextBox m_dtcOutU;
		private cltDataGridDSTRichTextBox m_dtcOutS;
		private cltDataGridDSTRichTextBox m_dtcOutV;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcSign;
		private cltDataGridDSTRichTextBox m_dtcInI;
		private cltDataGridDSTRichTextBox m_dtcInD;
		private System.Windows.Forms.Panel panel1;

		private System.ComponentModel.IContainer components = null;
		
		private string m_strCurrentOpenDate = "";
		private string m_strCreateUserID = "";

		#endregion
		public frmWatchItemTrack()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			m_dtcOutE.m_BlnUnderLineDST = true;
			m_dtcOutS.m_BlnUnderLineDST = true;
			m_dtcOutU.m_BlnUnderLineDST = true;
			m_dtcOutV.m_BlnUnderLineDST = true;
			m_dtcInD.m_BlnUnderLineDST = true;
			m_dtcInI.m_BlnUnderLineDST = true;
		}

		protected override Font m_FntHeaderFont
		{
			get
			{
				return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			}
		}

		#region �йش�ӡ������
//		/// <summary>
//		/// ������Σ�ػ����¼�ģ���ӡ�����ĵ���
//		/// </summary>		
//		private clsPrintRichTextContext m_objPrintContext;
		/// <summary>
		/// ��ǰ�е�Y����
		/// </summary>
		private int m_intPosY = (int)enmRecordRectangleInfo.TopY+150;
		/// <summary>
		/// ÿ�������еĸ߶�
		/// </summary>
		int intTempDeltaY = 38;	
		/// <summary>
		/// ���������
		/// </summary>
		private Font m_fotTitleFont;
		/// <summary>
		/// ��ͷ������
		/// </summary>
		private Font m_fotHeaderFont;
		/// <summary>
		/// �����ݵ�����
		/// </summary>
		private Font m_fotSmallFont;
		/// <summary>
		/// ��С������
		/// </summary>
		private Font m_fotTinyFont;
		/// <summary>
		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;
		/// <summary>
		/// ˢ��
		/// </summary>
		private SolidBrush m_slbBrush;
		/// <summary>
		/// ��¼��ӡ���ڼ�ҳ
		/// </summary>
		private int m_intNowPage=1;
		/// <summary>
		/// ��ǰ��ӡ�ļ�¼�����
		/// </summary>
		private int m_intCurrentRecord=0;  
		/// <summary>
		/// �ɼ�¼����,׼����ӡһ���¼�¼
		/// </summary>
		bool m_blnBeginPrintNewRecord=true;		

		/// <summary>
		/// ����Ҫ������ʷ�ۼ�����ǰ��¼����
		/// </summary>
		private string[][] m_strValueArr;

		/// <summary>
		/// ��ǰ��¼�����������޸ĵĴε�����
		/// </summary>
		private int m_intNowRowInOneRecord=0; 	

		/// <summary>
		/// ��ȡ�������
		/// </summary>
		private clsPrintPageSettingForRecord m_objPageSetting;
		/// <summary>
		/// ��ӡ�Ĳ��˻�����Ϣ��
		/// </summary>
		private class clsEveryRecordPageInfo
		{
			public string strPatientName;
			public string strSex;
			public string strAge;
			public string strBedNo;
			public string strAreaName;
			public string strDeptName;
			public string strInPatientID;
			//public int intCurrentPate;
			//public int intTotalPages;
			public string strPrintDate;
		}

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		private enum enmRecordRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 200,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 20,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-20,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 38,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 17,	
			/// <summary>
			/// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
			/// </summary>
			VOffSet = 20,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=19,
			/// <summary>
			/// ��һ�������(X),ʱ�䣨����ߣ�
			/// </summary>			
			ColumnsMark1=75,

			/// <summary>
			/// �ڶ��������(X)�����£�����ߣ�
			/// </summary>
			ColumnsMark2=120,

			/// <summary>
			/// ��3�������(X)�����ɣ�����ߣ�
			/// </summary>
			ColumnsMark3=154,

			/// <summary>
			/// ���� ��/�֣�����ߣ�
			/// </summary>
			ColumnsMark4=194,

			/// <summary>
			/// ����������ߣ�
			/// </summary>
			ColumnsMark5=224,

			/// <summary>
			/// ����������ߣ�
			/// </summary>
			ColumnsMark6=254,

			/// <summary>
			/// Ѫѹ������ߣ�
			/// </summary>
			ColumnsMark7=284,

			/// <summary>
			/// ͫ�״�С ������ߣ�
			/// </summary>
			ColumnsMark8=340,

			/// <summary>
			/// ͫ�״�С �ң�����ߣ�
			/// </summary>
			ColumnsMark9=370,

			/// <summary>
			/// ���� ������ߣ�
			/// </summary>
			ColumnsMark10=400,

			/// <summary>
			/// ���� �ң�����ߣ�
			/// </summary>
			ColumnsMark11=440,

			/// <summary>
			/// Ѫ�����Ͷȣ�����ߣ�
			/// </summary>
			ColumnsMark12=480,

			/// <summary>
			/// ����Ѫ�ǣ�����ߣ�
			/// </summary>
			ColumnsMark13=510,

			/// <summary>
			/// ��Һ��������ߣ�
			/// </summary>
			ColumnsMark14=550,

			/// <summary>
			/// ��ʳ��������ߣ�
			/// </summary>
			ColumnsMark15=580,

			/// <summary>
			/// ������������ߣ�
			/// </summary>
			ColumnsMark16=610,

			/// <summary>
			/// �� ��������ߣ�
			/// </summary>
			ColumnsMark17=640,

			ColumnsMark18=670,

			ColumnsMark19=700,

			/// <summary>
			/// ǩ��������ߣ�
			/// </summary>
			ColumnsMark20=730	
		}
		
		/// <summary>
		/// ��ӡԪ��
		/// </summary>
		private enum enmItemDefination
		{
			//����Ԫ��
			InPatientID_Title,
			InPatientID,
			Name_Title,
			Name,
			Sex_Title,
			Sex,
			Age_Title,
			Age,
			Dept_Name_Title,
			Dept_Name,
			BedNo_Title,
			BedNo,
            
			Page_HospitalName,
			Page_Name_Title,
			Page_Title,
			Page_Num,
			Page_Of,
			Page_Count,
					
			Print_Date_Title,
			Print_Date,
			//�����Ԫ��
			RecordDate,
			RecordTime,
			RecordContent,
			RecordSign1,
			RecordSign2,			
		}
	  

		#region �����ӡ��Ԫ�ص������
		private class clsPrintPageSettingForRecord
		{	
			public clsPrintPageSettingForRecord(){}
			
			/// <summary>
			/// ��������
			/// </summary>
			/// <param name="p_intItemName">��Ŀ����</param>
			/// <returns></returns>
			public PointF m_getCoordinatePoint(int p_intItemName)
			{
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                    
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(300f,60f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(225f,100f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,150f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,150f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(150f,150f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(200f,150f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(250f,150f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(300f,150f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(330f,150f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(380f,150f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(550f,150f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(600f,150f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,150f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(720f,150f);
						break;
											
					default:
						m_fReturnPoint = new PointF(400f,400f);
						break;
	
				}
				return m_fReturnPoint;
			}
		}

	    #endregion
	
		#endregion

		/// <summary>
		/// �趨���ڱȽ����ڵĳ�ʼֵ
		/// </summary>
		private DateTime m_dtmPreRecordDate;
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
            this.m_dtcDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcTemperature = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcHeartRhythm = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcHeartFrequency = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPulse = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBreath = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcEchoLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcEchoRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodOxygenSaturation = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBedsideBloodSugar = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureA = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInI = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInD = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOutE = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOutU = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOutS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOutV = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.m_dtcDate,
																										 this.m_dtcTime,
																										 this.m_dtcTemperature,
																										 this.m_dtcHeartRhythm,
																										 this.m_dtcHeartFrequency,
																										 this.m_dtcPulse,
																										 this.m_dtcBreath,
																										 this.m_dtcBloodPressureS,
																										 this.m_dtcBloodPressureA,
																										 this.m_dtcPupilLeft,
																										 this.m_dtcPupilRight,
																										 this.m_dtcEchoLeft,
																										 this.m_dtcEchoRight,
																										 this.m_dtcBloodOxygenSaturation,
																										 this.m_dtcBedsideBloodSugar,
																										 this.m_dtcInI,
																										 this.m_dtcInD,
																										 this.m_dtcOutE,
																										 this.m_dtcOutU,
																										 this.m_dtcOutS,
																										 this.m_dtcOutV,
																										 this.m_dtcSign});
            this.dgtsStyles.RowHeaderWidth = 15;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.HeaderFont = new System.Drawing.Font("����", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(26, 88);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(730, 492);
            // 
            // mniAppend
            // 
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.SystemColors.Window;
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvInPatientDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_trvInPatientDate.ItemHeight = 18;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(20, 86);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(144, 64);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.SystemColors.Control;
            this.lblSex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSex.Location = new System.Drawing.Point(544, 100);
            this.lblSex.Size = new System.Drawing.Size(28, 19);
            this.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.SystemColors.Control;
            this.lblAge.Location = new System.Drawing.Point(612, 100);
            this.lblAge.Size = new System.Drawing.Size(36, 19);
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.AutoSize = false;
            this.lblBedNoTitle.Location = new System.Drawing.Point(360, 93);
            this.lblBedNoTitle.Size = new System.Drawing.Size(48, 19);
            this.lblBedNoTitle.Text = "�� ��:";
            this.lblBedNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.AutoSize = false;
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(352, 126);
            this.lblInHospitalNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(504, 93);
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(504, 126);
            this.lblSexTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(572, 100);
            this.lblAgeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(176, 126);
            this.lblAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(412, 150);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(80, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.BorderColor = System.Drawing.Color.White;
            this.txtInPatientID.Location = new System.Drawing.Point(412, 98);
            this.txtInPatientID.Size = new System.Drawing.Size(84, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(548, 91);
            this.m_txtPatientName.Size = new System.Drawing.Size(84, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(412, 91);
            this.m_txtBedNO.Size = new System.Drawing.Size(68, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(224, 124);
            this.m_cboArea.Size = new System.Drawing.Size(120, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(544, 114);
            this.m_lsvPatientName.Size = new System.Drawing.Size(84, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(412, 114);
            this.m_lsvBedNO.Size = new System.Drawing.Size(80, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(224, 91);
            this.m_cboDept.Size = new System.Drawing.Size(120, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(176, 93);
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(680, 100);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNext.Location = new System.Drawing.Point(480, 91);
            this.m_cmdNext.Size = new System.Drawing.Size(16, 23);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdPre.Location = new System.Drawing.Point(672, 16);
            this.m_cmdPre.Size = new System.Drawing.Size(4, 8);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(676, 97);
            this.m_lblForTitle.Size = new System.Drawing.Size(44, 16);
            this.m_lblForTitle.Text = "�۲���Ŀ��¼��";
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(580, 96);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(662, 36);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // m_dtcDate
            // 
            this.m_dtcDate.Format = "";
            this.m_dtcDate.FormatInfo = null;
            this.m_dtcDate.HeaderText = "����";
            this.m_dtcDate.MappingName = "Date";
            this.m_dtcDate.NullText = "";
            this.m_dtcDate.Width = 80;
            // 
            // m_dtcTime
            // 
            this.m_dtcTime.Format = "";
            this.m_dtcTime.FormatInfo = null;
            this.m_dtcTime.HeaderText = "ʱ��";
            this.m_dtcTime.MappingName = "Time";
            this.m_dtcTime.NullText = "";
            this.m_dtcTime.Width = 70;
            // 
            // m_dtcTemperature
            // 
            this.m_dtcTemperature.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTemperature.HeaderText = "���¡�";
            this.m_dtcTemperature.m_BlnGobleSet = true;
            this.m_dtcTemperature.m_BlnUnderLineDST = false;
            this.m_dtcTemperature.MappingName = "Temperature";
            this.m_dtcTemperature.NullText = "";
            this.m_dtcTemperature.Width = 40;
            // 
            // m_dtcHeartRhythm
            // 
            this.m_dtcHeartRhythm.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcHeartRhythm.HeaderText = "����";
            this.m_dtcHeartRhythm.m_BlnGobleSet = true;
            this.m_dtcHeartRhythm.m_BlnUnderLineDST = false;
            this.m_dtcHeartRhythm.MappingName = "HeartRhythm";
            this.m_dtcHeartRhythm.NullText = "";
            this.m_dtcHeartRhythm.Width = 40;
            // 
            // m_dtcHeartFrequency
            // 
            this.m_dtcHeartFrequency.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcHeartFrequency.HeaderText = "���ɴ�/��";
            this.m_dtcHeartFrequency.m_BlnGobleSet = true;
            this.m_dtcHeartFrequency.m_BlnUnderLineDST = false;
            this.m_dtcHeartFrequency.MappingName = "HeartFrequency";
            this.m_dtcHeartFrequency.NullText = "";
            this.m_dtcHeartFrequency.Width = 40;
            // 
            // m_dtcPulse
            // 
            this.m_dtcPulse.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPulse.HeaderText = "������/��";
            this.m_dtcPulse.m_BlnGobleSet = true;
            this.m_dtcPulse.m_BlnUnderLineDST = false;
            this.m_dtcPulse.MappingName = "Pulse";
            this.m_dtcPulse.NullText = "";
            this.m_dtcPulse.Width = 40;
            // 
            // m_dtcBreath
            // 
            this.m_dtcBreath.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBreath.HeaderText = "������/��";
            this.m_dtcBreath.m_BlnGobleSet = true;
            this.m_dtcBreath.m_BlnUnderLineDST = false;
            this.m_dtcBreath.MappingName = "Breath";
            this.m_dtcBreath.NullText = "";
            this.m_dtcBreath.Width = 40;
            // 
            // m_dtcEchoLeft
            // 
            this.m_dtcEchoLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcEchoLeft.HeaderText = "ͫ�׷�����";
            this.m_dtcEchoLeft.m_BlnGobleSet = true;
            this.m_dtcEchoLeft.m_BlnUnderLineDST = false;
            this.m_dtcEchoLeft.MappingName = "EchoLeft";
            this.m_dtcEchoLeft.NullText = "";
            this.m_dtcEchoLeft.Width = 40;
            // 
            // m_dtcEchoRight
            // 
            this.m_dtcEchoRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcEchoRight.HeaderText = "ͫ�׷�����";
            this.m_dtcEchoRight.m_BlnGobleSet = true;
            this.m_dtcEchoRight.m_BlnUnderLineDST = false;
            this.m_dtcEchoRight.MappingName = "EchoRight";
            this.m_dtcEchoRight.NullText = "";
            this.m_dtcEchoRight.Width = 40;
            // 
            // m_dtcPupilLeft
            // 
            this.m_dtcPupilLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilLeft.HeaderText = "ͫ�״�С��(mm)";
            this.m_dtcPupilLeft.m_BlnGobleSet = true;
            this.m_dtcPupilLeft.m_BlnUnderLineDST = false;
            this.m_dtcPupilLeft.MappingName = "PupilLeft";
            this.m_dtcPupilLeft.NullText = "";
            this.m_dtcPupilLeft.Width = 40;
            // 
            // m_dtcPupilRight
            // 
            this.m_dtcPupilRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilRight.HeaderText = "ͫ�״�С��(mm)";
            this.m_dtcPupilRight.m_BlnGobleSet = true;
            this.m_dtcPupilRight.m_BlnUnderLineDST = false;
            this.m_dtcPupilRight.MappingName = "PupilRight";
            this.m_dtcPupilRight.NullText = "";
            this.m_dtcPupilRight.Width = 40;
            // 
            // m_dtcBloodOxygenSaturation
            // 
            this.m_dtcBloodOxygenSaturation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodOxygenSaturation.HeaderText = "Ѫ�����Ͷ�%";
            this.m_dtcBloodOxygenSaturation.m_BlnGobleSet = true;
            this.m_dtcBloodOxygenSaturation.m_BlnUnderLineDST = false;
            this.m_dtcBloodOxygenSaturation.MappingName = "BloodOxygenSaturation";
            this.m_dtcBloodOxygenSaturation.NullText = "";
            this.m_dtcBloodOxygenSaturation.Width = 40;
            // 
            // m_dtcBedsideBloodSugar
            // 
            this.m_dtcBedsideBloodSugar.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBedsideBloodSugar.HeaderText = "����Ѫ�Ǻ���mmol/L";
            this.m_dtcBedsideBloodSugar.m_BlnGobleSet = true;
            this.m_dtcBedsideBloodSugar.m_BlnUnderLineDST = false;
            this.m_dtcBedsideBloodSugar.MappingName = "BedsideBloodSugar";
            this.m_dtcBedsideBloodSugar.NullText = "";
            this.m_dtcBedsideBloodSugar.Width = 40;
            // 
            // m_dtcBloodPressureS
            // 
            this.m_dtcBloodPressureS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureS.HeaderText = "����ѹmmHg";
            this.m_dtcBloodPressureS.m_BlnGobleSet = true;
            this.m_dtcBloodPressureS.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureS.MappingName = "BloodPressureS";
            this.m_dtcBloodPressureS.NullText = "";
            this.m_dtcBloodPressureS.Width = 40;
            // 
            // m_dtcBloodPressureA
            // 
            this.m_dtcBloodPressureA.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureA.HeaderText = "����ѹmmHg";
            this.m_dtcBloodPressureA.m_BlnGobleSet = true;
            this.m_dtcBloodPressureA.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureA.MappingName = "BloodPressureA";
            this.m_dtcBloodPressureA.NullText = "";
            this.m_dtcBloodPressureA.Width = 40;
            // 
            // m_dtcInI
            // 
            this.m_dtcInI.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInI.HeaderText = "������Һ��(ml)";
            this.m_dtcInI.m_BlnGobleSet = true;
            this.m_dtcInI.m_BlnUnderLineDST = false;
            this.m_dtcInI.MappingName = "InI";
            this.m_dtcInI.NullText = "";
            this.m_dtcInI.Width = 40;
            // 
            // m_dtcInD
            // 
            this.m_dtcInD.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInD.HeaderText = "�����ʳ��(ml)";
            this.m_dtcInD.m_BlnGobleSet = true;
            this.m_dtcInD.m_BlnUnderLineDST = false;
            this.m_dtcInD.MappingName = "InD";
            this.m_dtcInD.NullText = "";
            this.m_dtcInD.Width = 40;
            // 
            // m_dtcOutE
            // 
            this.m_dtcOutE.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOutE.HeaderText = "�ų�������(ml)";
            this.m_dtcOutE.m_BlnGobleSet = true;
            this.m_dtcOutE.m_BlnUnderLineDST = false;
            this.m_dtcOutE.MappingName = "OutE";
            this.m_dtcOutE.NullText = "";
            this.m_dtcOutE.Width = 40;
            // 
            // m_dtcOutU
            // 
            this.m_dtcOutU.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOutU.HeaderText = "�ų�����(ml)";
            this.m_dtcOutU.m_BlnGobleSet = true;
            this.m_dtcOutU.m_BlnUnderLineDST = false;
            this.m_dtcOutU.MappingName = "OutU";
            this.m_dtcOutU.NullText = "";
            this.m_dtcOutU.Width = 40;
            // 
            // m_dtcOutS
            // 
            this.m_dtcOutS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOutS.HeaderText = "�ų������(ml)";
            this.m_dtcOutS.m_BlnGobleSet = true;
            this.m_dtcOutS.m_BlnUnderLineDST = false;
            this.m_dtcOutS.MappingName = "OutS";
            this.m_dtcOutS.NullText = "";
            this.m_dtcOutS.Width = 40;
            // 
            // m_dtcOutV
            // 
            this.m_dtcOutV.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOutV.HeaderText = "�ų�Ż����(ml)";
            this.m_dtcOutV.m_BlnGobleSet = true;
            this.m_dtcOutV.m_BlnUnderLineDST = false;
            this.m_dtcOutV.MappingName = "OutV";
            this.m_dtcOutV.NullText = "";
            this.m_dtcOutV.Width = 40;
            // 
            // m_dtcSign
            // 
            this.m_dtcSign.Format = "";
            this.m_dtcSign.FormatInfo = null;
            this.m_dtcSign.HeaderText = "ǩ��";
            this.m_dtcSign.MappingName = "Sign";
            this.m_dtcSign.NullText = "";
            this.m_dtcSign.Width = 70;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(20, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 508);
            this.panel1.TabIndex = 10000004;
            // 
            // frmWatchItemTrack
            // 
            this.AccessibleDescription = "�۲���Ŀ��¼��";
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(792, 673);
            this.Controls.Add(this.panel1);
            this.Name = "frmWatchItemTrack";
            this.Text = "�۲���Ŀ��¼��";
            this.Load += new System.EventHandler(this.frmWatchItemTrack_Load);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region ����
		protected override string m_StrCurrentOpenDate
		{
			get
			{
				if(m_strCurrentOpenDate=="")
				{
					clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
					return "";
				}
				return m_strCurrentOpenDate;
			}
		}

		protected override enmApproveType m_EnmAppType
		{
			get{return enmApproveType.Nurses;}
		}

		protected override string m_StrRecorder_ID
		{
			get
			{
				return m_strCreateUserID;
			}
		}
		#endregion ����

		private void frmWatchItemTrack_Load(object sender, System.EventArgs e)
		{
			m_dtmPreRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.Focus();
		}

		/// <summary>
		/// ��ʼ���������DataTable��
		/// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		/// </summary>
		/// <param name="p_dtbRecordTable"></param>
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{
			#region Add Column

			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("CreateDate");
		
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);
		
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  
		
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate");        

			//��������ַ���
			p_dtbRecordTable.Columns.Add("Date");
		
			//���ʱ���ַ���
			p_dtbRecordTable.Columns.Add("Time");
		
			//��������ַ���
			p_dtbRecordTable.Columns.Add("Temperature",typeof(clsDSTRichTextBoxValue));
		
			//��������ַ���
			p_dtbRecordTable.Columns.Add("HeartRhythm",typeof(clsDSTRichTextBoxValue));
		
			//��������ַ���
			p_dtbRecordTable.Columns.Add("HeartFrequency",typeof(clsDSTRichTextBoxValue));
		
			//��������ַ���
			p_dtbRecordTable.Columns.Add("Pulse",typeof(clsDSTRichTextBoxValue));

			//��ź����ַ���
			p_dtbRecordTable.Columns.Add("Breath",typeof(clsDSTRichTextBoxValue));

			//�������ѹ�����ַ���
			p_dtbRecordTable.Columns.Add("BloodPressureS",typeof(clsDSTRichTextBoxValue));

			//�������ѹ�����ַ���
			p_dtbRecordTable.Columns.Add("BloodPressureA",typeof(clsDSTRichTextBoxValue));
		
			//���ͫ�״�С���ַ���
			p_dtbRecordTable.Columns.Add("PupilLeft",typeof(clsDSTRichTextBoxValue));
		
			//���ͫ�״�С���ַ���
			p_dtbRecordTable.Columns.Add("PupilRight",typeof(clsDSTRichTextBoxValue));
		
			//���ͫ�׷������ַ���
			p_dtbRecordTable.Columns.Add("EchoLeft",typeof(clsDSTRichTextBoxValue));
		
			//���ͫ�׷������ַ���
			p_dtbRecordTable.Columns.Add("EchoRight",typeof(clsDSTRichTextBoxValue));
		
			//���Ѫ�����Ͷ��ַ���
			p_dtbRecordTable.Columns.Add("BloodOxygenSaturation",typeof(clsDSTRichTextBoxValue));
		
			//��Ŵ���Ѫ���ַ���
			p_dtbRecordTable.Columns.Add("BedsideBloodSugar",typeof(clsDSTRichTextBoxValue));
		
			//�������Һ���ַ���
			p_dtbRecordTable.Columns.Add("InI",typeof(clsDSTRichTextBoxValue));
		
			//��Ž�ʳ���ַ���
			p_dtbRecordTable.Columns.Add("InD",typeof(clsDSTRichTextBoxValue));
		
			//����������ַ���
			p_dtbRecordTable.Columns.Add("OutE",typeof(clsDSTRichTextBoxValue)); 
		
			//��������ַ���
			p_dtbRecordTable.Columns.Add("OutU",typeof(clsDSTRichTextBoxValue));   
		
			//��Ŵ���ַ���
			p_dtbRecordTable.Columns.Add("OutS",typeof(clsDSTRichTextBoxValue)); 
		
			//���Ż�����ַ���
			p_dtbRecordTable.Columns.Add("OutV",typeof(clsDSTRichTextBoxValue)); 

			//���ǩ��
            p_dtbRecordTable.Columns.Add("Sign");

            p_dtbRecordTable.Columns.Add("CreateUserID"); 

			#endregion

			#region Set Header
			this.m_dtcDate.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
			this.m_dtcTime.HeaderText = "  ʱ\r\n\r\n\r\n\r\n  ��";
			this.m_dtcTemperature.HeaderText = "��\r\n\r\n��\r\n\r\n��";
			this.m_dtcHeartRhythm.HeaderText = "��\r\n\r\n\r\n\r\n��";
			this.m_dtcHeartFrequency.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcPulse.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcBreath.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcEchoLeft.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";
			this.m_dtcEchoRight.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";
			this.m_dtcPupilLeft.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n��\r\n(mm)";
			this.m_dtcPupilRight.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n��\r\n(mm)";
			this.m_dtcBloodOxygenSaturation.HeaderText = "Ѫ\r\n��\r\n��\r\n��\r\n��\r\n%";
			this.m_dtcBedsideBloodSugar.HeaderText = "��\r\n��\r\nѪ\r\n��\r\nmmol\r\n/L";
			this.m_dtcBloodPressureS.HeaderText = "��\r\n\r\n��\r\n\r\nѹ\r\nmmHg";
			this.m_dtcBloodPressureA.HeaderText = "��\r\n\r\n��\r\n\r\nѹ\r\nmmHg";
			this.m_dtcInD.HeaderText = "��\r\n��\r\n��\r\nҺ\r\n��\r\n(ml)";
			this.m_dtcInI.HeaderText = "��\r\n��\r\n��\r\nʳ\r\n��\r\n(ml)";
			this.m_dtcOutE.HeaderText = "��\r\n��\r\n��\r\n��\r\n��\r\n(ml)";
			this.m_dtcOutU.HeaderText = "��\r\n��\r\n��\r\n��\r\n(ml)";
			this.m_dtcOutS.HeaderText = "��\r\n��\r\n��\r\n��\r\n��\r\n(ml)";
			this.m_dtcOutV.HeaderText = "��\r\n��\r\nŻ\r\n��\r\n��\r\n(ml)";
			this.m_dtcSign.HeaderText = "  ǩ\r\n\r\n\r\n\r\n  ��";

			m_mthSetControl(m_dtcDate);
			m_mthSetControl(m_dtcTime);
			m_mthSetControl(m_dtcTemperature);
			m_mthSetControl(m_dtcHeartRhythm);
			m_mthSetControl(m_dtcHeartFrequency);
			m_mthSetControl(m_dtcPulse);
			m_mthSetControl(m_dtcBreath);
			m_mthSetControl(m_dtcEchoLeft);
			m_mthSetControl(m_dtcEchoRight);
			m_mthSetControl(m_dtcPupilLeft);
			m_mthSetControl(m_dtcPupilRight);
			m_mthSetControl(m_dtcBloodOxygenSaturation);
			m_mthSetControl(m_dtcBedsideBloodSugar);
			m_mthSetControl(m_dtcBloodPressureS);
			m_mthSetControl(m_dtcBloodPressureA);
			m_mthSetControl(m_dtcOutE);
			m_mthSetControl(m_dtcOutU);
			m_mthSetControl(m_dtcOutS);
			m_mthSetControl(m_dtcOutV);
			m_mthSetControl(m_dtcSign);
			m_mthSetControl(m_dtcInI);
			m_mthSetControl(m_dtcInD);



			m_dtcDate.ReadOnly = true;
			m_dtcTime.ReadOnly = true;
			m_dtcTemperature.m_RtbBase.m_BlnReadOnly = true;
			m_dtcHeartRhythm.m_RtbBase.m_BlnReadOnly = true;
			m_dtcHeartFrequency.m_RtbBase.m_BlnReadOnly = true;
			m_dtcPulse.m_RtbBase.m_BlnReadOnly = true;
			m_dtcBreath.m_RtbBase.m_BlnReadOnly = true;
			m_dtcEchoLeft.m_RtbBase.m_BlnReadOnly = true;
			m_dtcEchoRight.m_RtbBase.m_BlnReadOnly = true;
			m_dtcPupilLeft.m_RtbBase.m_BlnReadOnly = true;
			m_dtcPupilRight.m_RtbBase.m_BlnReadOnly = true;
			m_dtcBloodOxygenSaturation.m_RtbBase.m_BlnReadOnly = true;
			m_dtcBedsideBloodSugar.m_RtbBase.m_BlnReadOnly = true;
			m_dtcBloodPressureS.m_RtbBase.m_BlnReadOnly = true;
			m_dtcBloodPressureA.m_RtbBase.m_BlnReadOnly = true;
			m_dtcOutE.m_RtbBase.m_BlnReadOnly = true;
			m_dtcOutU.m_RtbBase.m_BlnReadOnly = true;
			m_dtcOutS.m_RtbBase.m_BlnReadOnly = true;
			m_dtcOutV.m_RtbBase.m_BlnReadOnly = true;
			m_dtcInI.m_RtbBase.m_BlnReadOnly = true;
			m_dtcInD.m_RtbBase.m_BlnReadOnly = true;
			m_dtcSign.ReadOnly = true;

			#endregion
		}


		/// <summary>
		///  ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{			
			m_dtmPreRecordDate = DateTime.MinValue;
		}

		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}



		/// <summary>
		///  ��ȡ��ӵ�DataTable������
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			clsWatchItemDataInfo objDataInfo;
			object [] objData;
            ArrayList objReturnData = new ArrayList();
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.WatchItem)
			{
				return m_objGetPerDateSummaryRecordsValueArr(p_objTransDataInfo);
			}

			#region ��ȡ��ӵ�DataTable������

			if(p_objTransDataInfo.GetType().Name.ToString() == "clsTransDataInfo")
			{
				m_dtmPreRecordDate = DateTime.MinValue;
				string m_strInPatientID = p_objTransDataInfo.m_objRecordContent.m_strInPatientID;
				string m_strInPatientDate = p_objTransDataInfo.m_objRecordContent.m_dtmInPatientDate.ToString("yyy-MM-dd HH:mm:ss");
				string m_strOpenDate = p_objTransDataInfo.m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
				long lngRes = ((clsWatchItemRecordsDomain)m_objRecordsDomain).m_lngGetRecordContent(m_strInPatientID,m_strInPatientDate,m_strOpenDate,out objDataInfo);
				if(lngRes <= 0)
				{
					return null;
				}
			}
			else
			{
				objDataInfo = (clsWatchItemDataInfo)p_objTransDataInfo;
				
			}
		
			#region ��ȡ�޸��޶�ʱ��
			int intCanModifyTime = 0;
			try
			{
				intCanModifyTime = int.Parse(m_strCanModifyTime);
			}
			catch
			{
				intCanModifyTime = 6;
			}
			#endregion

            for (int i = 0; i < objDataInfo.m_objTransDataArr.Length; i++)
			{
				objData = new object[27];   
			
				clsSubWatchItemRecordContent objCurrent = objDataInfo.m_objTransDataArr[i];
                clsSubWatchItemRecordContent objNext = (i == objDataInfo.m_objTransDataArr.Length - 1) ? null : objDataInfo.m_objTransDataArr[i + 1];

				//����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ�����ʾ
                if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
				{
					TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
					if((int)tsModify.TotalHours < intCanModifyTime)
						continue;
				}

				//����ֵ
                //if(i==0)
                //{
					//ֻ�ڵ�һ�м�¼����������Ϣ
					objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
					objData[1] = (int)enmRecordsType.WatchItem;//��ż�¼���͵�intֵ
					objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
					//						objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���
					objData[3] = objDataInfo.m_objTransDataArr[objDataInfo.m_objTransDataArr.Length-1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                //}   
				if(objCurrent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
				{
					objData[4] = objCurrent.m_dtmCreateDate.Date.ToString("yyyy-MM-dd 00:00:00") ;//�����ַ���
				}
				if(objCurrent.m_dtmCreateDate.ToString("HH:mm") != m_dtmPreRecordDate.ToString("HH:mm"))
				{
//					objData[5] = objCurrent.m_dtmCreateDate.ToString("HH:mm:ss");//ʱ���ַ���
					objData[5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
				}
				m_dtmPreRecordDate = objCurrent.m_dtmCreateDate;
				strText = objCurrent.m_strTemperature;
				strXml = "<root />";
				if(objNext != null && objNext.m_strTemperature != objCurrent.m_strTemperature)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strTemperature,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);//ֵΪ"0"�Ĳ���Ҫ��ʾ������					
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[6] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_strHeartRhythm;
				strXml = "<root />";
				if(objNext != null && objNext.m_strHeartRhythm != objCurrent.m_strHeartRhythm)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strHeartRhythm,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[7] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_strHeartFrequency;
				strXml = "<root />";
				if(objNext != null && objNext.m_strHeartFrequency != objCurrent.m_strHeartFrequency)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strHeartFrequency,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				
				objData[8] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_strPulse;
				strXml = "<root />";
				if(objNext != null && objNext.m_strPulse != objCurrent.m_strPulse)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strPulse,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[9] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_strBreath;
				strXml = "<root />";
				if(objNext != null && objNext.m_strBreath != objCurrent.m_strBreath)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strBreath,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[10] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_strBloodPressureS;
				strXml = "<root />";
				if(objNext != null && objNext.m_strBloodPressureS != objCurrent.m_strBloodPressureS)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureS,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[11] = objclsDSTRichTextBoxValue;//Ѫѹ��S:����ѹ������
			
				strText = objCurrent.m_strBloodPressureA;
				strXml = "<root />";
				if(objNext != null && objNext.m_strBloodPressureA != objCurrent.m_strBloodPressureA)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureA,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[12] = objclsDSTRichTextBoxValue;//���Ѫѹ��A:����ѹ������
			
				strText = objCurrent.m_strPupilLeft;
				strXml = "<root />";
				if(objNext != null && objNext.m_strPupilLeft != objCurrent.m_strPupilLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strPupilLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[13] = objclsDSTRichTextBoxValue;//ͫ�״�С������
			
				strText = objCurrent.m_strPupilRight;
				strXml = "<root />";
				if(objNext != null && objNext.m_strPupilRight != objCurrent.m_strPupilRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strPupilRight,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[14] = objclsDSTRichTextBoxValue;//ͫ�״�С������
			
				strText = objCurrent.m_strEchoLeft;
				strXml = "<root />";
				if(objNext != null && objNext.m_strEchoLeft != objCurrent.m_strEchoLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strEchoLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[15] = objclsDSTRichTextBoxValue;//ͫ�׷���������
			
				strText = objCurrent.m_strEchoRight;
				strXml = "<root />";
				if(objNext != null && objNext.m_strEchoRight != objCurrent.m_strEchoRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strEchoRight,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[16] = objclsDSTRichTextBoxValue;//ͫ�׷���������
			
				strText = objCurrent.m_strBloodOxygenSaturation;
				strXml = "<root />";
				if(objNext != null && objNext.m_strBloodOxygenSaturation != objCurrent.m_strBloodOxygenSaturation)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strBloodOxygenSaturation,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[17] = objclsDSTRichTextBoxValue;//Ѫ�����Ͷ�����
			
				strText = objCurrent.m_strBedsideBloodSugar;
				strXml = "<root />";
				if(objNext != null && objNext.m_strBedsideBloodSugar != objCurrent.m_strBedsideBloodSugar)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_strBedsideBloodSugar,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[18] = objclsDSTRichTextBoxValue;//����Ѫ������
			
				strText = objCurrent.m_intInD.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intInD.ToString() != objCurrent.m_intInD.ToString())/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intInD.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[19] = objclsDSTRichTextBoxValue;//����Һ������
			
				strText = objCurrent.m_intInI.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intInI.ToString() != objCurrent.m_intInI.ToString())/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intInI.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[20] = objclsDSTRichTextBoxValue;//��ʳ������
			
				strText = objCurrent.m_intOutE.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intOutE.ToString() != objCurrent.m_intOutE.ToString())/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intOutE.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[21] = objclsDSTRichTextBoxValue;//����������
			
				strText = objCurrent.m_intOutU.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intOutU.ToString() != objCurrent.m_intOutU.ToString())/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intOutU.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[22] = objclsDSTRichTextBoxValue;//��������
			
				strText = objCurrent.m_intOutS.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intOutS.ToString() != objCurrent.m_intOutS.ToString())/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intOutS.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
				objData[23] = objclsDSTRichTextBoxValue;//�������
			
				strText = objCurrent.m_intOutV.ToString();
				strXml = "<root />";
				if(objNext != null && objNext.m_intOutV != objCurrent.m_intOutV)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
				{
					strXml = m_strGetDSTTextXML(objCurrent.m_intOutV.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
				}
				objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
				objData[24] = objclsDSTRichTextBoxValue;//Ż��������    
			
				objData[25] = objCurrent.m_strModifyUserName;//ǩ��
                objData[26] = objCurrent.m_strCreateUserID;
                objReturnData.Add(objData);
				#region DSL��� 
				m_strCurrentOpenDate=objCurrent.m_dtmCreateDate.ToString();
				m_strCreateUserID=objCurrent.m_strModifyUserID;
				#endregion DSL���
			}
            object[][] m_objRe = new object[objReturnData.Count][];

            for (int m = 0; m < objReturnData.Count; m++)
                m_objRe[m] = (object[])objReturnData[m];
            return m_objRe;

			#endregion
		}

		/// <summary>
		/// ��ȡ��ӵ�DataTable��ͳ������
		/// �ṩ�۲���Ŀ��¼��ʹ��
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
		private object[][] m_objGetPerDateSummaryRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			#region  ��ȡ��ӵ�DataTable��ͳ�����ݣ�����ͳ�ƣ�
			clsWatchItemDataInfo objDataInfo;
			object [][] objData;
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			objDataInfo = (clsWatchItemDataInfo)p_objTransDataInfo;
			//û��ͳ������ʱ����ʾͳ�Ƶ�����
			if(!(objDataInfo.m_objItemSummary.m_intInD_Total != 0 || objDataInfo.m_objItemSummary.m_intInI_Total != 0 || objDataInfo.m_objItemSummary.m_intOutE_Total != 0
				|| objDataInfo.m_objItemSummary.m_intOutS_Total != 0 || objDataInfo.m_objItemSummary.m_intOutU_Total != 0 || objDataInfo.m_objItemSummary.m_intOutV_Total != 0
				|| objDataInfo.m_objItemSummary.m_intTotal_In != 0 || objDataInfo.m_objItemSummary.m_intTotal_Out != 0))
			{
				return null;
			}
			objData = new object[3][];
			objData[2] = new string[26];
			objData[0] = new object[26];   
			bool m_blnIfLastSummary = false;
			
			if(objDataInfo.m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)//�ж��Ƿ�������ͳ��
			{
				m_blnIfLastSummary = true;
			}

			if(m_blnIfLastSummary)//���������ͳ�ƽ�������ָı�
				strText = "�Ϲ�";
			else
				strText = "����";

			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);//ֵΪ"0"�Ĳ���Ҫ��ʾ������						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[0][16] = objclsDSTRichTextBoxValue;//ͫ�׷���������

			strText = "����";
			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[0][17] = objclsDSTRichTextBoxValue;//Ѫ�����Ͷ�����

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[0][18] = objclsDSTRichTextBoxValue;//����Ѫ������
		
			strText = objDataInfo.m_objItemSummary.m_intInD_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[0][19] = objclsDSTRichTextBoxValue;//����Һ������
		
			strText = objDataInfo.m_objItemSummary.m_intInI_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText = (strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objData[0][20] = objclsDSTRichTextBoxValue;//��ʳ������
		
			strText = objDataInfo.m_objItemSummary.m_intOutE_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objData[0][21] = objclsDSTRichTextBoxValue;//����������
		
			strText = objDataInfo.m_objItemSummary.m_intOutU_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objData[0][22] = objclsDSTRichTextBoxValue;//��������
		
			strText = objDataInfo.m_objItemSummary.m_intOutS_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objData[0][23] = objclsDSTRichTextBoxValue;//�������
		
			strText = objDataInfo.m_objItemSummary.m_intOutV_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[0][24] = objclsDSTRichTextBoxValue;//Ż��������    

			objData[1] = new object[26];

			if(m_blnIfLastSummary)
				strText = "�Ϲ�";
			else
				strText = "����";

			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[1][16] = objclsDSTRichTextBoxValue;//ͫ�׷���������

			strText = "����";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[1][17] = objclsDSTRichTextBoxValue;//Ѫ�����Ͷ�����

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[1][18] = objclsDSTRichTextBoxValue;//����Ѫ������

			strText = objDataInfo.m_objItemSummary.m_intTotal_In.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objData[1][19] = objclsDSTRichTextBoxValue;//����Һ������

			strText = objDataInfo.m_objItemSummary.m_intTotal_Out.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objData[1][21] = objclsDSTRichTextBoxValue;//����������
			
			if(m_blnIfLastSummary==true)
			{
				object [][] objDataReturn=new object[2][];;
				objDataReturn[0]=objData[0];
				objDataReturn[1]=objData[1];
				return objDataReturn;
			}	
			
			return objData;			

			#endregion
		}

		#region No Use
//		/// <summary>
//		/// ��ȡ��ӵ�DataTable��ͳ�����ݣ�ȫ��ͳ�ƣ�
//		/// �ṩ�۲���Ŀ��¼��ʹ��
//		/// </summary>
//		/// <param name="p_objTransDataInfoArr"></param>
//		/// <returns></returns>
//		protected override object[][] m_objGetSummaryRecordsValueArr(clsTransDataInfo[] objTansDataInfoArr)
//		{
//				//���Ӵ�������
//			if(objTansDataInfoArr.Length <=0)
//				return null;
//
//			clsWatchItemSummary m_objSummary = new clsWatchItemSummary();
//			clsWatchItemDataInfo objDataInfo;
//			for(int i1=0;i1<objTansDataInfoArr.Length;i1++)
//			{
//				if(objTansDataInfoArr[i1].m_intFlag != (int)enmRecordsType.WatchItem)
//				{
//					objDataInfo = (clsWatchItemDataInfo)objTansDataInfoArr[i1];
//					m_objSummary.m_intInD_Total+=objDataInfo.m_objItemSummary.m_intInD_Total;
//					m_objSummary.m_intInI_Total+=objDataInfo.m_objItemSummary.m_intInI_Total;
//					m_objSummary.m_intOutE_Total+=objDataInfo.m_objItemSummary.m_intOutE_Total;
//					m_objSummary.m_intOutS_Total+=objDataInfo.m_objItemSummary.m_intOutS_Total;
//					m_objSummary.m_intOutV_Total+=objDataInfo.m_objItemSummary.m_intOutV_Total;
//					m_objSummary.m_intOutU_Total+=objDataInfo.m_objItemSummary.m_intOutU_Total;
//					m_objSummary.m_intTotal_In+=objDataInfo.m_objItemSummary.m_intTotal_In;
//					m_objSummary.m_intTotal_Out+=objDataInfo.m_objItemSummary.m_intTotal_Out;
//				}
//			}
//			#region  ��ȡ��ӵ�DataTable��ͳ�����ݣ�����ͳ�ƣ�
//			object [][] objData;
//			string strText,strXml;
//			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
//
//			objData = new object[3][];
//			objData[0] = new object[26];   
//		
//			objData[1] = new object[26];  
//			strText = "�Ϲ�";
//			strXml = "<root />";
//
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[1][16] = objclsDSTRichTextBoxValue;//ͫ�׷���������
//
//			strText = "����";
//			strXml = "<root />";
//
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[1][17] = objclsDSTRichTextBoxValue;//Ѫ�����Ͷ�����
//
//			strText = "�ܼ�:";
//			strXml = "<root />";
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[1][18] = objclsDSTRichTextBoxValue;//����Ѫ������
//		
//			strText = m_objSummary.m_intInD_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[1][19] = objclsDSTRichTextBoxValue;//����Һ������
//		
//			strText = m_objSummary.m_intInI_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//			objData[1][20] = objclsDSTRichTextBoxValue;//��ʳ������
//		
//			strText = m_objSummary.m_intOutE_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//			objData[1][21] = objclsDSTRichTextBoxValue;//����������
//		
//			strText = m_objSummary.m_intOutU_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//			objData[1][22] = objclsDSTRichTextBoxValue;//��������
//		
//			strText = m_objSummary.m_intOutS_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//			objData[1][23] = objclsDSTRichTextBoxValue;//�������
//		
//			strText = m_objSummary.m_intOutV_Total.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[1][24] = objclsDSTRichTextBoxValue;//Ż��������    
//
//			objData[2] = new object[26];
//
//			strText = "�Ϲ�";
//			strXml = "<root />";
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[2][16] = objclsDSTRichTextBoxValue;//ͫ�׷���������
//
//			strText = "����";
//			strXml = "<root />";
//
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[2][17] = objclsDSTRichTextBoxValue;//Ѫ�����Ͷ�����
//
//			strText = "�ܼ�:";
//			strXml = "<root />";
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[2][18] = objclsDSTRichTextBoxValue;//����Ѫ������
//
//			strText = m_objSummary.m_intTotal_In.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
//			objData[2][19] = objclsDSTRichTextBoxValue;//����Һ������
//
//			strText = m_objSummary.m_intTotal_Out.ToString();
//			strXml = "<root />";
//			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
//			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
//			objclsDSTRichTextBoxValue.m_strText=strText;						
//			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
//			objData[2][21] = objclsDSTRichTextBoxValue;//����������
//			return objData;
//
//			#endregion
//		}

		#endregion

		/// <summary>
		///  ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsSubWatchItemRecordContent
			clsSubWatchItemRecordContent objContent = new clsSubWatchItemRecordContent();
		
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = p_objDataArr[26].ToString();    
		
			return objContent;
		}

		/// <summary>
		///  ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.WatchItem);
		}

		protected override infPrintRecord m_objGetPrintTool()
		{
			return new clsWatchItemTrackPrintTool();
		}

		private void mniAppend_Click(object sender, System.EventArgs e)
		{
				enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			m_mthAddNewRecord((int)enmDiseaseTrackType.WatchItem);
		}
		/// <summary>
		///  ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <returns></returns>
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			///���ع۲���Ŀ��¼�����Ӧ�ı༭����
			return new frmSubWatchItemRecord();
		}

		/// <summary>
		/// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		/// <param name="p_intRecordType"></param>
		protected override void m_mthAddNewRecord(int p_intRecordType)
		{
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);    
		
			//��ӿ���
			frmAddNewForm.m_mthSetDiseaseTrackInfoForAddNew(m_objCurrentPatient);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,false);
		}

		/// <summary>
		/// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		protected override void m_mthModifyRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
		}

        protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        {
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession,0);
        }

        /// <summary>
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
            DateTime p_dtmCreateRecordTime)
        {
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
        }

		#region ��ӡ
		private clsWatchItemDataInfo[] m_objPrintDataArr;
		/// <summary>
		/// ���ô�ӡ���ݡ�
		/// </summary>
		/// <param name="p_objTransDataArr"></param>
		/// <param name="p_dtmFirstPrintDate"></param>
		protected override void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
			DateTime[] p_dtmFirstPrintDate)
		{
			if(p_objTransDataArr==null || p_dtmFirstPrintDate==null ||p_objTransDataArr.Length !=p_dtmFirstPrintDate.Length)
			{
				clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
				return;
			}
			ArrayList m_arlTemp = new ArrayList();
			for(int i1=0;i1<p_objTransDataArr.Length;i1++)
			{
				m_arlTemp.Add(p_objTransDataArr[i1]);
			}
			m_objPrintDataArr = (clsWatchItemDataInfo[])m_arlTemp.ToArray(typeof(clsWatchItemDataInfo));
		}

		// ��ʼ����ӡ����
		protected override void m_mthInitPrintTool()
		{
			#region �йش�ӡ��ʼ��
		
			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 12f);
			m_fotSmallFont = new Font("SimSun",10.5f);
			m_fotTinyFont=new Font("SimSun",8f);
		
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
	
			m_objPageSetting = new clsPrintPageSettingForRecord();
			//			m_intCurrentRecord=0;
			//			m_intNowPage=1;				
			#endregion	
		}

		// �ͷŴ�ӡ����
		protected override void m_mthDisposePrintTools()
		{
			m_objPageSetting = null;
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont.Dispose();
			m_fotTinyFont.Dispose();
			m_GridPen.Dispose();
			m_slbBrush.Dispose();
		}

		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		protected override void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{

		}

		// ��ӡҳ
		protected override void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{			
			try
			{
				p_objPrintPageArg.HasMorePages =false;
				m_mthPrintTitleInfo(p_objPrintPageArg);
				m_mthPrintRectangleInfo(p_objPrintPageArg);	
				m_mthPrintHeaderInfo(p_objPrintPageArg);

				if(m_objCurrentPatient==null || m_objPrintDataArr==null || m_objPrintDataArr.Length==0)
					return;
				while(m_intCurrentRecord < m_objPrintDataArr.Length)
				{				
					if(m_intCurrentRecord==0)
						m_intSetPrintOneValueRows(p_objPrintPageArg,ref m_intPosY);
					m_blnBeginPrintNewRecord = m_blnPrintOneValue(p_objPrintPageArg, m_intPosY);	
				
					//б��
					p_objPrintPageArg.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8 ,
						m_intPosY-intTempDeltaY,
						(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,
						m_intPosY);			

					if(m_blnBeginPrintNewRecord)
					{
						m_intCurrentRecord++;
					
						m_mthPrintOneHorizontalLine(p_objPrintPageArg,m_intPosY);

						int intMaxRows=m_intSetPrintOneValueRows(p_objPrintPageArg,ref m_intPosY);
						if(m_intPosY + intMaxRows*intTempDeltaY >= 1100	&& m_intCurrentRecord < m_objPrintDataArr.Length)
						{
							p_objPrintPageArg.HasMorePages = true;				

							//Print VLine
							m_mthPrintVLines(p_objPrintPageArg,m_intPosY);
							m_mthPrintOneHorizontalLine(p_objPrintPageArg,m_intPosY);

							//ҳ��//////////////////////////////////////////////////////////////
							p_objPrintPageArg.Graphics.DrawString("����"+m_intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
								m_intPosY+(int)enmRecordRectangleInfo.VOffSet );
           

							m_intPosY = (int)enmRecordRectangleInfo.TopY+150;
							m_intNowPage++;
							return;
					
						}
					}					
				
				}
				m_mthPrintVLines(p_objPrintPageArg,m_intPosY);
				//ҳ��//////////////////////////////////////////////////////////////
				p_objPrintPageArg.Graphics.DrawString("����"+m_intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
					m_intPosY+(int)enmRecordRectangleInfo.VOffSet );
			
				#region ��ӡ��ϣ�ReSet(��λ)����
				if(m_intCurrentRecord==m_objPrintDataArr.Length)
				{	
					m_intPosY = (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep;
					m_intCurrentRecord=0;//��ǰ��¼����Ÿ�λ���Ա���һ�δ�ӡ����
					m_blnBeginPrintNewRecord=true;//��λ
					m_intNowPage=1;//��λ						
				}
				#endregion				
			}
			catch(Exception err)
			{
				clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

			}
		}

		// ��ӡ����ʱ�Ĳ���
		protected override void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
		
		}



		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			clsEveryRecordPageInfo objEveryRecordPageInfo=new clsEveryRecordPageInfo();
			//************************************************
			objEveryRecordPageInfo.strAge =m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge.ToString() : "";
			objEveryRecordPageInfo.strPatientName=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			objEveryRecordPageInfo.strDeptName= m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName : "";			
			objEveryRecordPageInfo.strBedNo =m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			objEveryRecordPageInfo.strAreaName=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName:"";
			objEveryRecordPageInfo.strSex=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_StrSex:"";
			objEveryRecordPageInfo.strInPatientID=m_objCurrentPatient!=null? m_objCurrentPatient.m_StrInPatientID:"";
			objEveryRecordPageInfo.strPrintDate=m_objCurrentPatient!=null? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		
			
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("�� �� �� Ŀ �� ¼ ��",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strDeptName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,new PointF(430f,150f));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAreaName ,m_fotSmallFont,m_slbBrush,new PointF(480f,150f));

			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
						
		}
		#endregion

		#region ����ͷ����
		/// <summary>
		///  ����ͷ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			int m_intHeaderRowStep=50;
			
			//�����Ӻ���
			for(int i1=0;i1<4 /*(int)enmRecordRectangleInfo.RowLinesNum*/ ;i1++)
			{
				if(i1 !=1 && i1 !=2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1,
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1);
				else if(i1==1)
				{
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark8,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark12,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5);
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark14,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark20,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5);
				}
				else //if(i1==2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark8,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark12,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1);
			}
			
			#region ����������
			int intHeight=3*m_intHeaderRowStep;
			//���������
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY+intHeight);
			#endregion				
			
		}

						
		#endregion		

		#region ���������Ŀ
		private int m_intHeaderRowStep=50;
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{			
		
			e.Graphics.DrawString("����",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+15,
				(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
		     
			e.Graphics.DrawString("ʱ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);

			//���� C			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY-10+2*m_intHeaderRowStep+5);
			e.Graphics.DrawString("C",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+9, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+1+5);

			// ����			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			
			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//Ѫѹ(mmHg)
			e.Graphics.DrawString("Ѫѹ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+8, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-15);
			e.Graphics.DrawString("mmHg",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+8, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-10);
	
			e.Graphics.DrawString(" ͫ ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+31, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��С",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+10, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5-5);
			e.Graphics.DrawString("(mm)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+10, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5+15);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark9+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+18, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark11+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			//Ѫ�����Ͷ�(%)
			e.Graphics.DrawString("Ѫ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*1/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*2/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*3/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*4/6);
			e.Graphics.DrawString("(%)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*5/6);

			//����Ѫ��(mmol/L)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*1/6);
			e.Graphics.DrawString("Ѫ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*2/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*3/6);
			e.Graphics.DrawString("mmol",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*4/6);
			e.Graphics.DrawString("/L",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*5/6);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15-20, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("(ml)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15-20, (int)enmRecordRectangleInfo.TopY+25);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("Һ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("ʳ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);


			e.Graphics.DrawString("�ų�",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18-20, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("(ml)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18-20, (int)enmRecordRectangleInfo.TopY+25);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark17+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark17+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);

			e.Graphics.DrawString("Ż",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);


			e.Graphics.DrawString("ǩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark20+1,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
		
		
		}
		#endregion

		#region ��ӡ���еĴ�ֱ��
		/// <summary>
		/// ��ӡ���еĴ�ֱ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intPageBottomY"></param>
		private void m_mthPrintVLines(PrintPageEventArgs e,int p_intPageBottomY)
		{			
			#region ����������
			int intContentTopY=(int)enmRecordRectangleInfo.TopY+ 150;
			//���������
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,p_intPageBottomY);
			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,p_intPageBottomY);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,p_intPageBottomY);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,p_intPageBottomY);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,p_intPageBottomY);
			
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,p_intPageBottomY);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,p_intPageBottomY);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,intContentTopY,
				(int)enmRecordRectangleInfo.RightX,p_intPageBottomY);
			#endregion		
		}
		#endregion

		#region ��ӡһ��ˮƽ��
		/// <summary>
		/// ��ӡһ��ˮƽ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		private void m_mthPrintOneHorizontalLine(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
				p_intBottomY,
				(int)enmRecordRectangleInfo.RightX,
				p_intBottomY);			
		}
		#endregion

		#region ֻ��ӡһ��
		/// <summary>
		/// ֻ��ӡһ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		/// <returns></returns>
		private bool m_blnPrintOneValue(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			p_intBottomY +=(int)enmRecordRectangleInfo.VOffSet;
			#region ������¼�¼����ӡ����
			if(m_blnBeginPrintNewRecord==true) 
			{
				m_intNowRowInOneRecord=0;

				//��������
				string strCreateDate;
				string strCreateTime;
				string strCreateDateTime;
				
				if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
				{
					strCreateDate = "";
					strCreateTime = "";
					strCreateDateTime = "";
				}
				else
				{
					strCreateDateTime=m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
					try
					{
						strCreateDate=DateTime.Parse(strCreateDateTime).ToString("yyyy-M-d");
						strCreateTime=DateTime.Parse(strCreateDateTime).ToString("HH:mm");
					}
					catch
					{strCreateDate="����";strCreateTime="����";}	
				}
				//��ʼ��ӡһ���¼�¼/////////////////////////////////////////////////////////////////////
				e.Graphics.DrawString(strCreateDate,m_fotSmallFont ,m_slbBrush,
					(int)enmRecordRectangleInfo.LeftX, 
					p_intBottomY);	
				e.Graphics.DrawString(strCreateTime,m_fotSmallFont ,m_slbBrush,
					(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1+1, 
					p_intBottomY );	
			}
			#endregion			
			
			#region ���޸�˳���ӡ��ǰ��¼��ĳһ��	
			bool blnIsRecordFinish=m_blnPrintOneRowValue(m_strValueArr,m_intNowRowInOneRecord,e,p_intBottomY);
			
			#region ǩ���������޸ĵ���ǩ����
			string strSign;
			if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
				strSign = "";
			else
				strSign = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord].m_strModifyUserName;			
			//			clsEmployee objclsEmployee=new clsEmployee(m_objclsWatchItemRecordContent_AllArr[m_intCurrentRecord].m_objclsWatchItemRecordContentArr[m_intNowRowInOneRecord].m_strModifyUserID);
			//			if(objclsEmployee!=null)
			//				strSign=objclsEmployee.m_StrFirstName;			
			e.Graphics.DrawString(strSign ,m_fotSmallFont ,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20+1, 
				p_intBottomY);
			#endregion

			m_blnBeginPrintNewRecord=blnIsRecordFinish;//��ǰ��¼�Ƿ����					
			m_intNowRowInOneRecord++;
			#endregion

			m_intPosY += intTempDeltaY;
			return blnIsRecordFinish;			
		}

		#endregion
	
		#region Liyi
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private bool m_blnPrintOneRowValue(string [][] p_strValueArr,int p_intIndex,System.Drawing.Printing.PrintPageEventArgs e,int p_intPosY)
		{			
			string [] strValueArr = p_strValueArr[p_intIndex];

			if(p_strValueArr[0][12] == "�ܼ�:")
			{
				return m_blnPrintOneRowValueOfSummary(p_strValueArr,p_intIndex,e,p_intPosY);
			}

			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;

			int intTempColumn=0;//��ǰ����������ԣ�
			int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
			//����
			#region ��ӡһ�񣬣�������ȫ��ͬ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=1;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=2;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=3;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=4;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			
			bool blnIsLastModify=false;
			if( p_intIndex == p_strValueArr.Length-1 || (strValueArr[5] == p_strValueArr[p_intIndex+1][5] && strValueArr[6] == p_strValueArr[p_intIndex+1][6] && strValueArr[5] == p_strValueArr[p_strValueArr.Length-1][5] && strValueArr[6] == p_strValueArr[p_strValueArr.Length-1][6] ))
			{// ��������һ�У����ҵ�ǰԪ�� != ��һ�д�Ԫ��				
				blnIsLastModify=true;					
			}
			//Ѫѹ(����ѹ)
			if(strValueArr[5].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[5],m_fotSmallFont,Brushes.Black,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,p_intPosY-15);
				if( ! blnIsLastModify)
				{					
					rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7;
					rtfText.Y = p_intPosY-15;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[5].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[5],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					
				}
			}	
			
			//Ѫѹ(����ѹ)
			if(strValueArr[6].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[6],m_fotSmallFont,Brushes.Black,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7+30,p_intPosY);
				if( ! blnIsLastModify)
				{					
					rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7+30;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[6].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[6],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					
				}
			}

			intTempColumn=7;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
			//ͫ�״�С����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
			//ͫ�״�С���ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			//ͫ�׷��䣨��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
			//ͫ�׷��䣨�ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
			//Ѫ�����Ͷ�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
			//����Ѫ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
			//����Һ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��					

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
			//��ʳ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
			//������
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18;//��ǰ��X����
			//���
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19;//��ǰ��X����
			//Ż����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			return p_intIndex==p_strValueArr.Length-1;
		}
		#endregion


		#region Alex
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private bool m_blnPrintOneRowValueOfSummary(string [][] p_strValueArr,int p_intIndex,System.Drawing.Printing.PrintPageEventArgs e,int p_intPosY)
		{			
			string [] strValueArr = p_strValueArr[p_intIndex];

			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;

			int intTempColumn=0;//��ǰ����������ԣ�
			int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
			//����

			intTempColumn=1;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
			//����

			intTempColumn=2;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			//����

			intTempColumn=3;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			//����
			intTempColumn=4;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			//����

			intTempColumn=7;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
			//ͫ�״�С����

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
			//ͫ�״�С���ң�

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			//ͫ�׷��䣨��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
			//ͫ�׷��䣨�ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
			//Ѫ�����Ͷ�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
			//����Ѫ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
			//����Һ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��					

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
			//��ʳ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
			//������
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18;//��ǰ��X����
			//���
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19;//��ǰ��X����
			//Ż����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			return p_intIndex==p_strValueArr.Length-1;
		}
		#endregion

		#region ���õ�ǰҪ��ӡ��һ����¼����
		/// <summary>
		/// ���õ�ǰҪ��ӡ��һ����¼����
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		/// <returns></returns>
		private int m_intSetPrintOneValueRows(PrintPageEventArgs e,ref int p_intBottomY)
		{			
			if(m_objPrintDataArr==null || m_intCurrentRecord>= m_objPrintDataArr.Length)
				return 0;
//
//			if(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr==null || m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length==0)
//				return 0;

			if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag == (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr==null)
				return 0;
//			int intRowsOfOneRecord=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
//			string strModifyDate=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord-1].m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss") ;

			int intRowsOfOneRecord;
			string strModifyDate;
		
			try
			{
				#region ������¼�¼���ж��Ƿ����ۼ�
				int intLenth;
				if(m_blnBeginPrintNewRecord==true) 
				{									
					#region ��ǰ��¼���鸳ֵ
					if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
					{
						intLenth = 2;
						intRowsOfOneRecord = intLenth;
						strModifyDate = "";
						m_strValueArr=new string[intLenth][];
						m_strValueArr[0]=new string[19];
						m_strValueArr[0][0]="";
						m_strValueArr[0][1]="";
						m_strValueArr[0][2]="";
						m_strValueArr[0][3]="";
						m_strValueArr[0][4]="";
						m_strValueArr[0][5]="";
						m_strValueArr[0][6]="";
						m_strValueArr[0][7]="";
						m_strValueArr[0][8]="";
						m_strValueArr[0][9]="";
						if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)
							m_strValueArr[0][10]="�Ϲ�";
						else
							m_strValueArr[0][10]="����";
						m_strValueArr[0][11]="����";
						m_strValueArr[0][12]="�ܼ�:";
						m_strValueArr[0][13]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInI_Total.ToString();
						m_strValueArr[0][14]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInD_Total.ToString();
						m_strValueArr[0][15]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutE_Total.ToString();
						m_strValueArr[0][16]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutU_Total.ToString();
						m_strValueArr[0][17]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutS_Total.ToString();
						m_strValueArr[0][18]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutV_Total.ToString();
					
						m_strValueArr[1]=new string[19];
						m_strValueArr[1][0]="";
						m_strValueArr[1][1]="";
						m_strValueArr[1][2]="";
						m_strValueArr[1][3]="";
						m_strValueArr[1][4]="";
						m_strValueArr[1][5]="";
						m_strValueArr[1][6]="";
						m_strValueArr[1][7]="";
						m_strValueArr[1][8]="";
						m_strValueArr[1][9]="";
						if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)
							m_strValueArr[1][10]="�Ϲ�";
						else
							m_strValueArr[1][10]="����";
						m_strValueArr[1][11]="����";
						m_strValueArr[1][12]="�ܼ�:";
						m_strValueArr[1][13]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_In.ToString();
						m_strValueArr[1][14]="";
						m_strValueArr[1][15]=m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_Out.ToString();
						m_strValueArr[1][16]="";
						m_strValueArr[1][17]="";
						m_strValueArr[1][18]="";
						return intLenth;
					}
					else
					{
						intRowsOfOneRecord=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
						strModifyDate=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord-1].m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
						intLenth=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
						m_strValueArr=new string[intLenth][];
						for(int k1=0;k1<intLenth;k1++)
						{
							m_strValueArr[k1]=new string[19];
							m_strValueArr[k1][0]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTemperature;
							m_strValueArr[k1][1]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strHeartRhythm;
							m_strValueArr[k1][2]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strHeartFrequency;
							m_strValueArr[k1][3]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPulse;
							m_strValueArr[k1][4]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBreath;
							m_strValueArr[k1][5]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodPressureS;
							m_strValueArr[k1][6]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodPressureA;
							m_strValueArr[k1][7]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPupilLeft;
							m_strValueArr[k1][8]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPupilRight;
							m_strValueArr[k1][9]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strEchoLeft;
							m_strValueArr[k1][10]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strEchoRight;
							m_strValueArr[k1][11]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodOxygenSaturation;
							m_strValueArr[k1][12]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBedsideBloodSugar;
							m_strValueArr[k1][13]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInI.ToString();
							m_strValueArr[k1][14]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInD.ToString();
							m_strValueArr[k1][15]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutE.ToString();
							m_strValueArr[k1][16]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutU.ToString();
							m_strValueArr[k1][17]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutS.ToString();
							m_strValueArr[k1][18]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutV.ToString();
						}
						return intLenth;
					}
					#endregion
				}
				else 
					return 0;
				#endregion
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
				return 1;
			}			
		}
		#endregion
		#endregion

		/// <summary>
		/// ��ȡ��ǰ���˵���������
		/// </summary>
		/// <param name="p_dtmRecordDate">��¼����</param>
		/// <param name="p_intFormID">����ID</param>
		protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate,int p_intFormID)
		{			
			m_mthGetDeletedRecord(p_intFormID,p_dtmRecordDate);
		}

		protected override void m_mthGetDeletedRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{	
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDeletedDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,false);
		}

		protected override void m_mthStartPrint()
		{
			if(m_blnDirectPrint)
			{
				m_pdcPrintDocument.Print();
			}
			else
			{
				((clsWatchItemTrackPrintTool)objPrintTool).m_mthPrintPage();
				
			}
		}

	}
}

