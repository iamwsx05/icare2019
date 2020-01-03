using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using com.digitalwave.controls;
using System.Data; 
 
namespace iCare
{
	public class frmMainGeneralNurseRecord : iCare.frmRecordsBase
	{
		private cltDataGridDSTRichTextBox clmContent;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private DataGridColumnStyle GridDateColumn;
		private PinkieControls.ButtonXP m_cmdThreeMeasure;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components = null;

		

		public frmMainGeneralNurseRecord()
		{
			// This call is required by the Windows Form Designer.
			
			InitializeComponent();
            // 
            // dgtsStyles *******
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {dataGridTextBoxColumn1,
																										
																										 this.clmContent,
																										 this.dataGridTextBoxColumn2});
            this.dgtsStyles.PreferredColumnWidth = 85;
            this.dgtsStyles.RowHeaderWidth = 15;
            //            *******
			clmContent.m_RtbBase.ScrollBars = RichTextBoxScrollBars.None;
			//��ҳ����
		
//			m_dtgRecordDetail.TableStyles.Add(DGStyle);
		}

		#region �йش�ӡ������	
		/// <summary>
		/// ���д�ӡ������
		/// </summary>
		private clsPrintData[] m_objPrintDataArr;
		private class clsPrintData
		{
			public string m_strContent;
			public string m_strContentXml;
			public string m_strSign;
//			public string m_strSignXml;
			public DateTime m_dtmFirstPrintDate;
			public string m_strCreateDate;
		}
		
		private com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext;
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
		private int intNowPage;
		/// <summary>
		/// ��ǰ��ӡ�Ļ����¼
		/// </summary>
		private int intCurrentRecord;
		/// <summary>
		/// ׼����ӡһ���¼�¼(������������¼,��������¼����)
		/// </summary>
		private bool blnBeginPrintNewRecord=true;		
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
			public string strDeptName;
			public string strInPatientID;
//			public int intCurrentPage;
//			public int intTotalPages;
//			public string strPrintDate;
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
			RightX = 827-30,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 40,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 21,
			/// <summary>
			/// ���̼�¼ÿ�е�pixel����
			/// </summary>
			RecordLineLength=520,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=3,
			/// <summary>
			/// ��һ�������(X)
			/// </summary>
			ColumnsMark1=160,
			/// <summary>
			/// �ڶ��������(X)
			/// </summary>
			ColumnsMark2=680
				
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
			RecordSign2
		
		}
	  
	
		#endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainGeneralNurseRecord));
            this.clmContent = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_cmdThreeMeasure = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.AllowSorting = false;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(8, 77);
            this.m_dtgRecordDetail.PreferredColumnWidth = 85;
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(785, 544);
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
            this.m_trvInPatientDate.Location = new System.Drawing.Point(224, 187);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(156, 60);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.SystemColors.Control;
            this.lblSex.Location = new System.Drawing.Point(258, 176);
            this.lblSex.Size = new System.Drawing.Size(48, 24);
            this.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.SystemColors.Control;
            this.lblAge.Location = new System.Drawing.Point(248, 161);
            this.lblAge.Size = new System.Drawing.Size(48, 24);
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(233, 176);
            this.lblBedNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(272, 176);
            this.lblInHospitalNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(230, 181);
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(278, 184);
            this.lblSexTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(270, 188);
            this.lblAgeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(233, 173);
            this.lblAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(236, 155);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(76, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(236, 164);
            this.txtInPatientID.Size = new System.Drawing.Size(76, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.BorderColor = System.Drawing.Color.White;
            this.m_txtPatientName.Location = new System.Drawing.Point(236, 167);
            this.m_txtPatientName.Size = new System.Drawing.Size(104, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(256, 158);
            this.m_txtBedNO.Size = new System.Drawing.Size(56, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(224, 179);
            this.m_cboArea.Size = new System.Drawing.Size(116, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(236, 169);
            this.m_lsvPatientName.Size = new System.Drawing.Size(92, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(236, 155);
            this.m_lsvBedNO.Size = new System.Drawing.Size(60, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(236, 161);
            this.m_cboDept.Size = new System.Drawing.Size(116, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(233, 162);
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(268, 218);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(268, 171);
            this.m_cmdNext.Size = new System.Drawing.Size(20, 24);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(224, 173);
            this.m_cmdPre.Size = new System.Drawing.Size(28, 16);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(268, 187);
            this.m_lblForTitle.Size = new System.Drawing.Size(4, 8);
            this.m_lblForTitle.Text = "һ �� �� �� �� ¼";
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(429, 188);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(720, 36);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 28);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(3, 7);
            this.m_pnlNewBase.Size = new System.Drawing.Size(794, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(792, 29);
            // 
            // clmContent
            // 
            this.clmContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clmContent.HeaderText = "��¼����";
            this.clmContent.m_BlnGobleSet = true;
            this.clmContent.m_BlnUnderLineDST = false;
            this.clmContent.MappingName = "clmContent";
            this.clmContent.NullText = "";
            this.clmContent.Width = 500;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "��¼����";
            this.dataGridTextBoxColumn1.MappingName = "CreateDate";
            this.dataGridTextBoxColumn1.NullText = "";
            this.dataGridTextBoxColumn1.Width = 150;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "ǩ��";
            this.dataGridTextBoxColumn2.MappingName = "SignName";
            this.dataGridTextBoxColumn2.NullText = "";
            this.dataGridTextBoxColumn2.Width = 65;
            // 
            // m_cmdThreeMeasure
            // 
            this.m_cmdThreeMeasure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdThreeMeasure.DefaultScheme = true;
            this.m_cmdThreeMeasure.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdThreeMeasure.Hint = "";
            this.m_cmdThreeMeasure.Location = new System.Drawing.Point(602, 36);
            this.m_cmdThreeMeasure.Name = "m_cmdThreeMeasure";
            this.m_cmdThreeMeasure.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdThreeMeasure.Size = new System.Drawing.Size(108, 28);
            this.m_cmdThreeMeasure.TabIndex = 10000006;
            this.m_cmdThreeMeasure.TabStop = false;
            this.m_cmdThreeMeasure.Text = "�� �� ��";
            this.m_cmdThreeMeasure.Click += new System.EventHandler(this.m_cmdThreeMeasure_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(3, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 552);
            this.panel1.TabIndex = 10000007;
            // 
            // frmMainGeneralNurseRecord
            // 
            this.AccessibleDescription = "һ�㻤���¼";
            this.AutoScroll = false;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 673);
            this.Controls.Add(this.m_cmdThreeMeasure);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainGeneralNurseRecord";
            this.Text = "һ�㻤���¼";
            this.Load += new System.EventHandler(this.frmMainGeneralNurseRecord_Load);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_cmdThreeMeasure, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void CreateDataGridStyle() 
		{


			PropertyDescriptorCollection pcol = this.BindingContext[m_dtbRecords].GetItemProperties();

			GridDateColumn = new ColumnStyle(pcol[0]);
			GridDateColumn.NullText="";
			GridDateColumn.HeaderText = "";
			GridDateColumn.MappingName = "PagiNation";
			GridDateColumn.Width = 10;
			dgtsStyles.GridColumnStyles.Add(GridDateColumn);
//			dgtsStyles.GridColumnStyles.Add(clmContent);
//			dgtsStyles.GridColumnStyles.Add(dataGridTextBoxColumn2);
		}


		// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		protected override void m_mthClearRecordInfo()
		{			
			m_dtgRecordDetail.CurrentRowIndex=0;
			m_dtbRecords.Rows.Clear();
		}

		// ��ʼ���������DataTable��
		// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{
			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("CreateDate");
		
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);
		
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  
		
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate"); 
		
			//�����ʾ����
			p_dtbRecordTable.Columns.Add("clmContent",typeof(clsDSTRichTextBoxValue)); 

			//��ż�¼��ǩ���ַ���
			p_dtbRecordTable.Columns.Add("SignName"); 

			//��ż�¼�ķ�ҳ��־
			p_dtbRecordTable.Columns.Add("PagiNation");

            //��ż�¼������ID
            p_dtbRecordTable.Columns.Add("CreateUserID"); 

			//���ü�¼���ݵ��Ҽ��˵�
			m_mthSetControl(clmContent);
			//���ü�¼���ڵ��Ҽ��˵�
//			m_mthSetControl(dataGridTextBoxColumn1);
//				m_mthSetControl(GridDateColumn);
			//����ǩ�����Ҽ��˵�
			m_mthSetControl(dataGridTextBoxColumn2);
			clmContent.m_RtbBase.m_BlnReadOnly = true;
		}

		// ��ȡ��ӵ�DataTable������
		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			//���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
			clsDiseaseTrackInfo objTrackInfo = null;

			objTrackInfo = new clsGeneralNurseRecordInfo();

			//����clsDiseaseTrackInfo������
			objTrackInfo.m_ObjRecordContent = p_objTransDataInfo.m_objRecordContent;
		
			//���� clsDiseaseTrackInfo ��õ��ı���Xml  
			string strText = objTrackInfo.m_strGetTrackText(); 
			string strXML = objTrackInfo.m_strGetTrackXml();

			int intCharPerLine = clmContent.Width/14-4;//jacky=2003-6-1

			
			//���DataGridÿ�е���ʾ��Ŀ
			//����ÿ�����ݵ��ı���Xml����
			string [] strTextArr,strXmlArr;			

			com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strText.TrimEnd(new char[]{'\n','\r'}),strXML,64,out strTextArr,out strXmlArr);

		
			object [][] objData = new object[strTextArr.Length + 1][];
            int i;
			for(i=0;i<objData.Length - 1;i++)
			{
				objData[i] = new object[8];
			
				//����ֵ
				if(i==0)
				{
					//ֻ�ڵ�һ�м�¼����������Ϣ
					//objData[i][0] = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");//��ż�¼ʱ����ַ���
					objData[i][0] = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmGeneralNurseRecord"));//��ż�¼ʱ����ַ���
					objData[i][1] =(int)objTrackInfo.m_enmGetTrackType() ;//��ż�¼���͵�intֵ
					objData[i][2] =objTrackInfo.m_ObjRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");//��ż�¼��OpenDate�ַ���
					objData[i][3] =objTrackInfo.m_ObjRecordContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss") ;//��ż�¼��ModifyDate�ַ���  
					objData[i][6]=objTrackInfo.m_ObjRecordContent.m_StrPagination;
                    objData[i][7] = objTrackInfo.m_ObjRecordContent.m_strCreateUserID;
				}

				clsDSTRichTextBoxValue objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
				objclsDSTRichTextBoxValue.m_strText=strTextArr[i];						
				objclsDSTRichTextBoxValue.m_strDSTXml=strXmlArr[i];
				
				objData[i][4]=objclsDSTRichTextBoxValue;//��¼����	
			
//                if(i+1 == objData.Length)
////					objData[i][5]=objTrackInfo.m_ObjRecordContent.m_strModifyUserID;//ǩ��
//                    objData[i][5]=objTrackInfo.m_strGetSignText();//ǩ��
			}
            objData[i] = new object[8];
			objData[i][5]=objTrackInfo.m_strGetSignText();//ǩ��
			return objData;
		

		}

		// ��ȡ�����¼�������ʵ��
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.GeneralNurseRecord);
		}

		protected override infPrintRecord m_objGetPrintTool()
		{
			return new clsGeneralNurseRecordPrintTool();
		}

        //protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        //{
 
        
        //        TreeNode trvTemp = m_trvInPatientDate.SelectedNode;
        //        m_trvInPatientDate.SelectedNode = null;
        //        m_trvInPatientDate.SelectedNode = trvTemp;
           
        //}
		// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
            //switch((enmDiseaseTrackType)p_intRecordType)
            //{
            //    case enmDiseaseTrackType.GeneralNurseRecord:
					return new frmGeneralNurseRecord();
            //}  
		
            //return null;
		}

		// ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
			clsTrackRecordContent objContent = null;
			switch((enmDiseaseTrackType)p_intRecordType)
			{ 
				case enmDiseaseTrackType.GeneralNurseRecord:
					objContent = new clsGeneralNurseRecordContent();
					break;
			}
		
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
            objContent.m_strCreateUserID = (string)p_objDataArr[7];   
		
			return objContent;
		}

		private void mniGeneralDisease_Click(object sender, System.EventArgs e)
		{
			m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralDisease);
		}

		private void mniAppend_Click(object sender, System.EventArgs e)
		{			
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(PrivilegeData.enmPrivilegeSF.frmMainGeneralNurseRecord,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralNurseRecord);
		}

		#region ����쳣��Ϣ�����÷�ҳ
		private void frmMainGeneralNurseRecord_Load(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MenuItem mniPageAdd=new System.Windows.Forms.MenuItem();
			mniPageAdd.Index = 0;
			mniPageAdd.Text = "����";
			mniPageAdd.Click += new System.EventHandler(mniPageAdd_Click);
			System.Windows.Forms.MenuItem mniPageRemove=new System.Windows.Forms.MenuItem();
			mniPageRemove.Index = 1;
			mniPageRemove.Text = "���";
			mniPageRemove.Click += new System.EventHandler(mniPageRemove_Click);
			System.Windows.Forms.MenuItem menuItem1=new System.Windows.Forms.MenuItem();
			menuItem1.Index = 5;
			menuItem1.Text = "-";
			System.Windows.Forms.MenuItem menuItem2=new System.Windows.Forms.MenuItem();
			menuItem2.Index = 6;
			menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] 
			{
				mniPageAdd,
				mniPageRemove
			});
			menuItem2.Text = "���÷�ҳ";
			
			System.Windows.Forms.MenuItem menuItem3=new System.Windows.Forms.MenuItem();
			menuItem3.Index = 7;
			menuItem3.Text = "-";
			System.Windows.Forms.MenuItem mniAbnormalInfo=new System.Windows.Forms.MenuItem();
			mniAbnormalInfo.Index = 3;
			mniAbnormalInfo.Text = "�쳣��Ϣ";
			mniAbnormalInfo.Click += new System.EventHandler(this.mniAbnormalInfo_Click);
			mniAbnormalInfo.Enabled = true;
			this.ctmRecordControl.MenuItems.Add(menuItem1);
			this.ctmRecordControl.MenuItems.Add(menuItem2);
			this.ctmRecordControl.MenuItems.Add(menuItem3);
			this.ctmRecordControl.MenuItems.Add(mniAbnormalInfo);
				CreateDataGridStyle();
		}
		private clsInPatientEvaluateDomain m_objInPatientEvaluateDomain=new clsInPatientEvaluateDomain();
		private void mniAbnormalInfo_Click(object sender, System.EventArgs e)
		{
			string strAbnormalInfo;
			if(txtInPatientID.Text.Trim()=="" || this.m_trvInPatientDate.SelectedNode==null || this.m_trvInPatientDate.SelectedNode==m_trvInPatientDate.Nodes[0])
				return;
            long lngRes = m_objInPatientEvaluateDomain.m_lngGetAbnormalInfo(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strAbnormalInfo);
			if(lngRes<=0)
			{
				if(lngRes==(long) enmOperationResult.Not_permission)
					m_mthShowNotPermitted();
				else m_mthShowDBError();
			}
			else 
			{
				frmMessageForm frmmessageform=new frmMessageForm("�쳣��Ϣ:",strAbnormalInfo);
				frmmessageform.ShowDialog();
			}
		}
		/// <summary>
		/// ��ӷ�ҳ��־
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mniPageAdd_Click(object sender, System.EventArgs e)
		{
			m_mthSetPagination("1");
		}
		/// <summary>
		/// ɾ����ҳ��־
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mniPageRemove_Click(object sender, System.EventArgs e)
		{
			m_mthSetPagination("0");
		}
		#endregion ����쳣��Ϣ

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

				objTrackInfo = new clsGeneralNurseRecordInfo();
		
				//����clsDiseaseTrackInfo������
				objTrackInfo.m_ObjRecordContent = p_objTransDataArr[i].m_objRecordContent;
		
				m_objPrintDataArr[i]=new clsPrintData();
				//���� clsDiseaseTrackInfo ��õ��ı���Xml
				m_objPrintDataArr[i].m_strCreateDate = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate.ToString();
				m_objPrintDataArr[i].m_strContent = objTrackInfo.m_strGetTrackText(); 
				m_objPrintDataArr[i].m_strContentXml = objTrackInfo.m_strGetTrackXml();
				
				string strSignText=objTrackInfo.m_strGetSignText();

				m_objPrintDataArr[i].m_strSign =  strSignText;				
				
				m_objPrintDataArr[i].m_dtmFirstPrintDate=p_dtmFirstPrintDate[i];
			}
		}

		// ��ʼ����ӡ����
		protected override void m_mthInitPrintTool()
		{			
			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 18);
			m_fotSmallFont = new Font("SimSun",12);
			
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
		
			m_objPageSetting = new clsPrintPageSettingForRecord();

			m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fotSmallFont);

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
				((clsGeneralNurseRecordPrintTool)objPrintTool).m_mthPrintPage();
				
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
				m_mthPrintTitleInfo(p_objPrintPageArg);	//e
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
			objEveryRecordPageInfo.strDeptName= m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName : "";			
			objEveryRecordPageInfo.strSex=m_objCurrentPatient!=null? m_objCurrentPatient.m_ObjPeopleInfo.m_StrSex:"";
			objEveryRecordPageInfo.strInPatientID=m_objCurrentPatient!=null? m_objCurrentPatient.m_StrInPatientID:"";
			

            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("һ   ��   ��   ��   ��   ¼",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("����:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("����:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("����:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strDeptName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

			e.Graphics.DrawString("����:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ��:",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
		}
		#endregion		

		#region ���������Ŀ
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			
			e.Graphics.DrawString("��¼ʱ��",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+1,
				(int)enmRecordRectangleInfo.TopY+2);
		     
			e.Graphics.DrawString("�� �� �� ¼",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+185, (int)enmRecordRectangleInfo.TopY+2);
	
			e.Graphics.DrawString("ǩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1,(int)enmRecordRectangleInfo.TopY+2);
		}
		#endregion

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
				e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
					(int)enmRecordRectangleInfo.RightX,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1);
			}

			//����������
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep);
		}

						
		#endregion

		#region ������ݵ����
		/// <summary>
		/// ������ݵ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthAddDataToGrid(System.Drawing.Printing.PrintPageEventArgs e)
		{
			try
			{
				int intPrintLenth=(int) ((float)enmRecordRectangleInfo.RecordLineLength/17.5)+1; /*ÿ����ʾ�ĺ��ֵ���Ŀ*/				
				string strRecord="";
				string strRecordXML="";				
				DateTime dtmFlagTime;
			
				int intNowRow=1; /*��¼��ҳ��ǰ�Ĵ�ӡ����*/

				//ҳ��//////////////////////////////////////////////////////////////
				e.Graphics.DrawString("����"+intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2 ,
					(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*((int)enmRecordRectangleInfo.RowLinesNum+1)+10 );
      
				if(m_objCurrentPatient ==null || m_objPrintDataArr == null)	return;
				for(;intCurrentRecord<m_objPrintDataArr.Length;intCurrentRecord++)	//������ʼ��
				{	
					#region ������¼�¼����ӡ���ڣ����ô�ӡ����ֵ
					if(blnBeginPrintNewRecord)
					{	
						//��ӡ����
						e.Graphics.DrawString(m_objPrintDataArr[intCurrentRecord].m_strCreateDate,m_fotSmallFont ,m_slbBrush,
							(int)enmRecordRectangleInfo.LeftX+1, 
							(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*intNowRow + 20);						

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

					#region ����ǰ��¼��ǩ����ȫ���������;��ҳ����	
					while(m_objPrintContext.m_BlnHaveNextLine())//�жϸ�����¼�Ƿ�����һ��
					{
						if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
							return;

						m_objPrintContext.m_mthPrintLine((int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1, 
							(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*intNowRow + 20,e.Graphics,intPrintLenth);
											

						//��������һ��������û�д���ʱ����ǰ�в�����
						if(m_objPrintContext.m_BlnHaveNextLine())
						{
							blnBeginPrintNewRecord=false;//��ǰ��¼û�д���
							intNowRow ++;//���¹���
						}
					}					
					#endregion
					
					#region ǩ��
					e.Graphics.DrawString(m_objPrintDataArr[intCurrentRecord].m_strSign,m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2+1, 
						(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*intNowRow + 20);//+8);					
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
			catch(Exception err)
			{
				clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" +err.StackTrace);
			}
		}	

		/// <summary>
		/// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
		/// </summary>
		/// <param name="p_intNowRow">��ǰ��ӡ�У���p_intNowRow��</param>
		/// <param name="e"></param>
		/// <returns></returns>
		private bool m_blnCheckPageChange(int p_intNowRow,System.Drawing.Printing.PrintPageEventArgs e)
		{
			//����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
			if(p_intNowRow>(int)enmRecordRectangleInfo.RowLinesNum-1/*��ȥ��ͷ1��������Ч����*/) 
			{
				e.HasMorePages =true;
				intNowPage ++;

				return true;
			}
			else return false;
		}

		
		#endregion 
	
	    #region �����ӡ��Ԫ�ص������
		protected class clsPrintPageSettingForRecord
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
						m_fReturnPoint = new PointF(320f,30f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(225f,70f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,120f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,120f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(150f,120f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(200f,120f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(250f,120f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(300f,120f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(350f,120f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(400f,120f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(550f,120f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(600f,120f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,120f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(710f,120f);
						break;
											
					default:
						m_fReturnPoint = new PointF(400f,400f);
						break;
	
				}
				return m_fReturnPoint;
			}
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

		private void m_cmdThreeMeasure_Click(object sender, System.EventArgs e)
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
				Form frm;
//				frmThreeMeasureRecord frmChild;
//				frmThreeMeasureRecordGN frmChild;
				for(int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
				{
//					frmChild = this.MdiParent.MdiChildren[i] as frmThreeMeasureRecord;
//					frmChild = this.MdiParent.MdiChildren[i] as frmThreeMeasureRecordGN;
					frm = this.MdiParent.MdiChildren[i];
					if((frm is frmThreeMeasureRecord) || (frm is frmThreeMeasureRecordGN))
					{
						frm.WindowState = FormWindowState.Normal;
						frm.WindowState = FormWindowState.Maximized;
						this.MdiParent.MdiChildren[i].Activate();
						this.Cursor=Cursors.Default;//Cursor���ر�ע��Ŷ
						return;
					}
				}
				if(clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
				{
					//				frmChild = new frmThreeMeasureRecord();
					frmThreeMeasureRecordGN frmChild = new frmThreeMeasureRecordGN();
					frmChild.MdiParent = this.MdiParent;
					frmChild.m_mthDisableSelectPatient(false);
					frmChild.Show(); 
					frmChild.m_mthSetPatient(p_objPatient);
				}
				else
				{
					frmThreeMeasureRecord frmChild = new frmThreeMeasureRecord();
					frmChild.MdiParent = this.MdiParent;
					frmChild.m_mthDisableSelectPatient(false);
					frmChild.Show(); 
					frmChild.m_mthSetPatient(p_objPatient);
				}
			}
			catch
			{}
            finally
            {
                this.Cursor = Cursors.Default;
            }
		}

        #region ��������
        protected override bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            bool blnIsOK = false;
            if (m_blnSubIsExists())
            {
                return blnIsOK;
            }
            if (p_objSelectedValue != null)
            {
                m_mthGetDeletedRecord(-1, p_objSelectedValue.m_DtmOpenDate);
                blnIsOK = true;
            }
            return blnIsOK;
        }

        protected override clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            clsInactiveRecordInfo_VO[] objArr = null;
            m_objGetRecordsDomain().m_lngGetAllInactiveInfo(null,p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate, out objArr);
            return objArr;
        }
        public override bool m_blnIsNewSetInactiveForm
        {
            get { return true; }
        }
        #endregion ��������

	}

}

