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
	/// <summary>
	/// Σ�ػ����¼�������ʵ��,Jacky-2003-5-15
	/// </summary>
	public class frmIntensiveTendMain : iCare.frmRecordsBase
	{
		private System.Windows.Forms.PictureBox m_picExpand;
		private cltDataGridDSTRichTextBox m_dtcTemperature;
		private cltDataGridDSTRichTextBox m_dtcPulse;
		private cltDataGridDSTRichTextBox m_dtcBreath;
		private cltDataGridDSTRichTextBox m_dtcBloodPressureS;
		private cltDataGridDSTRichTextBox m_dtcBloodPressureA;
		private cltDataGridDSTRichTextBox m_dtcInID;
		private cltDataGridDSTRichTextBox m_dtcPupilLeft;
		private cltDataGridDSTRichTextBox m_dtcPupilRight;
		private cltDataGridDSTRichTextBox m_dtcEchoLeft;
		private cltDataGridDSTRichTextBox m_dtcEchoRight;
		private cltDataGridDSTRichTextBox m_dtcIn_Qty;
		private cltDataGridDSTRichTextBox m_dtcOutID;
		private cltDataGridDSTRichTextBox m_dtcOut_Qty;
		private System.Windows.Forms.DataGridTextBoxColumn clmSign;
		private cltDataGridDSTRichTextBox m_dtcContent;
		private System.Windows.Forms.DataGridTextBoxColumn clmCreateTime;
		private System.Windows.Forms.DataGridTextBoxColumn clmCreateDateofDay;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components = null;
		private string m_strCurrentOpenDate = "";
		private string m_strCreateUserID = "";
//		private cltDataGridDSTRichTextBox m_dtcTemperature;
//		private cltDataGridDSTRichTextBox m_dtcPulse;
//		private cltDataGridDSTRichTextBox m_dtcBreath;
//		private cltDataGridDSTRichTextBox m_dtcEchoLeft;
//		private cltDataGridDSTRichTextBox m_dtcEchoRight;
//		private cltDataGridDSTRichTextBox m_dtcPupilLeft;
//		private cltDataGridDSTRichTextBox m_dtcPupilRight;
//		private cltDataGridDSTRichTextBox m_dtcBloodPreasureS;
//		private cltDataGridDSTRichTextBox m_dtcIn_TypeID;
//		private cltDataGridDSTRichTextBox m_dtcIn_Quantity;
//		private cltDataGridDSTRichTextBox m_dtcOut_TypeID;
//		private cltDataGridDSTRichTextBox m_dtcOut_Quantity;
//		private cltDataGridDSTRichTextBox m_dtcContent;
//	

		public frmIntensiveTendMain()
		{
			try
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			
//				m_dtcInID.m_BlnUnderLineDST=true;
//				m_dtcIn_Qty.m_BlnUnderLineDST=true;
//				m_dtcOutID.m_BlnUnderLineDST=true;
//				m_dtcOut_Qty.m_BlnUnderLineDST=true;
				m_dtcInID.m_BlnGobleSet=false;
				m_dtcIn_Qty.m_BlnGobleSet=false;
				m_dtcOutID.m_BlnGobleSet=false;
				m_dtcOut_Qty.m_BlnGobleSet=false;
				// TODO: Add any initialization after the InitializeComponent call
				//m_dtbRecords.Columns.Add("clmContent",typeof(clsDSTRichTextBoxValue));
				//			clmContent.m_RtbBase.ScrollBars = RichTextBoxScrollBars.None;
				
				imgUserclose = new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "CLSDFOLD.ICO");
				imgUseropen= new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "OPENFOLD.ICO");
				
				m_blnIsExpand=true;
				this.m_picExpand.Image=imgUseropen;	
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region �йش�ӡ������
		/// <summary>
		/// ���д�ӡ������
		/// </summary>
		private clsPrintData[] m_objPrintDataArr;
		private class clsPrintData
		{
			public DateTime m_dtmCreateDate;			
			public string m_strContent;
			public string m_strContentXml;
			public string m_strSign;
			public string m_strSignXml;
			public DateTime m_dtmFirstPrintDate;
		}			

		/// <summary>
		/// ������Σ�ػ����¼�ģ���ӡ�����ĵ���
		/// </summary>		
		private com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext;
		/// <summary>
		/// ÿ����ʾ�ĺ��֣����̼�¼������ĸ������������Ŀ
		/// </summary>
		private class clsPrintLenth_IntensiveTendRecord
		{
			public int m_intPrintLenth_RecordContent;
			public int m_intPrintLenth_Temperature;
			public int m_intPrintLenth_Breath;
			public int m_intPrintLenth_Pulse;
			public int m_intPrintLenth_BloodPressure;			
			public int m_intPrintLenth_Pupil;	//ͫ�ף���С��		
			public int m_intPrintLenth_Echo;	//����		
			public int m_intPrintLenth_In;//����
			public int m_intPrintLenth_Out;	//�ų�		
		}
		private clsPrintLenth_IntensiveTendRecord m_objPrintLenth;
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
		private int intNowPage=1;
		/// <summary>
		/// ��ǰ��ӡ�ļ�¼�����
		/// </summary>
		private int intCurrentRecord=0;  
		/// <summary>
		/// �ɼ�¼����,׼����ӡһ���¼�¼
		/// </summary>
		bool blnBeginPrintNewRecord=true;		
		/// <summary>
		
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
			TopY = 170,
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
			RowStep = 40,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 21,			
			/// <summary>
			/// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
			/// </summary>
			VOffSet = 22,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=16,
			/// <summary>
			/// ��һ�������(X)
			/// </summary>
			ColumnsMark1=85,
			/// <summary>
			/// �ڶ��������(X)
			/// </summary>
			ColumnsMark2=135,
			ColumnsMark3=170,
			ColumnsMark4=200,
			ColumnsMark5=230,
			ColumnsMark6=290,
			ColumnsMark7=325,
			ColumnsMark8=360,
			ColumnsMark9=400,
			ColumnsMark10=440,
			ColumnsMark11=465,
			ColumnsMark12=495,
			ColumnsMark13=520,
			ColumnsMark14=550,
			ColumnsMark15=725				
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
	  

		/// <summary>
		/// �����ӡ��Ԫ�ص������
		/// </summary>
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
						m_fReturnPoint = new PointF(350f,150f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(400f,150f);
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

		private Bitmap imgUserclose;
		private Bitmap imgUseropen;

		protected override Font m_FntHeaderFont
		{
			get
			{
				return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			}
		}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntensiveTendMain));
            this.m_picExpand = new System.Windows.Forms.PictureBox();
            this.m_dtcTemperature = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPulse = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBreath = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureS = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodPressureA = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInID = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcEchoLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcEchoRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcIn_Qty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOutID = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcOut_Qty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.clmSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcContent = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.clmCreateDateofDay = new System.Windows.Forms.DataGridTextBoxColumn();
            this.clmCreateTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picExpand)).BeginInit();
            this.SuspendLayout();
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.clmCreateDateofDay,
																										 this.clmCreateTime,
																										 this.m_dtcTemperature,
																										 this.m_dtcPulse,
																										 this.m_dtcBreath,
																										 this.m_dtcBloodPressureS,
																										 this.m_dtcBloodPressureA,
																										 this.m_dtcPupilLeft,
																										 this.m_dtcPupilRight,
																										 this.m_dtcEchoLeft,
																										 this.m_dtcEchoRight,
																										 this.m_dtcInID,
																										 this.m_dtcIn_Qty,
																										 this.m_dtcOutID,
																										 this.m_dtcOut_Qty,
																										 this.m_dtcContent,
																										 this.clmSign});
            this.dgtsStyles.RowHeaderWidth = 30;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_dtgRecordDetail.HeaderForeColor = System.Drawing.SystemColors.Window;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(28, 94);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(730, 518);
            // 
            // mniAppend
            // 
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.Color.White;
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvInPatientDate.ForeColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.ItemHeight = 18;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(20, 18);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(160, 52);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(521, 51);
            this.lblSex.Size = new System.Drawing.Size(41, 19);
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(603, 51);
            this.lblAge.Size = new System.Drawing.Size(80, 19);
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(368, 19);
            this.lblBedNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(356, 51);
            this.lblInHospitalNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(484, 19);
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(480, 51);
            this.lblSexTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(562, 51);
            this.lblAgeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(192, 51);
            this.lblAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(412, 70);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(64, 104);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(412, 47);
            this.txtInPatientID.Size = new System.Drawing.Size(64, 23);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(528, 15);
            this.m_txtPatientName.Size = new System.Drawing.Size(108, 23);
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(412, 15);
            this.m_txtBedNO.Size = new System.Drawing.Size(48, 23);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(240, 47);
            this.m_cboArea.Size = new System.Drawing.Size(112, 23);
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(528, 38);
            this.m_lsvPatientName.Size = new System.Drawing.Size(108, 104);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(412, 46);
            this.m_lsvBedNO.Size = new System.Drawing.Size(48, 104);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(240, 15);
            this.m_cboDept.Size = new System.Drawing.Size(112, 23);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(192, 19);
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(658, 34);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(460, 15);
            this.m_cmdNext.Size = new System.Drawing.Size(16, 23);
            this.m_cmdNext.Visible = true;
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(648, 12);
            this.m_cmdPre.Size = new System.Drawing.Size(36, 21);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblForTitle.Font = new System.Drawing.Font("����", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblForTitle.Location = new System.Drawing.Point(756, 46);
            this.m_lblForTitle.Size = new System.Drawing.Size(16, 4);
            this.m_lblForTitle.Text = "Σ�ػ����¼";
            this.m_lblForTitle.Visible = false;
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(689, 40);
            // 
            // m_picExpand
            // 
            this.m_picExpand.Image = ((System.Drawing.Image)(resources.GetObject("m_picExpand.Image")));
            this.m_picExpand.Location = new System.Drawing.Point(712, 28);
            this.m_picExpand.Name = "m_picExpand";
            this.m_picExpand.Size = new System.Drawing.Size(16, 16);
            this.m_picExpand.TabIndex = 5002;
            this.m_picExpand.TabStop = false;
            this.m_picExpand.Tag = "1";
            this.m_picExpand.Visible = false;
            this.m_picExpand.Click += new System.EventHandler(this.m_picExpand_Click);
            // 
            // m_dtcTemperature
            // 
            this.m_dtcTemperature.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcTemperature.m_BlnGobleSet = true;
            this.m_dtcTemperature.m_BlnUnderLineDST = false;
            this.m_dtcTemperature.MappingName = "Temperature";
            this.m_dtcTemperature.Width = 50;
            // 
            // m_dtcPulse
            // 
            this.m_dtcPulse.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPulse.m_BlnGobleSet = true;
            this.m_dtcPulse.m_BlnUnderLineDST = false;
            this.m_dtcPulse.MappingName = "Pulse";
            this.m_dtcPulse.Width = 50;
            // 
            // m_dtcBreath
            // 
            this.m_dtcBreath.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBreath.m_BlnGobleSet = true;
            this.m_dtcBreath.m_BlnUnderLineDST = false;
            this.m_dtcBreath.MappingName = "Breath";
            this.m_dtcBreath.Width = 50;
            // 
            // m_dtcBloodPressureS
            // 
            this.m_dtcBloodPressureS.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureS.m_BlnGobleSet = true;
            this.m_dtcBloodPressureS.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureS.MappingName = "BloodPressureS";
            this.m_dtcBloodPressureS.Width = 50;
            // 
            // m_dtcBloodPressureA
            // 
            this.m_dtcBloodPressureA.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodPressureA.m_BlnGobleSet = true;
            this.m_dtcBloodPressureA.m_BlnUnderLineDST = false;
            this.m_dtcBloodPressureA.MappingName = "BloodPressureA";
            this.m_dtcBloodPressureA.Width = 50;
            // 
            // m_dtcInID
            // 
            this.m_dtcInID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInID.m_BlnGobleSet = true;
            this.m_dtcInID.m_BlnUnderLineDST = false;
            this.m_dtcInID.MappingName = "InID";
            this.m_dtcInID.Width = 60;
            // 
            // m_dtcPupilLeft
            // 
            this.m_dtcPupilLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilLeft.m_BlnGobleSet = true;
            this.m_dtcPupilLeft.m_BlnUnderLineDST = false;
            this.m_dtcPupilLeft.MappingName = "PupilLeft";
            this.m_dtcPupilLeft.Width = 50;
            // 
            // m_dtcPupilRight
            // 
            this.m_dtcPupilRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilRight.m_BlnGobleSet = true;
            this.m_dtcPupilRight.m_BlnUnderLineDST = false;
            this.m_dtcPupilRight.MappingName = "PupilRight";
            this.m_dtcPupilRight.Width = 50;
            // 
            // m_dtcEchoLeft
            // 
            this.m_dtcEchoLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcEchoLeft.m_BlnGobleSet = true;
            this.m_dtcEchoLeft.m_BlnUnderLineDST = false;
            this.m_dtcEchoLeft.MappingName = "EchoLeft";
            this.m_dtcEchoLeft.Width = 50;
            // 
            // m_dtcEchoRight
            // 
            this.m_dtcEchoRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcEchoRight.m_BlnGobleSet = true;
            this.m_dtcEchoRight.m_BlnUnderLineDST = false;
            this.m_dtcEchoRight.MappingName = "EchoRight";
            this.m_dtcEchoRight.Width = 50;
            // 
            // m_dtcIn_Qty
            // 
            this.m_dtcIn_Qty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcIn_Qty.m_BlnGobleSet = true;
            this.m_dtcIn_Qty.m_BlnUnderLineDST = false;
            this.m_dtcIn_Qty.MappingName = "In_Qty";
            this.m_dtcIn_Qty.Width = 60;
            // 
            // m_dtcOutID
            // 
            this.m_dtcOutID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOutID.m_BlnGobleSet = true;
            this.m_dtcOutID.m_BlnUnderLineDST = false;
            this.m_dtcOutID.MappingName = "OutID";
            this.m_dtcOutID.Width = 60;
            // 
            // m_dtcOut_Qty
            // 
            this.m_dtcOut_Qty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcOut_Qty.m_BlnGobleSet = true;
            this.m_dtcOut_Qty.m_BlnUnderLineDST = false;
            this.m_dtcOut_Qty.MappingName = "Out_Qty";
            this.m_dtcOut_Qty.Width = 60;
            // 
            // clmSign
            // 
            this.clmSign.Format = "";
            this.clmSign.FormatInfo = null;
            this.clmSign.MappingName = "Sign";
            this.clmSign.NullText = "";
            this.clmSign.Width = 70;
            // 
            // m_dtcContent
            // 
            this.m_dtcContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcContent.m_BlnGobleSet = true;
            this.m_dtcContent.m_BlnUnderLineDST = false;
            this.m_dtcContent.MappingName = "Content";
            this.m_dtcContent.NullText = "";
            this.m_dtcContent.Width = 300;
            // 
            // clmCreateDateofDay
            // 
            this.clmCreateDateofDay.Format = "";
            this.clmCreateDateofDay.FormatInfo = null;
            this.clmCreateDateofDay.MappingName = "CreateDateofDay";
            this.clmCreateDateofDay.NullText = "";
            this.clmCreateDateofDay.Width = 80;
            // 
            // clmCreateTime
            // 
            this.clmCreateTime.Format = "";
            this.clmCreateTime.FormatInfo = null;
            this.clmCreateTime.MappingName = "CreateTime";
            this.clmCreateTime.NullText = "";
            this.clmCreateTime.Width = 60;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(20, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 542);
            this.panel1.TabIndex = 10000004;
            // 
            // frmIntensiveTendMain
            // 
            this.AccessibleDescription = "Σ�ػ����¼";
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(792, 673);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_picExpand);
            this.Name = "frmIntensiveTendMain";
            this.Text = "Σ�ػ����¼";
            this.Load += new System.EventHandler(this.frmIntensiveTendMain_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_picExpand, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
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
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picExpand)).EndInit();
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

		//���ó�ʼ�ıȽ�����
		private DateTime m_dtmPreRecordDate;
		// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		protected override void m_mthClearRecordInfo()
		{			
			m_dtmPreRecordDate=DateTime.MinValue;
			m_dtgRecordDetail.CurrentRowIndex=0;
			m_dtbRecords.Rows.Clear();
		}

		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}

		// ��ʼ���������DataTable��
		// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{

			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("CreateDate");//0
		
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);//1
		
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  //2
		
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate"); //3

			p_dtbRecordTable.Columns.Add("CreateDateofDay");//4
			p_dtbRecordTable.Columns.Add("CreateTime");//5
			p_dtbRecordTable.Columns.Add("Temperature",typeof(clsDSTRichTextBoxValue));//6
			p_dtbRecordTable.Columns.Add("Pulse",typeof(clsDSTRichTextBoxValue));//7
			p_dtbRecordTable.Columns.Add("Breath",typeof(clsDSTRichTextBoxValue));//8
			p_dtbRecordTable.Columns.Add("BloodPressureA",typeof(clsDSTRichTextBoxValue));//9
			p_dtbRecordTable.Columns.Add("BloodPressureS",typeof(clsDSTRichTextBoxValue));//9
			p_dtbRecordTable.Columns.Add("PupilLeft",typeof(clsDSTRichTextBoxValue));//10
			p_dtbRecordTable.Columns.Add("PupilRight",typeof(clsDSTRichTextBoxValue));//11
			p_dtbRecordTable.Columns.Add("EchoLeft",typeof(clsDSTRichTextBoxValue));//12
			p_dtbRecordTable.Columns.Add("EchoRight",typeof(clsDSTRichTextBoxValue));//13
			p_dtbRecordTable.Columns.Add("InID",typeof(clsDSTRichTextBoxValue));//14
			p_dtbRecordTable.Columns.Add("In_Qty",typeof(clsDSTRichTextBoxValue));//15
			p_dtbRecordTable.Columns.Add("OutID",typeof(clsDSTRichTextBoxValue));//16
			p_dtbRecordTable.Columns.Add("Out_Qty",typeof(clsDSTRichTextBoxValue));//17		
			p_dtbRecordTable.Columns.Add("Content",typeof(clsDSTRichTextBoxValue)); //18
			p_dtbRecordTable.Columns.Add("Sign");//19			
			m_dtcContent.m_RtbBase.m_BlnReadOnly = true;

			m_mthSetControl(clmCreateDateofDay);
			m_mthSetControl(clmCreateTime);
			m_mthSetControl(m_dtcTemperature);
			m_mthSetControl(m_dtcPulse);
			m_mthSetControl(m_dtcBreath);
			m_mthSetControl(m_dtcBloodPressureA);
			m_mthSetControl(m_dtcBloodPressureS);
			m_mthSetControl(m_dtcPupilLeft);
			m_mthSetControl(m_dtcPupilRight);
			m_mthSetControl(m_dtcEchoLeft);
			m_mthSetControl(m_dtcEchoRight);
			m_mthSetControl(m_dtcInID);
			m_mthSetControl(m_dtcIn_Qty);
			m_mthSetControl(m_dtcOutID);
			m_mthSetControl(m_dtcOut_Qty);
			m_mthSetControl(m_dtcContent);
			m_mthSetControl(clmSign);
			
			this.clmCreateDateofDay.HeaderText = "\r\n\r\n����";
			this.clmCreateTime.HeaderText = "\r\n\r\nʱ��";
			this.m_dtcTemperature.HeaderText = "��\r\n\r\n��\r\n\r\n��";
			this.m_dtcPulse.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcBreath.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcBloodPressureA.HeaderText = "��\r\n��\r\nѹ\r\n\r\nmmHg";
			this.m_dtcBloodPressureS.HeaderText = "��\r\n��\r\nѹ\r\n\r\nmmHg";
			this.m_dtcPupilLeft.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n(mm)\r\n��";
			this.m_dtcPupilRight.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n(mm)\r\n��";
			this.m_dtcEchoLeft.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";	
			this.m_dtcEchoRight.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";					
			this.m_dtcInID.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcIn_Qty.HeaderText = "��\r\n��\r\n��\r\n(ml)";
			this.m_dtcOutID.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcOut_Qty.HeaderText = "��\r\n��\r\n��\r\n(ml)";
			this.m_dtcContent.HeaderText = "\r\n\r\n �� �� �� ¼";
			this.clmSign.HeaderText = "\r\n\r\nǩ��";			
		}


		/// <summary>
		/// ��ȡ��ӵ�DataTable������
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
 

		private object[][] m_objGetPerDateSummaryRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			#region  ��ȡ��ӵ�DataTable��ͳ�����ݣ�����ͳ�ƣ�
			clsIntensiveTendDataInfo objDataInfo;
			object [][] objData;
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			objDataInfo = (clsIntensiveTendDataInfo)p_objTransDataInfo;
			//û��ͳ������ʱ����ʾͳ�Ƶ�����
			if(!(objDataInfo.m_objItemSummary.m_intTotal_In != 0 || objDataInfo.m_objItemSummary.m_intTotal_Out != 0 ))
			{
				return null;
			}
			//objData��������
			objData = new object[2][];
			//���һ���ǿ���
			objData[1] = new string[21];
			objData[0] = new object[21];   
			bool m_blnIfLastSummary = false;

			#region DSL���
			m_strCreateUserID=objDataInfo.m_objRecordContent.m_dtmCreateDate.ToString();
			m_strCurrentOpenDate=objDataInfo.m_objRecordContent.m_strModifyUserID;
			#endregion DSL���

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
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][12] = objclsDSTRichTextBoxValue;

			strText = "����";
			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][13] = objclsDSTRichTextBoxValue;

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][14] = objclsDSTRichTextBoxValue;
		
			strText = "����";
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][15] = objclsDSTRichTextBoxValue;
		
			strText = objDataInfo.m_objItemSummary.m_intTotal_In.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][16] = objclsDSTRichTextBoxValue;//��ʳ��������λ��
		
			strText = "�ų�";
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][17] = objclsDSTRichTextBoxValue;
		
			strText = objDataInfo.m_objItemSummary.m_intTotal_Out.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][18] = objclsDSTRichTextBoxValue;
		
			if(m_blnIfLastSummary==true)
			{
				object [][] objDataReturn=new object[1][];
				objDataReturn[0]=objData[0];
				return objDataReturn;
			}	
			
			return objData;			

			#endregion
		}

		
		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			try
			{
				//���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsIntensiveTendDataInfo
				object[] objData;
				ArrayList objReturnData=new ArrayList();

				clsIntensiveTendDataInfo objIntesiveTendInfo=new clsIntensiveTendDataInfo();			
				clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
				string strText,strXml;

				//if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.IntensiveTend )
				//2��ʾ��¼��1��ʾ�ܼƣ�0��ʾС��
				if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.IntensiveTend)
				{
					return m_objGetPerDateSummaryRecordsValueArr(p_objTransDataInfo);
				}
		
				//����clsDiseaseTrackInfo������
				objIntesiveTendInfo=(clsIntensiveTendDataInfo)p_objTransDataInfo;

				if(objIntesiveTendInfo.m_objTransDataArr==null)
					return null;
			
				int intSingleTypeCount = objIntesiveTendInfo.m_objTransDataArr.Length;

				//���±����������洢������ų�������
				
				ArrayList objIn;
				ArrayList objOut;
			

				for(int n=0;n<intSingleTypeCount;n++)
				{

					//�������������ֱ���������룬�ų������̼�¼�ļ�¼��
					int intIn_Count=0;
					int intOut_Count=0;
					int intContent_Count=0;

					objIn=new ArrayList();
					objOut=new ArrayList();

					#region ��õ�ǰ��¼���������
					//��ʳ
					string[] strInfo0=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intInD!=0)
					{
						strInfo0[0]="D";
						strInfo0[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intInD.ToString() ;
						strInfo0[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strInDXML;

						objIn.Add(strInfo0);

						intIn_Count++;
					}
				 
					//��Һ
					string[] strInfo1=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intInI!=0)
					{
						strInfo1[0]="I";
						strInfo1[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intInI.ToString();
						strInfo1[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strInIXML;

						objIn.Add(strInfo1);

						intIn_Count++;
					}

					//����Һ
					string[] strInfo2=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intOutE!=0)
					{
						strInfo2[0]="E";
						strInfo2[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intOutE.ToString() ;
						strInfo2[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strOutEXML;

						objOut.Add(strInfo2);

						intOut_Count++;
					}
			
					//���
					string[] strInfo3=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intOutS!=0)
					{
						strInfo3[0]="S";
						strInfo3[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intOutS.ToString() ;
						strInfo3[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strOutSXML;

						objOut.Add(strInfo3);

						intOut_Count++;
					}

					//��
					string[] strInfo4=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intOutU!=0)
					{
						strInfo4[0]="U";
						strInfo4[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intOutU.ToString() ;
						strInfo4[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strOutUXML;

						objOut.Add(strInfo4);

						intOut_Count++;
					}

					//Ż����
					string[] strInfo5=new String[3];
					if(objIntesiveTendInfo.m_objTransDataArr[n].m_intOutV!=0)
					{
						strInfo5[0]="V";
						strInfo5[1]=objIntesiveTendInfo.m_objTransDataArr[n].m_intOutV.ToString();
						strInfo5[2]=objIntesiveTendInfo.m_objTransDataArr[n].m_strOutVXML;

						objOut.Add(strInfo5);

						intOut_Count++;
					}

					//���̼�¼
					string strCase="";
					//ֻ��ʾԭʼֵ��û����Ϊ�ա�tfzhang ���� 2005-7-19 9:57:08
//					if (objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent_Right.Trim().Length==0 || objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent==objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent_Right)
//						strCase = objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent ;
//					else
						strCase = objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent_Right;

					string strCaseXML = objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContentXml ;
					string[] strCaseTextArr,strCaseXmlArr;
					com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(strCase,strCaseXML,18,out strCaseTextArr,out strCaseXmlArr);
//					strCaseTextArr=new string [1];
//					strCaseXmlArr=new string [1];
//					strCaseTextArr[0]=strCase;
//					strCaseXmlArr[0]=strCaseXML;
					int intCaseCount = strCaseTextArr.Length;
					intContent_Count=intCaseCount;

					int intMaxCount = 1;
					if(intMaxCount<intIn_Count)
						intMaxCount=intIn_Count;
					if(intMaxCount<intOut_Count)
						intMaxCount=intOut_Count;
					if(intMaxCount<intContent_Count)
						intMaxCount=intContent_Count;
			
					if(intMaxCount == 0)
						intMaxCount = 1;

					#endregion

//					objData = new object[21]; 
				
			#region Ϊ���鸳ֵ
					///�����ѭ�������ɵ����ݽ���ʾ��DataGrid��,����һ����¼����������DataGrid����ʾ�ж���
					for(int i=0;i<intMaxCount;i++)
					{
						objData = new object[21];   
			
						clsIntensiveTendRecordContent1 objCurrent = (n<intSingleTypeCount)?objIntesiveTendInfo.m_objTransDataArr[n]:null;
						clsIntensiveTendRecordContent1 objNext = (n>= intSingleTypeCount-1)?null:objIntesiveTendInfo.m_objTransDataArr[n+1];
			
						if(i==0)
						{
							//ֻ�ڵ�һ�м�¼����������Ϣ
							objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
							objData[1] = (int)enmRecordsType.IntensiveTend;//��ż�¼���͵�intֵ
							objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
							objData[3] = objIntesiveTendInfo.m_objTransDataArr[objIntesiveTendInfo.m_objTransDataArr.Length-1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   


							if(objCurrent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
							{
								objData[4] = objCurrent.m_dtmCreateDate.Date.ToString("yyyy-MM-dd 00:00:00") ;//�����ַ���
							}
							if(objCurrent.m_dtmCreateDate.ToString("HH:mm") != m_dtmPreRecordDate.ToString("HH:mm"))
							{
								objData[5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
							}
							m_dtmPreRecordDate = objCurrent.m_dtmCreateDate;

						} 				
				
				
						if(i == 0)
						{
							//����
							strText = objCurrent.m_strTemperature;
							strXml = "<root />";
							if(objNext != null && objNext.m_strTemperature  != objCurrent.m_strTemperature)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strTemperature ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[6] = objclsDSTRichTextBoxValue;//T
				
							//����
							strText = objCurrent.m_strPulse ;
							strXml = "<root />";
							if(objNext != null && objNext.m_strPulse != objCurrent.m_strPulse)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strPulse,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[7] = objclsDSTRichTextBoxValue;//HR
	
							//����
							strText = objCurrent.m_strBreath ;
							strXml = "<root />";
							if(objNext != null && objNext.m_strBreath!= objCurrent.m_strBreath)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strBreath,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[8] = objclsDSTRichTextBoxValue;//P
				
							//ѪѹA
							strText = objCurrent.m_strBloodPressureA;
							strXml = "<root />";
							if(objNext != null && objNext.m_strBloodPressureA != objCurrent.m_strBloodPressureA)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureA,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[9] = objclsDSTRichTextBoxValue;//
				
							//ѪѹS
							strText = objCurrent.m_strBloodPressureS ;
							strXml = "<root />";
							if(objNext != null && objNext.m_strBloodPressureS != objCurrent.m_strBloodPressureS)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureS,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[10] = objclsDSTRichTextBoxValue;//
				
							//��ͫ
							strText = objCurrent.m_strPupilLeft ;
							strXml = "<root />";
							if(objNext != null && objNext.m_strPupilLeft != objCurrent.m_strPupilLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strPupilLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[11] = objclsDSTRichTextBoxValue;//
				
							//��ͫ
							strText = objCurrent.m_strPupilRight;
							strXml = "<root />";
							if(objNext != null && objNext.m_strPupilRight != objCurrent.m_strPupilRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strPupilRight ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[12] = objclsDSTRichTextBoxValue;
				
							//����  ��
							strText = objCurrent.m_strEchoLeft;
							strXml = "<root />";
							if(objNext != null && objNext.m_strEchoLeft != objCurrent.m_strEchoLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strEchoLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[13] = objclsDSTRichTextBoxValue;
				
							//����  ��
							strText = objCurrent.m_strEchoRight;
							strXml = "<root />";
							if(objNext != null && objNext.m_strEchoRight != objCurrent.m_strEchoRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(objCurrent.m_strEchoRight,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[14] = objclsDSTRichTextBoxValue;//
		
						}
						else
						{
							//����ֵ
						}

						if(i < intIn_Count)	//��ʳ
						{
							//��������:D,I
							strText =((string[])objIn[i])[0];
							strXml = "<root />";
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[15] = objclsDSTRichTextBoxValue;//�������	

							//�� ml
							strText =((string[])objIn[i])[1];
							switch (((string[])objIn[i])[0])
							{
								case "D":
									if(objNext != null && objNext.m_intInD != objCurrent.m_intInD)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml = ((string[])objIn[i])[2];
									}
									break;
								case "I":
									if(objNext != null && objNext.m_intInI != objCurrent.m_intInI)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml = ((string[])objIn[i])[2];
									}
									break;
							}
							
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[16] = objclsDSTRichTextBoxValue;
						}
						else
						{
							//����ֵ
						}

						if(i < intOut_Count)	//�ų�
						{
							//��������:U,V,S,E
							strText =((string[])objOut[i])[0];
							strXml ="<root />";
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[17] = objclsDSTRichTextBoxValue;

							//�� ml
							strText =((string[])objOut[i])[1];
							switch (((string[])objOut[i])[0])
							{
								case "E":
									if(objNext != null && objNext.m_intOutE != objCurrent.m_intOutE)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml =((string[])objOut[i])[2];
									}
									break;
								case "S":
									if(objNext != null && objNext.m_intOutS != objCurrent.m_intOutS)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml =((string[])objOut[i])[2];
									}
									break;
								case "U":
									if(objNext != null && objNext.m_intOutU != objCurrent.m_intOutU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml =((string[])objOut[i])[2];
									}
									break;
								case "V":
									if(objNext != null && objNext.m_intOutV != objCurrent.m_intOutV)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
									{
										strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
									}
									else
									{
										strXml =((string[])objOut[i])[2];
									}
									break;
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[18] = objclsDSTRichTextBoxValue;

						}
						else
						{
							//����ֵ
						}

						if(i < intCaseCount)
						{
							strText = strCaseTextArr[i];
							if(objNext != null && objNext.m_strRecordContent_Right != objCurrent.m_strRecordContent_Right)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
							{
								strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
							}
							else
							{
								strXml = strCaseXmlArr[i];
							}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[19] = objclsDSTRichTextBoxValue;
						}
						else
						{
							//����ֵ
						}
						clsEmployee m_objSign;
						if((objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount-1].m_strModifyUserName=="" || 
							objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount-1].m_strModifyUserName==null) && 
							objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount-1].m_strModifyUserID!="")
						{
							m_objSign=new clsEmployee(objIntesiveTendInfo.m_objTransDataArr[n/*intSingleTypeCount-1*/].m_strModifyUserID);
							objData[20] = (i==(intMaxCount-1) ? m_objSign.m_StrFirstName : "");//ǩ��
						}
						else
							objData[20] = (i==(intMaxCount-1) ? objIntesiveTendInfo.m_objTransDataArr[n/*intSingleTypeCount-1*/].m_strModifyUserName : "");//ǩ��

						objReturnData.Add(objData);	//��Ӽ�¼
			
					}	
				}		//for(n)����
				object[][] m_objRe=new object[objReturnData.Count][];

				for(int m=0;m<objReturnData.Count ;m++)
					m_objRe[m]=(object[])objReturnData[m];
					
				return m_objRe;
				
			#endregion
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message );
				return null;
			}
		}



		// ��ȡ���̼�¼�������ʵ��
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.IntensiveTend);
		}

		protected override infPrintRecord m_objGetPrintTool()
		{
			return new clsIntensiveTendMainPrintTool();
		}

		// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.IntensiveTend:
					return new frmIntensiveTend();
			}  
		
			return null;
		}


		// ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
			clsTrackRecordContent objContent = null;
			switch((enmDiseaseTrackType)p_intRecordType)
			{ 
				case enmDiseaseTrackType.IntensiveTend:
					objContent = new clsIntensiveTendRecordContent1();
					break;
			}

			if(objContent == null)
				objContent=new clsIntensiveTendRecordContent1();	
		
			if(m_objCurrentPatient !=null)
				objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
			else 
			{
				clsPublicFunction.ShowInformationMessageBox("��ǰ����Ϊ��!");
				return null;
			}
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate;
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
			objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);     
		
			return objContent;
		}



		private void frmIntensiveTendMain_Load(object sender, System.EventArgs e)
		{
			m_dtmPreRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.Focus();
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
			m_mthAddNewRecord((int)enmDiseaseTrackType.IntensiveTend);
		}


		/// <summary>
		/// �Ƿ�չ��
		/// </summary>
		private bool m_blnIsExpand=true;
		private void m_picExpand_Click(object sender, System.EventArgs e)
		{			
			if(m_blnIsExpand)
			{
				m_blnIsExpand=false;
				this.m_picExpand.Image=imgUserclose;				
				this.m_dtcTemperature.Width = 0;
				this.m_dtcPulse.Width = 0;
				this.m_dtcBreath.Width = 0;
				this.m_dtcBloodPressureA.Width = 0;
				this.m_dtcBloodPressureS.Width =0;
				this.m_dtcPupilLeft.Width = 0;
				this.m_dtcPupilRight.Width = 0;	
				this.m_dtcEchoLeft.Width = 0;
				this.m_dtcEchoRight.Width = 0;						
				this.m_dtcInID.Width = 0;
				this.m_dtcIn_Qty.Width = 0;
				this.m_dtcOutID.Width = 0;
				this.m_dtcOut_Qty.Width = 0;	
				this.m_dtcContent.Width = 800;
			}
			else 
			{
				m_blnIsExpand=true;
				this.m_picExpand.Image=imgUseropen;				
				this.m_dtcTemperature.Width = 30;
				this.m_dtcPulse.Width = 30;
				this.m_dtcBreath.Width = 30;
				this.m_dtcBloodPressureA.Width = 50;
				this.m_dtcBloodPressureS.Width =50;
				this.m_dtcPupilLeft.Width = 35;
				this.m_dtcPupilRight.Width = 35;	
				this.m_dtcEchoLeft.Width = 30;
				this.m_dtcEchoRight.Width = 30;						
				this.m_dtcInID.Width = 30;
				this.m_dtcIn_Qty.Width = 35;
				this.m_dtcOutID.Width = 30;
				this.m_dtcOut_Qty.Width = 35;	
				this.m_dtcContent.Width = 390;
			}
			
			if(m_objCurrentPatient !=null)
				m_mthSetPatientFormInfo(m_objCurrentPatient);
		}		

		protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
		{
			m_mthSetPatientFormInfo(m_objCurrentPatient);
		}

		protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
			m_mthSetPatientFormInfo(m_objCurrentPatient);
		}

		#region ��ӡ
		// ���ô�ӡ���ݡ�
		protected override void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
			DateTime[] p_dtmFirstPrintDate)
		{
			if(p_objTransDataArr==null || p_dtmFirstPrintDate==null ||p_objTransDataArr.Length !=p_dtmFirstPrintDate.Length)
			{
				clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
				return;
			}			

			//���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
			clsDiseaseTrackInfo objTrackInfo=null;
			m_objPrintDataArr = new clsPrintData[p_objTransDataArr.Length];			
			for(int i=0;i<p_objTransDataArr.Length;i++)
			{
				switch((enmDiseaseTrackType)p_objTransDataArr[i].m_intFlag)
				{
					case enmDiseaseTrackType.IntensiveTend:
						objTrackInfo = new clsIntensiveRecordInfo();
						break;
				}
		
				//����clsDiseaseTrackInfo������
				objTrackInfo.m_ObjRecordContent = p_objTransDataArr[i].m_objRecordContent;
		
				m_objPrintDataArr[i]=new clsPrintData();
				m_objPrintDataArr[i].m_dtmCreateDate=objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
				//���� clsDiseaseTrackInfo ��õ��ı���Xml
				m_objPrintDataArr[i].m_strContent = objTrackInfo.m_strGetTrackText(); 
				m_objPrintDataArr[i].m_strContentXml = objTrackInfo.m_strGetTrackXml();
				
				clsEmployee objEmployee= new clsEmployee(objTrackInfo.m_ObjRecordContent.m_strModifyUserID);
				string strSignText="";
				if(objEmployee !=null)
					strSignText = objEmployee.m_StrLastName;

				m_objPrintDataArr[i].m_strSign = strSignText;
				m_objPrintDataArr[i].m_strSignXml = "<Root />";
				
				m_objPrintDataArr[i].m_dtmFirstPrintDate=p_dtmFirstPrintDate[i];
			}
		}

		// ��ʼ����ӡ����
		protected override void m_mthInitPrintTool()
		{			
			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 15f);
			m_fotSmallFont = new Font("SimSun",12f);
			m_fotTinyFont=new Font("SimSun",10.5f);
			
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
		
			m_objPageSetting = new clsPrintPageSettingForRecord();
			
			m_objPrintContext= new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fotSmallFont);			
		
			m_objPrintLenth=new clsPrintLenth_IntensiveTendRecord();
			m_objPrintLenth.m_intPrintLenth_BloodPressure = (int) ((float)(enmRecordRectangleInfo.ColumnsMark6-enmRecordRectangleInfo.ColumnsMark5)/2/8.75)+1;//Ѫѹ���ȷ�һ�룬�����ĸ
			m_objPrintLenth.m_intPrintLenth_Breath = (int) ((float)(enmRecordRectangleInfo.ColumnsMark5-enmRecordRectangleInfo.ColumnsMark4)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Echo = (int) ((float)(enmRecordRectangleInfo.ColumnsMark9-enmRecordRectangleInfo.ColumnsMark8)/8.75)+1;		
			m_objPrintLenth.m_intPrintLenth_In = (int) ((float)(enmRecordRectangleInfo.ColumnsMark12-enmRecordRectangleInfo.ColumnsMark11)/8.75)+1;		
			m_objPrintLenth.m_intPrintLenth_Out = (int) ((float)(enmRecordRectangleInfo.ColumnsMark14-enmRecordRectangleInfo.ColumnsMark13)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Pulse = (int) ((float)(enmRecordRectangleInfo.ColumnsMark4-enmRecordRectangleInfo.ColumnsMark3)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Pupil = (int) ((float)(enmRecordRectangleInfo.ColumnsMark7-enmRecordRectangleInfo.ColumnsMark6)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_RecordContent = (int) ((float)(enmRecordRectangleInfo.ColumnsMark15-enmRecordRectangleInfo.ColumnsMark14-6)/17.5)+1;//���̼�¼����人��
			m_objPrintLenth.m_intPrintLenth_Temperature = (int) ((float)(enmRecordRectangleInfo.ColumnsMark3-enmRecordRectangleInfo.ColumnsMark2)/8.75)+1;
					
			intCurrentRecord=0;
			intNowPage=1;
			blnBeginPrintNewRecord=true;		
		}

		// �ͷŴ�ӡ����
		protected override void m_mthDisposePrintTools()
		{
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont.Dispose();
			m_fotTinyFont.Dispose();
			m_GridPen.Dispose();
			m_slbBrush.Dispose();
			
		}

		/// <summary>
		///  ��ʼ��ӡ��
		/// </summary>
		protected override void m_mthStartPrint()
		{
			if(m_blnDirectPrint)
			{
				m_pdcPrintDocument.Print();
			}
			else
			{
				((clsIntensiveTendMainPrintTool)objPrintTool).m_mthPrintPage();
				
			}
//			base.m_mthStartPrint();
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
				m_mthAddDataToGrid(p_objPrintPageArg);
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
			clsEveryRecordPageInfo objEveryRecordPageInfo=new clsEveryRecordPageInfo ();
			//************************************************
			objEveryRecordPageInfo.strAge =m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge.ToString() : "";
			objEveryRecordPageInfo.strPatientName=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			objEveryRecordPageInfo.strBedNo =m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			objEveryRecordPageInfo.strAreaName=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName:"";
			objEveryRecordPageInfo.strSex=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_StrSex:"";
			objEveryRecordPageInfo.strInPatientID=m_objCurrentPatient!=null? m_objCurrentPatient.m_StrInPatientID:"";
			objEveryRecordPageInfo.strPrintDate=m_objCurrentPatient!=null? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		
			
            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("Σ �� �� �� �� �� �� ¼",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAreaName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
			
			//�����һ�������ӡ˵������
			e.Graphics.DrawString("����������:U--�� S--��� V--Ż���� E--����Һ D--��ʳ I--��Һ",m_fotSmallFont,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark2,
				(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*((int)enmRecordRectangleInfo.RowLinesNum+1)-20);

		}

		#endregion
		
		#region ���������Ŀ
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			
			e.Graphics.DrawString("����",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+15,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
		     
			e.Graphics.DrawString("ʱ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);

			//���� C			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY-10+2*(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawString("C",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+9, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+1);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+10);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep-9);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+11);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+10);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+5, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep-9);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+11);

			//Ѫѹ(mmHg)
			e.Graphics.DrawString("Ѫѹ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+8, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
	
			e.Graphics.DrawString(" ͫ ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+31, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��С",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+10, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5-10);
			e.Graphics.DrawString("(mm)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+10, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5+10);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+18, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark9+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+1, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark11+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("ml",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark11+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);


			e.Graphics.DrawString("�ų�",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+1, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("ml",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);


			e.Graphics.DrawString("�� �� �� ¼",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+25, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
	
			e.Graphics.DrawString("ǩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
		
		
		}

		#endregion
//
		#region ������
		/// <summary>
		///  ������
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			
			
			//�����Ӻ���
			for(int i1=0;i1<=(int)enmRecordRectangleInfo.RowLinesNum ;i1++)
			{
				if(i1 !=1 && i1 !=2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1);
				else if(i1==1)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark6,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1-8,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark14,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1-8);
				else //if(i1==2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark6,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark10,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1);
			}
			

			#region ����������
			int intHeight=((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep;
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
			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+intHeight);						
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY+intHeight);

			#endregion

			//���Խ��ߣ�Ѫѹ��
			for(int i1=3;i1<(int)enmRecordRectangleInfo.RowLinesNum ;i1++)//б��ֻ��Ҫ�ӵ����п�ʼ�������ڶ���
			{
				e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark6,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
					(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark5,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*(i1+1));					
			}	
			
		}

						

		#endregion
		
		#region ������ݵ����		
		/// <summary>
		/// ������ݵ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthAddDataToGrid(System.Drawing.Printing.PrintPageEventArgs e)
		{  
			string strCreateDate="";
			string strCreateTime="";
			string strRecord="";			
			string strRecordXML="";			
			DateTime dtmFlagTime;
			/*��¼��ҳ��ǰ�Ĵ�ӡ����*/
			int intNowRow=1; 			

			//ҳ��//////////////////////////////////////////////////////////////
			e.Graphics.DrawString("����"+intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*((int)enmRecordRectangleInfo.RowLinesNum+1)+(int)enmRecordRectangleInfo.VOffSet );
           	
			if(m_objCurrentPatient ==null||m_objPrintDataArr==null)return;
			for(;intCurrentRecord<m_objPrintDataArr.Length ;intCurrentRecord++)
			{
				strCreateDate=m_objPrintDataArr[intCurrentRecord].m_dtmCreateDate.ToString("yyyy-M-d");
				strCreateTime=m_objPrintDataArr[intCurrentRecord].m_dtmCreateDate.ToString("HH:mm");
				
				try
				{

					#region ������¼�¼����ӡ���ڣ����ô�ӡ����ֵ
					if(blnBeginPrintNewRecord)
					{
						if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
							return;

						//��ʼ��ӡһ���¼�¼/////////////////////////////////////////////////////////////////////
						e.Graphics.DrawString(strCreateDate,m_fotSmallFont ,m_slbBrush,
							(int)enmRecordRectangleInfo.LeftX+1, 
							(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*(intNowRow+2) +(int)enmRecordRectangleInfo.VOffSet);	
						e.Graphics.DrawString(strCreateTime,m_fotSmallFont ,m_slbBrush,
							(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1+1, 
							(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*(intNowRow+2) +(int)enmRecordRectangleInfo.VOffSet);					
						
						strRecord =m_objPrintDataArr[intCurrentRecord].m_strContent;
						strRecordXML=m_objPrintDataArr[intCurrentRecord].m_strContentXml;
					
						//��ӡһ����¼/////////////////////////////////////////////////////////////////////
						/*�޸Ĵ�ӡ���ݷ�ʽ���Ե�һ�δ�ӡʱ��Ϊ�ָ��ʱ���������޸ĵĺۼ���Ҫ���������δ��ӡ������ʾ��ȷ�ļ�¼��*/				
						if(m_objPrintDataArr[intCurrentRecord].m_dtmFirstPrintDate==DateTime.MinValue)
							dtmFlagTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
						else 
							dtmFlagTime=m_objPrintDataArr[intCurrentRecord].m_dtmFirstPrintDate;
						
						m_objPrintContext.m_mthSetContextWithCorrectBefore(strRecord,strRecordXML,dtmFlagTime);					
					
						com.digitalwave.controls.ctlRichTextBox.clsModifyUserInfo[] m_objModifyUserArr = m_objPrintContext.m_ObjModifyUserArr;

						for(int i=0;i< m_objModifyUserArr.Length;i++)
						{
							if(m_objModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
							{
								m_objModifyUserArr[i].m_clrText = Color.Black;
							}
						}

					}

					#endregion
				}
				catch(Exception ex)
				{
					clsPublicFunction.ShowInformationMessageBox(ex.Message);
				}					
					
					

				#region ����ǰ��¼��ǩ����ȫ���������;��ҳ����						
				while( m_objPrintContext.m_BlnHaveNextLine() )
				{
					if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
						return;

					m_objPrintContext.m_mthPrintLine((int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14, 
						(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*(intNowRow+3-1)+(int)enmRecordRectangleInfo.VOffSet ,e.Graphics,m_objPrintLenth.m_intPrintLenth_RecordContent);	  
											
					//��������һ��������û�д���ʱ����ǰ�в�����
					if(m_objPrintContext.m_BlnHaveNextLine())
					{
						blnBeginPrintNewRecord=false;//��ǰ��¼û�д���
						intNowRow ++;//���¹���
					}
				}

				#endregion
//					

				#region ǩ��					
				e.Graphics.DrawString(m_objPrintDataArr[intCurrentRecord].m_strSign ,m_fotSmallFont ,m_slbBrush,
					(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15+1, 
					(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*(intNowRow+3-1)+(int)enmRecordRectangleInfo.VOffSet);
				blnBeginPrintNewRecord=true;  //��ǰ��¼����	
				intNowRow ++;//���¹���

			
				#endregion
				
			}
			

			#region ��ӡ��ϣ�ReSet(��λ)����
			if(intCurrentRecord==m_objPrintDataArr.Length)
			{				
				intCurrentRecord=0;//��ǰ��¼����Ÿ�λ���Ա���һ�δ�ӡ����
				blnBeginPrintNewRecord=true;//��λ
				intNowPage=1;//��λ						
			}

			#endregion
			
		}

		/// <summary>
//		/// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
		/// </summary>
		/// <param name="p_intNowRow">��ǰ��ӡ�У���p_intNowRow��</param>
		/// <param name="e"></param>
		/// <returns></returns>
		private bool m_blnCheckPageChange(int p_intNowRow,System.Drawing.Printing.PrintPageEventArgs e)
		{
			//����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
			if(p_intNowRow>(int)enmRecordRectangleInfo.RowLinesNum-3/*��ȥ��ͷ3��������Ч����*/) 
			{
				e.HasMorePages =true;
				intNowPage ++;

				return true;
			}
			else return false;
		}
 


		#endregion 					

		#endregion ��ӡ

		/// <summary>
		/// ��ȡ��ǰ���˵���������
		/// </summary>
		/// <param name="p_dtmRecordDate">��¼����</param>
		/// <param name="p_intFormID">����ID</param>
		protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate,int p_intFormID)
		{			
			m_mthGetDeletedRecord(p_intFormID,p_dtmRecordDate);
		}

		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
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
			//			//��ʾ����
			//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
			//			{
			//				m_mthSetPatientFormInfo(m_objCurrentPatient);
			//			}
		}
	}
}