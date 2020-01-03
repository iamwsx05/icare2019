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
	/// ��������������۲��¼ ��ժҪ˵����
	/// </summary>
	public class frmPostartumSeeRecord: iCare.frmRecordsBase
	{
		#region system define
		
		private string m_strCurrentOpenDate = "";
		private string m_strCreateUserID = "";
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBLOODPRESSURE_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcPULSE_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcUTERUS_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBLOODED_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBREAKWATER_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcEMBRYO_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcUTERUSSIZE_CHR;
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcSIGNNAME_CHR;
		
		private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcBODYTEMPARTURE_CHR;

		private System.Windows.Forms.DataGridTextBoxColumn m_dtcRecordDate_chr;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcTime_chr;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
         
		public frmPostartumSeeRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			InitializeComponent();
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
                this.Text = "���������������̹۲��¼";
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.m_dtcRecordDate_chr = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcTime_chr = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcBLOODPRESSURE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPULSE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcUTERUS_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBLOODED_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBREAKWATER_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcEMBRYO_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcUTERUSSIZE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSIGNNAME_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBODYTEMPARTURE_CHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();

            // dgtsStyles
            // 
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.m_dtcRecordDate_chr,
																										 this.m_dtcTime_chr,
																										 this.m_dtcBLOODPRESSURE_CHR,
																										 this.m_dtcBODYTEMPARTURE_CHR,
																										 this.m_dtcPULSE_CHR,
																										 this.m_dtcUTERUS_CHR,
																										 this.m_dtcBLOODED_CHR,
																										 this.m_dtcBREAKWATER_CHR,
																										 this.m_dtcEMBRYO_CHR,
																										 this.m_dtcUTERUSSIZE_CHR,
																										 this.m_dtcSIGNNAME_CHR});
            this.dgtsStyles.RowHeaderWidth = 15;
            // 
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.m_dtgRecordDetail.HeaderForeColor = System.Drawing.SystemColors.Window;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(11, 72);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(787, 528);
            // 
            // mniAppend
            // 
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.Color.White;
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 10.5F);
            this.m_trvInPatientDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_trvInPatientDate.LineColor = System.Drawing.Color.Black;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(776, 124);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(10, 10);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(749, 98);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(739, 79);
            this.lblAge.Size = new System.Drawing.Size(78, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(764, 75);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(725, 102);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(749, 88);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(739, 122);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(749, 88);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(743, 99);
            this.lblAreaTitle.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(725, 95);
            this.txtInPatientID.Size = new System.Drawing.Size(10, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(826, 95);
            this.m_txtPatientName.Size = new System.Drawing.Size(10, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(826, 95);
            this.m_txtBedNO.Size = new System.Drawing.Size(10, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(748, 73);
            this.m_cboArea.Size = new System.Drawing.Size(10, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(781, 95);
            this.m_lsvPatientName.Size = new System.Drawing.Size(10, 12);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(803, 73);
            this.m_lsvBedNO.Size = new System.Drawing.Size(10, 10);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(748, 95);
            this.m_cboDept.Size = new System.Drawing.Size(10, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(755, 82);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(752, 86);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cmdNext.Location = new System.Drawing.Point(742, 105);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(761, 90);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(780, 76);
            this.m_lblForTitle.Size = new System.Drawing.Size(8, 23);
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(542, 43);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(675, 8);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = true;
            this.m_ctlPatientInfo.m_BlnIsShowOccupy = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // m_dtcRecordDate_chr
            // 
            this.m_dtcRecordDate_chr.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcRecordDate_chr.Format = "";
            this.m_dtcRecordDate_chr.FormatInfo = null;
            this.m_dtcRecordDate_chr.MappingName = "RecordDate_chr";
            this.m_dtcRecordDate_chr.Width = 90;
            // 
            // m_dtcTime_chr
            // 
            this.m_dtcTime_chr.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcTime_chr.Format = "";
            this.m_dtcTime_chr.FormatInfo = null;
            this.m_dtcTime_chr.MappingName = "Time_chr";
            this.m_dtcTime_chr.Width = 65;
            // 
            // m_dtcBLOODPRESSURE_CHR
            // 
            this.m_dtcBLOODPRESSURE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBLOODPRESSURE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBLOODPRESSURE_CHR.m_BlnGobleSet = true;
            this.m_dtcBLOODPRESSURE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBLOODPRESSURE_CHR.MappingName = "BLOODPRESSURE_CHR";
            this.m_dtcBLOODPRESSURE_CHR.Width = 80;
            // 
            // m_dtcPULSE_CHR
            // 
            this.m_dtcPULSE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcPULSE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPULSE_CHR.m_BlnGobleSet = true;
            this.m_dtcPULSE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcPULSE_CHR.MappingName = "PULSE_CHR";
            this.m_dtcPULSE_CHR.Width = 60;
            // 
            // m_dtcUTERUS_CHR
            // 
            this.m_dtcUTERUS_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcUTERUS_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcUTERUS_CHR.m_BlnGobleSet = true;
            this.m_dtcUTERUS_CHR.m_BlnUnderLineDST = false;
            this.m_dtcUTERUS_CHR.MappingName = "UTERUS_CHR";
            this.m_dtcUTERUS_CHR.Width = 60;
            // 
            // m_dtcBLOODED_CHR
            // 
            this.m_dtcBLOODED_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBLOODED_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBLOODED_CHR.m_BlnGobleSet = true;
            this.m_dtcBLOODED_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBLOODED_CHR.MappingName = "BLOODED_CHR";
            this.m_dtcBLOODED_CHR.Width = 55;
            // 
            // m_dtcBREAKWATER_CHR
            // 
            this.m_dtcBREAKWATER_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBREAKWATER_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBREAKWATER_CHR.m_BlnGobleSet = true;
            this.m_dtcBREAKWATER_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBREAKWATER_CHR.MappingName = "BREAKWATER_CHR";
            this.m_dtcBREAKWATER_CHR.Width = 55;
            // 
            // m_dtcEMBRYO_CHR
            // 
            this.m_dtcEMBRYO_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcEMBRYO_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcEMBRYO_CHR.m_BlnGobleSet = true;
            this.m_dtcEMBRYO_CHR.m_BlnUnderLineDST = false;
            this.m_dtcEMBRYO_CHR.MappingName = "EMBRYO_CHR";
            this.m_dtcEMBRYO_CHR.Width = 55;
            // 
            // m_dtcUTERUSSIZE_CHR
            // 
            this.m_dtcUTERUSSIZE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcUTERUSSIZE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcUTERUSSIZE_CHR.m_BlnGobleSet = true;
            this.m_dtcUTERUSSIZE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcUTERUSSIZE_CHR.MappingName = "UTERUSSIZE_CHR";
            this.m_dtcUTERUSSIZE_CHR.Width = 55;
            // 
            // m_dtcSIGNNAME_CHR
            // 
            this.m_dtcSIGNNAME_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcSIGNNAME_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSIGNNAME_CHR.m_BlnGobleSet = true;
            this.m_dtcSIGNNAME_CHR.m_BlnUnderLineDST = false;
            this.m_dtcSIGNNAME_CHR.MappingName = "SIGNNAME_CHR";
            this.m_dtcSIGNNAME_CHR.Width = 150;
            // 
            // m_dtcBODYTEMPARTURE_CHR
            // 
            this.m_dtcBODYTEMPARTURE_CHR.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_dtcBODYTEMPARTURE_CHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBODYTEMPARTURE_CHR.m_BlnGobleSet = true;
            this.m_dtcBODYTEMPARTURE_CHR.m_BlnUnderLineDST = false;
            this.m_dtcBODYTEMPARTURE_CHR.MappingName = "BODYTEMPARTURE_CHR";
            this.m_dtcBODYTEMPARTURE_CHR.Width = 65;
            // 
            // frmPostartumSeeRecord
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(814, 657);
            this.Name = "frmPostartumSeeRecord";
            this.Text = "��������������۲��¼";
            this.Load += new System.EventHandler(this.frmPostartumSeeRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		protected override Font m_FntHeaderFont
		{
			get
			{
				return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			}
		}

		// ��ʼ���������DataTable��(��Ҫ�Ķ�)
		// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ����ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate
		protected override void m_mthInitDataTable(DataTable p_dtbRecordTable)
		{

			//��ż�¼ʱ����ַ���
			p_dtbRecordTable.Columns.Add("RecordDate");//0
			//��ż�¼���͵�intֵ
			DataColumn dcRecordType = new DataColumn("RecordType",typeof(int));
			p_dtbRecordTable.Columns.Add(dcRecordType);//1
			//��ż�¼��OpenDate�ַ���
			p_dtbRecordTable.Columns.Add("OpenDate");  //2
			//��ż�¼��ModifyDate�ַ���
			p_dtbRecordTable.Columns.Add("ModifyDate"); //3

			DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDate_chr");//4
			dc1.DefaultValue = "";
			DataColumn dc2 = p_dtbRecordTable.Columns.Add("Time_chr");//5
			dc2.DefaultValue = "";


			//			p_dtbRecordTable.Columns.Add("RecordDate_chr",typeof(clsDSTRichTextBoxValue));//4
			//			p_dtbRecordTable.Columns.Add("Time_chr",typeof(clsDSTRichTextBoxValue));//5
			p_dtbRecordTable.Columns.Add("BLOODPRESSURE_CHR",typeof(clsDSTRichTextBoxValue));//6
					
			p_dtbRecordTable.Columns.Add("BODYTEMPARTURE_CHR",typeof(clsDSTRichTextBoxValue));//7

			p_dtbRecordTable.Columns.Add("PULSE_CHR",typeof(clsDSTRichTextBoxValue));//8
			p_dtbRecordTable.Columns.Add("UTERUS_CHR",typeof(clsDSTRichTextBoxValue));//9
			p_dtbRecordTable.Columns.Add("BLOODED_CHR",typeof(clsDSTRichTextBoxValue));//10
			p_dtbRecordTable.Columns.Add("BREAKWATER_CHR",typeof(clsDSTRichTextBoxValue));//11
			p_dtbRecordTable.Columns.Add("EMBRYO_CHR",typeof(clsDSTRichTextBoxValue));//12	
			p_dtbRecordTable.Columns.Add("UTERUSSIZE_CHR",typeof(clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("SIGNNAME_CHR", typeof(clsDSTRichTextBoxValue));//14

            p_dtbRecordTable.Columns.Add("RecordAndDetailSign");//15
            p_dtbRecordTable.Columns.Add("CreateUserID");//16
			
			

			//			m_dtcGenaralInstance.m_RtbBase.m_BlnReadOnly = true;
			m_mthSetControl(m_dtcRecordDate_chr);
			m_mthSetControl(m_dtcTime_chr);

			m_mthSetControl(m_dtcBLOODPRESSURE_CHR);
			m_mthSetControl(m_dtcBODYTEMPARTURE_CHR);

			m_mthSetControl(m_dtcPULSE_CHR);
			m_mthSetControl(m_dtcUTERUS_CHR);
			m_mthSetControl(m_dtcBLOODED_CHR);
			m_mthSetControl(m_dtcBREAKWATER_CHR);
			m_mthSetControl(m_dtcEMBRYO_CHR);
			m_mthSetControl(m_dtcUTERUSSIZE_CHR);
			m_mthSetControl(m_dtcSIGNNAME_CHR);

			
			//����������
			this.m_dtcRecordDate_chr.HeaderText = "\r\n��\r\n\r\n\r\n\r\n��";
			this.m_dtcTime_chr.HeaderText = "\r\nʱ\r\n\r\n\r\n\r\n��";
			this.m_dtcBLOODPRESSURE_CHR.HeaderText = "\r\nѪ\r\n\r\n\r\n\r\nѹ";

			this.m_dtcBODYTEMPARTURE_CHR.HeaderText = "\r\n��\r\n\r\n\r\n\r\n��";


			this.m_dtcPULSE_CHR.HeaderText = "\r\n��\r\n\r\n\r\n\r\n��";
			this.m_dtcUTERUS_CHR.HeaderText = "\r\n��\r\n\r\n\r\n\r\n��";
			this.m_dtcBLOODED_CHR.HeaderText = "\r\n��\r\n\r\n\r\n\r\nѪ";
			this.m_dtcBREAKWATER_CHR.HeaderText = "\r\n��\r\n\r\n\r\n\r\nˮ";
			this.m_dtcEMBRYO_CHR.HeaderText = "\r\n̥\r\n\r\n\r\n\r\n��";
			this.m_dtcUTERUSSIZE_CHR.HeaderText = "��\r\n\r\n��\r\n\r\n��\r\n\r\nС";
			this.m_dtcSIGNNAME_CHR.HeaderText = "\r\nǩ\r\n\r\n\r\n\r\n��";

		}

		#region ����
		/// <summary>
		/// ��ǰ��Ժʱ��
		/// </summary>
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

		//(��Ҫ�Ķ�)
		protected override enmApproveType m_EnmAppType
		{
			get{return enmApproveType.Nurses;}
		}
		/// <summary>
		/// ��¼��ID?
		/// </summary>
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

		/// <summary>
		/// ��ȡ�ۼ�����
		/// </summary>
		/// <param name="p_strText"></param>
		/// <param name="p_strModifyUserID"></param>
		/// <param name="p_strModifyUserName"></param>
		/// <returns></returns>
		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}

		// ��ȡ���̼�¼�������ʵ��(��Ҫ�Ķ�)
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.PostartumSeeRecord);
		}

		// ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsTrackRecordContent
			clsTrackRecordContent objContent = null;
			//(��Ҫ�Ķ�)
			switch((enmDiseaseTrackType)p_intRecordType)
			{ 
				case enmDiseaseTrackType.PostartumSeeRecord:
					objContent = new clsIcuACAD_PostPartumseeRecord_VO();//(��Ҫ�Ķ�)
					break;
			}

			if(objContent == null)
				objContent=new clsIcuACAD_PostPartumseeRecord_VO();	//(��Ҫ�Ķ�)
		
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

            objContent.m_strCreateUserID = (string)p_objDataArr[16];
		
			return objContent;
		}

		private void frmPostartumSeeRecord_Load(object sender, System.EventArgs e)
		{
			m_dtmPreRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.Focus();
			m_mniAddBlank.Visible=false;
			m_mniDeleteBlank.Visible=false;

//			m_txtDiseaseID.ReadOnly = true;
//			m_dtpCreateDate.ReadOnly = true;
//		
		}

		#region �Բ�������εĴ���
//
//		private void m_mthget()
//		{
//			string p_strBEFOREHAND_CHR = "";
//			string p_strLAYCOUNT_CHR = "";
//			if(this.m_trvInPatientDate.SelectedNode != null)
//			{
//				string date = this.m_trvInPatientDate.SelectedNode.Text.ToString().Trim();
//				string InPatientID = this.txtInPatientID.Text.Trim();
//				if(InPatientID != "")
//				{
//					com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService domin = new com.digitalwave.clsRecordsService.clsWaitLayRecord_AcadService();
//					domin.m_lngGetBEFOREHAND_CHR_LAYCOUNT_CHR(InPatientID,date,out p_strBEFOREHAND_CHR, out p_strLAYCOUNT_CHR );
//					m_txtDiseaseID.Text = p_strLAYCOUNT_CHR;  //����
//					m_dtpCreateDate.Value = Convert.ToDateTime(p_strBEFOREHAND_CHR);
//				}
//			}
//		}
		#endregion 
		// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.PostartumSeeRecord://(��Ҫ�Ķ�)
					
				{
					frmPostartumSeeRecordCon frmwcon = new frmPostartumSeeRecordCon();
					//frmwcon.m_dtmBEFOREHAND_CHR = m_dtpCreateDate.Value;
					//frmwcon.m_strLAYCOUNT_CHR =  m_txtDiseaseID.Text;//����
					//frmwcon.m_setLaycout();
					return  frmwcon;//(��Ҫ�Ķ�)
					break;
				}
			}  
		
			return null;
		}

		/// <summary>
		/// �����Ӵ���
		/// </summary>
		/// <param name="p_frmSubForm"></param>
		protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
		{
			//m_mthSetPatientFormInfo(m_objCurrentPatient);

            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
		}
		/// <summary>
		/// ��Tableɾ������
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_dtmCreateRecordTime"></param>
    		protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{
			//m_mthSetPatientFormInfo(m_objCurrentPatient);

            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
		}

		/// <summary>
		/// ��ȡ��ǰ���˵���������
		/// </summary>
		/// <param name="p_dtmRecordDate">��¼����</param>
		/// <param name="p_intFormID">����ID</param>
		protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate,int p_intFormID)
		{			
			m_mthGetDeletedRecord(p_intFormID,p_dtmRecordDate); 
		}

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

		protected override void m_mthClearPatientRecordInfo()
		{
			m_mthSetDataGridFirstRowFocus();
			m_dtgRecordDetail.CurrentRowIndex = 0;
			m_dtbRecords.Rows.Clear();
			//��ռ�¼����                       
			m_mthClearRecordInfo();
		}

		private void mniAppend_Click(object sender, System.EventArgs e)
		{
			m_mthAddNewRecord((int)enmDiseaseTrackType.PostartumSeeRecord);//(��Ҫ�Ķ�)
		}

		protected override infPrintRecord m_objGetPrintTool()
		{			
			clsPostartumSeeRecordPrintTool pt = new clsPostartumSeeRecordPrintTool();
//			pt.m_strLaycount = m_txtDiseaseID.Text ;//����
//			pt.m_strBirthDate = m_dtpCreateDate.Value.Date.ToShortDateString();
			return pt;
		}

		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			#region ��ʾ��¼��DataGrid
			try
			{

//				#region �������
//				m_txtDiseaseID.Text = "";				
//				//m_dtpCreateDate.Value = System.DateTime.Now;
//				#endregion

				if(p_objTransDataInfo == null)
					return null;

				object[] objData;
				ArrayList objReturnData=new ArrayList();

				clsICUACAD_POSTPARTUMSEERECORDContentDataInfo objICUInfo=new clsICUACAD_POSTPARTUMSEERECORDContentDataInfo();	//(��Ҫ�Ķ�)		
				clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
				string strText,strXml;

				objICUInfo = (clsICUACAD_POSTPARTUMSEERECORDContentDataInfo)p_objTransDataInfo;//(��Ҫ�Ķ�)

				if(objICUInfo.m_objRecordArr == null)
					return null;

				int intRecordCount = objICUInfo.m_objRecordArr.Length;
				int intRowOfCurrentDetail = 0;

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

				for(int i=0; i<intRecordCount; i++)
				{
					objData = new object[17];   //(��Ҫ�Ķ�) DataTable������
					clsIcuACAD_PostPartumseeRecord_VO objCurrent = objICUInfo.m_objRecordArr[i];//(��Ҫ�Ķ�)
                    clsIcuACAD_PostPartumseeRecord_VO objNext = new clsIcuACAD_PostPartumseeRecord_VO();//��һ����¼//(��Ҫ�Ķ�)
					if(i < intRecordCount-1)
						objNext = objICUInfo.m_objRecordArr[i+1];

                    ////����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ�����ʾ
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate)
                    //{
                    //    TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
                    //    if((int)tsModify.TotalHours < intCanModifyTime)
                    //        continue;
                    //}


                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
                    {
                        TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                        if ((int)tsModify.TotalHours < intCanModifyTime)
                        {
                           // blnPreIsHide = true;
                            continue;
                        }
                    }


					#region ��Źؼ��ֶ�
					if(objCurrent.m_dtmCreateDate!=DateTime.MinValue)
					{
						objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
						objData[1] = (int)enmRecordsType.PostartumSeeRecord;//��ż�¼���͵�intֵ  //(��Ҫ�Ķ�)
                        objData[2] = objCurrent.m_dtmCreateDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���,�����Ǻۼ�ʱ
                        //objData[3] = objICUInfo.m_objRecordArr[objICUInfo.m_objRecordArr.Length-1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
						
						//ͬһ����ֻ�ڵ�һ����ʾ����
						if(objCurrent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())//m_dtmRECORDDATE
						{
                            objData[4] = objCurrent.m_dtmRecordDate.Date.ToString("yyyy-MM-dd"); ;//�����ַ���
						}
						//�޸ĺ���кۼ��ļ�¼������ʾʱ��
						if(m_dtmPreRecordDate != objCurrent.m_dtmCreateDate)
                            objData[5] = objCurrent.m_dtmRecordDate.ToString("HH:mm");//ʱ���ַ���
					}
                    m_dtmPreRecordDate = objCurrent.m_dtmCreateDate;	
					#endregion ;

					
//					//���δ���
//					m_txtDiseaseID.Text = objCurrent.m_strLayCount_chr;
//					if(objCurrent.m_strBeforehand_chr.Trim() != "")
//						m_dtpCreateDate.Value = Convert.ToDateTime(objCurrent.m_strBeforehand_chr.ToString().Trim());
//					//

					#region ��ŵ�����Ϣ
					//Ѫѹ
					strText = objCurrent.m_strBLOODPRESSURE_CHR_RIGHT;
					strXml = "<root />";
					if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBLOODPRESSURE_CHR_RIGHT != objCurrent.m_strBLOODPRESSURE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
					{
						strXml = m_strGetDSTTextXML(objCurrent.m_strBLOODPRESSURE_CHR_RIGHT ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
					}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[6] = objclsDSTRichTextBoxValue;//Ѫѹ

					//����
					strText = objCurrent.m_strBODYTEMPARTURE_CHR_RIGHT;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBODYTEMPARTURE_CHR_RIGHT != objCurrent.m_strBODYTEMPARTURE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strBODYTEMPARTURE_CHR_RIGHT ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
					objData[7] = objclsDSTRichTextBoxValue;//����

			
					//����
					strText = objCurrent.m_strPULSE_CHR_RIGHT ;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strPULSE_CHR_RIGHT != objCurrent.m_strPULSE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strPULSE_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[8] = objclsDSTRichTextBoxValue;//����

					//����
					strText = objCurrent.m_strUTERUS_CHR_RIGHT;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strUTERUS_CHR_RIGHT != objCurrent.m_strUTERUS_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strUTERUS_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[9] = objclsDSTRichTextBoxValue;//����

			
					//��Ѫ
					strText = objCurrent.m_strBLOODED_CHR_RIGHT;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBLOODED_CHR_RIGHT != objCurrent.m_strBLOODED_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strBLOODED_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[10] = objclsDSTRichTextBoxValue;//��Ѫ
			
					//��ˮ
					strText = objCurrent.m_strBREAKWATER_CHR_RIGHT ;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strBREAKWATER_CHR_RIGHT != objCurrent.m_strBREAKWATER_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strBREAKWATER_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[11] = objclsDSTRichTextBoxValue;//��ˮ

					//̥��
					strText = objCurrent.m_strEMBRYO_CHR_RIGHT ;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strEMBRYO_CHR_RIGHT != objCurrent.m_strEMBRYO_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strEMBRYO_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[12] = objclsDSTRichTextBoxValue;//̥��

					//���ڴ�С
					strText = objCurrent.m_strUTERUSSIZE_CHR_RIGHT ;
					strXml = "<root />";
                    //if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strUTERUSSIZE_CHR_RIGHT != objCurrent.m_strUTERUSSIZE_CHR_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    //{
                    //    strXml = m_strGetDSTTextXML(objCurrent.m_strUTERUSSIZE_CHR_RIGHT,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                    //}
					objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
					objclsDSTRichTextBoxValue.m_strText=strText;						
					objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
					objData[13] = objclsDSTRichTextBoxValue;//���ڴ�С

                    //ǩ��
                    if (objCurrent.objSignerArr != null || objCurrent.objSignerArr.Length > 0)
                    {
                        string str = string.Empty;
                        if(objCurrent.objSignerArr[0].objEmployee != null)
                            str = objCurrent.objSignerArr[0].objEmployee.m_strGetTechnicalRankAndName;
                        for (int w1 = 1 ; w1 < objCurrent.objSignerArr.Length ; w1++)
                        {
                            if(objCurrent.objSignerArr[w1].objEmployee != null)
                                str += ";"+objCurrent.objSignerArr[w1].objEmployee.m_strGetTechnicalRankAndName;
                        }
					    objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = str;						
					    objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
                        objData[14] = objclsDSTRichTextBoxValue;//ǩ��
                    }

                    //
                    objData[15] = objCurrent.m_strRecordUserID;//
                    objData[16] = objCurrent.m_strCreateUserID;//

					objReturnData.Add(objData);
					#endregion
				}
				object[][] m_objRe=new object[objReturnData.Count][];

				for(int m=0;m<objReturnData.Count ;m++)
					m_objRe[m]=(object[])objReturnData[m];
				return m_objRe;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message );
				return null;
			}
			#endregion
		}

        protected override void m_mthGetTransDataInfoArr(out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            m_objRecordsDomain.m_lngGetTransDataInfoArr(m_objCurrentPatient.m_StrRegisterId, out p_objTansDataInfoArr);

        }

	}
}
