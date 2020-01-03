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
	/// Σ�ػ����¼(�°�) ��ժҪ˵����
	/// </summary>
	public class frmIntensiveTendMain_FC  : iCare.frmRecordsBase
	{
		private System.Windows.Forms.PictureBox m_picExpand;
		private cltDataGridDSTRichTextBox m_dtcTemperature;
		private cltDataGridDSTRichTextBox m_dtcPulse;
		private cltDataGridDSTRichTextBox m_dtcBreath;
		private cltDataGridDSTRichTextBox m_dtcMind;
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
        private cltDataGridDSTRichTextBox m_dtcBloodOxygenSaturation;
		private System.Windows.Forms.DataGridTextBoxColumn clmSign;
		private cltDataGridDSTRichTextBox m_dtcContent;
		private System.Windows.Forms.DataGridTextBoxColumn clmCreateTime;
		private System.Windows.Forms.DataGridTextBoxColumn clmCreateDateofDay;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components = null;
		private string m_strCurrentOpenDate = "";
		private string m_strCreateUserID = "";
		private DataTable dtTempTable;                         //���ڹ���datagrid���
		private string strCurrentClass;                        //��ǰ���Ĭ��Ϊ��
        private int intCaseRowCount;                           //��ǰ���̼�¼���������
		private string[] strCurrentCaseTextArr;                //��ǰ���̼�¼��������
		private string[] strCurrentCaseXmlArr;                 //��ǰ���̼�¼�ۼ�����
		private string[] strCurrentCaseCreateDateArr;          //��ǰ���̼�¼����ʱ������
        DateTime dtContentRecordDate = DateTime.MinValue;
		public frmIntensiveTendMain_FC()
		{
			try
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
				m_dtcInID.m_BlnGobleSet=false;
				m_dtcIn_Qty.m_BlnGobleSet=false;
				m_dtcOutID.m_BlnGobleSet=false;
				m_dtcOut_Qty.m_BlnGobleSet=false;
                //imgUserclose = new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "CLSDFOLD.ICO");
                //imgUseropen= new Bitmap(m_strGetFilePathHeader()+"picture\\"+ "OPENFOLD.ICO");
				
				m_blnIsExpand=true;
                //this.m_picExpand.Image=imgUseropen;	
				dtTempTable = new DataTable("RecordDetail");
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message);
			}
		}

        //private Bitmap imgUserclose;
        //private Bitmap imgUseropen;

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
            this.m_picExpand = new System.Windows.Forms.PictureBox();
            this.m_dtcTemperature = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPulse = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBreath = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMind = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
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
            this.m_dtcBloodOxygenSaturation = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.clmSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcContent = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.clmCreateDateofDay = new System.Windows.Forms.DataGridTextBoxColumn();
            this.clmCreateTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
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
																										 this.m_dtcMind,
																										 this.m_dtcBloodPressureS,
																										 this.m_dtcBloodPressureA,
																										 this.m_dtcPupilLeft,
																										 this.m_dtcPupilRight,
																										 this.m_dtcEchoLeft,
																										 this.m_dtcEchoRight,
                                                                                                          this.m_dtcBloodOxygenSaturation,
																										 this.m_dtcInID,
																										 this.m_dtcIn_Qty,
																										 this.m_dtcOutID,
																										 this.m_dtcOut_Qty,//this.clmSign,
																										 this.m_dtcContent});

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
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(12, 76);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(776, 536);
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
            this.m_trvInPatientDate.Location = new System.Drawing.Point(126, 151);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(160, 52);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(212, 158);
            this.lblSex.Size = new System.Drawing.Size(41, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(230, 144);
            this.lblAge.Size = new System.Drawing.Size(80, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(219, 161);
            this.lblBedNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(230, 144);
            this.lblInHospitalNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(171, 151);
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(240, 151);
            this.lblSexTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(266, 138);
            this.lblAgeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(192, 151);
            this.lblAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(222, 138);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(64, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(205, 168);
            this.txtInPatientID.Size = new System.Drawing.Size(64, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(215, 171);
            this.m_txtPatientName.Size = new System.Drawing.Size(108, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(205, 154);
            this.m_txtBedNO.Size = new System.Drawing.Size(48, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(240, 161);
            this.m_cboArea.Size = new System.Drawing.Size(112, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(178, 138);
            this.m_lsvPatientName.Size = new System.Drawing.Size(108, 104);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(238, 129);
            this.m_lsvBedNO.Size = new System.Drawing.Size(48, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(205, 180);
            this.m_cboDept.Size = new System.Drawing.Size(112, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(192, 180);
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(358, 162);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(233, 152);
            this.m_cmdNext.Size = new System.Drawing.Size(16, 23);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(219, 158);
            this.m_cmdPre.Size = new System.Drawing.Size(36, 21);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblForTitle.Font = new System.Drawing.Font("����", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblForTitle.Location = new System.Drawing.Point(197, 187);
            this.m_lblForTitle.Size = new System.Drawing.Size(16, 4);
            this.m_lblForTitle.Text = "Σ�ػ����¼";
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(391, 128);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(720, 36);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(4, 7);
            this.m_pnlNewBase.Size = new System.Drawing.Size(792, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(790, 29);
            // 
            // m_picExpand
            // 
            this.m_picExpand.Location = new System.Drawing.Point(266, 149);
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
            // m_dtcMind
            // 
            this.m_dtcMind.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMind.m_BlnGobleSet = true;
            this.m_dtcMind.m_BlnUnderLineDST = false;
            this.m_dtcMind.MappingName = "Mind";
            this.m_dtcMind.Width = 50;
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
            // m_dtcBloodOxygenSaturation
            // 
            this.m_dtcBloodOxygenSaturation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodOxygenSaturation.m_BlnGobleSet = true;
            this.m_dtcBloodOxygenSaturation.m_BlnUnderLineDST = false;
            this.m_dtcBloodOxygenSaturation.MappingName = "BloodOxygenSaturation";
            this.m_dtcBloodOxygenSaturation.Width = 50;
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
            this.m_dtcContent.Width = 350;
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
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(4, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 552);
            this.panel1.TabIndex = 10000004;
            // 
            // frmIntensiveTendMain_FC
            // 
            this.AccessibleDescription = "Σ�ػ����¼";
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(800, 673);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_picExpand);
            this.Name = "frmIntensiveTendMain_FC";
            this.Text = "Σ�ػ����¼";
            this.Load += new System.EventHandler(this.frmIntensiveTendMain_Load);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_picExpand, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).EndInit();
            this.m_pnlNewBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picExpand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


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
			p_dtbRecordTable.Columns.Add("BloodPressureS",typeof(clsDSTRichTextBoxValue));//10
			p_dtbRecordTable.Columns.Add("PupilLeft",typeof(clsDSTRichTextBoxValue));//11
			p_dtbRecordTable.Columns.Add("PupilRight",typeof(clsDSTRichTextBoxValue));//12
			p_dtbRecordTable.Columns.Add("EchoLeft",typeof(clsDSTRichTextBoxValue));//13
			p_dtbRecordTable.Columns.Add("EchoRight",typeof(clsDSTRichTextBoxValue));//14
			p_dtbRecordTable.Columns.Add("InID",typeof(clsDSTRichTextBoxValue));//15
			p_dtbRecordTable.Columns.Add("In_Qty",typeof(clsDSTRichTextBoxValue));//16
			p_dtbRecordTable.Columns.Add("OutID",typeof(clsDSTRichTextBoxValue));//17
			p_dtbRecordTable.Columns.Add("Out_Qty",typeof(clsDSTRichTextBoxValue));//18	
			p_dtbRecordTable.Columns.Add("Content",typeof(clsDSTRichTextBoxValue)); //19
            p_dtbRecordTable.Columns.Add("Sign");//20			
			p_dtbRecordTable.Columns.Add("Mind",typeof(clsDSTRichTextBoxValue));//21
            p_dtbRecordTable.Columns.Add("ContentCreateDate");//22
            p_dtbRecordTable.Columns.Add("CreateUserID");//23
            p_dtbRecordTable.Columns.Add("BloodOxygenSaturation", typeof(clsDSTRichTextBoxValue));//24


			m_dtcContent.m_RtbBase.m_BlnReadOnly = true;
			m_mthSetControl(clmCreateDateofDay);
			m_mthSetControl(clmCreateTime);
			m_mthSetControl(m_dtcTemperature);
			m_mthSetControl(m_dtcPulse);
			m_mthSetControl(m_dtcBreath);
			m_mthSetControl(m_dtcMind);
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
            m_mthSetControl(m_dtcBloodOxygenSaturation);
			//����������
			this.clmCreateDateofDay.HeaderText = "\r\n\r\n����";
			this.clmCreateTime.HeaderText = "\r\n\r\nʱ��";
			this.m_dtcTemperature.HeaderText = "��\r\n\r\n��\r\n\r\n��";
			this.m_dtcPulse.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcBreath.HeaderText = "��\r\n��\r\n��\r\n/\r\n��";
			this.m_dtcMind.HeaderText = "��\r\n\r\n־\r\n";
            this.m_dtcBloodPressureA.HeaderText =  "��\r\n��\r\nѹ\r\n\r\nmmHg";
            this.m_dtcBloodPressureS.HeaderText = "��\r\n��\r\nѹ\r\n\r\nmmHg";
			this.m_dtcPupilLeft.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n(mm)\r\n��";
			this.m_dtcPupilRight.HeaderText = "ͫ\r\n��\r\n��\r\nС\r\n(mm)\r\n��";
			this.m_dtcEchoLeft.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";	
			this.m_dtcEchoRight.HeaderText = "ͫ\r\n��\r\n��\r\n��\r\n��";
            this.m_dtcBloodOxygenSaturation.HeaderText = "Ѫ\r\n��\r\n��\r\n��\r\n��\r\n%";
			this.m_dtcInID.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcIn_Qty.HeaderText = "��\r\n��\r\n��\r\n(ml)";
			this.m_dtcOutID.HeaderText = "��\r\n��\r\n��\r\n��";
			this.m_dtcOut_Qty.HeaderText = "��\r\n��\r\n��\r\n(ml)";
            this.clmSign.HeaderText = "ǩ\r\n\r\n\r\n��";
			this.m_dtcContent.HeaderText = "\r\n\r\n ���顢�����ʩ��Ч����ǩ��";
//			this.clmSign.HeaderText = "\r\n\r\nǩ��";	
		
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
			object[] objData;
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			objDataInfo = (clsIntensiveTendDataInfo)p_objTransDataInfo;
			//û��ͳ������ʱ����ʾͳ�Ƶ�����
			if(!(objDataInfo.m_objItemSummary.m_intTotal_In != 0 || objDataInfo.m_objItemSummary.m_intTotal_Out != 0 ))
			{
				return null;
			}
			objData = new object[25]; 
			#region DSL���
			m_strCreateUserID=objDataInfo.m_objRecordContent.m_strCreateUserID;
			m_strCurrentOpenDate=objDataInfo.m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
			#endregion DSL���
            strText = "24Сʱ";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[13] = objclsDSTRichTextBoxValue;

			strText = "����";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[14] = objclsDSTRichTextBoxValue;

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[24] = objclsDSTRichTextBoxValue;
		
			strText = "����";
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[15] = objclsDSTRichTextBoxValue;
		
			strText = objDataInfo.m_objItemSummary.m_intTotal_In.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[16] = objclsDSTRichTextBoxValue;//��ʳ��������λ��
		
			strText = "�ų�";
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[17] = objclsDSTRichTextBoxValue;
		
			strText = objDataInfo.m_objItemSummary.m_intTotal_Out.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[18] = objclsDSTRichTextBoxValue;
		
			object [][] objDataReturn=new object[1][];
			objDataReturn[0]=objData;
			return objDataReturn;
			//�жϵ�ǰ��εĲ��̼�¼�Ƿ�������,�ۼ���������
            //while(intCaseRowCount>0)//���̼�¼
            //{
            //    DataRow dr=m_dtbRecords.NewRow();
				
            //    strText = strCurrentCaseTextArr[strCurrentCaseTextArr.Length-intCaseRowCount];
            //    //				if(objNext != null && objNext.m_strRecordContent_Right != objCurrent.m_strRecordContent_Right)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
            //    //				{
            //    //					strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
            //    //				}
            //    //				else
            //    //				{
            //    strXml = strCurrentCaseXmlArr[strCurrentCaseXmlArr.Length-intCaseRowCount];
            //    //				}
            //    objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
            //    objclsDSTRichTextBoxValue.m_strText=strText;						
            //    objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
            //    dr["Content"]= objclsDSTRichTextBoxValue;
            //    dr["Sign"]=strCurrentCaseCreateDateArr[strCurrentCaseCreateDateArr.Length-intCaseRowCount];
            //    m_dtbRecords.Rows.Add(dr);
            //    intCaseRowCount--;
            //}
            //return objData;	
			#endregion
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
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
				if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.IntensiveTend_FC)
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
                for (int n = intSingleTypeCount -1; n < intSingleTypeCount; n++)
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
                    #region ���̼�¼���ݴ���--wf20080117
                    //���̼�¼���ݴ���
                    ArrayList arlRecordContent = new ArrayList();
                    ArrayList arlRecordContentXML = new ArrayList();
                    for (int k = 0; k < objIntesiveTendInfo.m_objCourseDiseasesRecordArr.Length; k++)
                    {
                        if (objIntesiveTendInfo.m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContent.Trim().Length != 0)
                        {
                            arlRecordContent.Add(objIntesiveTendInfo.m_objCourseDiseasesRecordArr[k].m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            arlRecordContentXML.Add("<root />");
                            string strCase = "    " +objIntesiveTendInfo.m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContent.Trim();
                            string strCaseXML = objIntesiveTendInfo.m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContentXml;
                            string[] strCaseTextArr, strCaseXmlArr;
                            com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strCase, strCaseXML, 44, out strCaseTextArr, out strCaseXmlArr);
                            for (int k1 = 0; k1 < strCaseTextArr.Length; k1++)
                            {
                                arlRecordContent.Add(strCaseTextArr[k1]);
                                arlRecordContentXML.Add(strCaseXmlArr[k1]);
                            }
                            arlRecordContent.Add("                            --" + objIntesiveTendInfo.m_objCourseDiseasesRecordArr[k].m_strModifyUserName);
                            arlRecordContentXML.Add("<root />");
                        }
                    }
                    intContent_Count = arlRecordContent.Count + 1;
                    #endregion
                    #region ����
                    /*
                        if (objIntesiveTendInfo.m_objCourseDiseasesRecordArr[n].m_strRecordContent.Trim().Length != 0)
                        {
                            string[] strCurrentCaseTextArrTemp = null;                //��ǰ���̼�¼��������(��ʱ)
                            string[] strCurrentCaseXmlArrTemp = null;                 //��ǰ���̼�¼�ۼ�����(��ʱ)
                            string[] strCurrentCaseCreateDateArrTemp = null;          //��ǰ���̼�¼����ʱ������(��ʱ)


                            if (intCaseRowCount == 0)
                            {
                                strCurrentCaseTextArr = null;
                                strCurrentCaseXmlArr = null;
                                strCurrentCaseCreateDateArr = null;
                                intCaseRowCount = 0;
                            }
                            else
                            {
                                strCurrentCaseTextArrTemp = strCurrentCaseTextArr;
                                strCurrentCaseXmlArrTemp = strCurrentCaseXmlArr;
                                strCurrentCaseCreateDateArrTemp = strCurrentCaseCreateDateArr;
                                strCurrentCaseTextArr = null;
                                strCurrentCaseXmlArr = null;
                                strCurrentCaseCreateDateArr = null;
                            }

                            string strCase = "";
                            //ֻ��ʾԭʼֵ��û����Ϊ�ա�tfzhang ���� 2005-7-19 9:57:08
                            //					if (objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent_Right.Trim().Length==0 || objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent==objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent_Right)
                            //						strCase = objIntesiveTendInfo.m_objTransDataArr[n].m_strRecordContent ;
                            //					else
                            strCase = objIntesiveTendInfo.m_objCourseDiseasesRecordArr[n].m_strRecordContent;

                            string strCaseXML = objIntesiveTendInfo.m_objCourseDiseasesRecordArr[n].m_strRecordContentXml;
                            string[] strCaseTextArr, strCaseXmlArr;
                            com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strCase, strCaseXML, 44, out strCaseTextArr, out strCaseXmlArr);
                            int intCaseCount = strCaseTextArr.Length;
                            if (intCaseRowCount > 0)//׷�ӵ�����
                            {
                                int intOldCaseCount = strCurrentCaseTextArrTemp.Length;
                                strCurrentCaseTextArr = new string[intCaseCount + intOldCaseCount + 1];
                                strCurrentCaseXmlArr = new string[intCaseCount + intOldCaseCount + 1];
                                strCurrentCaseCreateDateArr = new string[intCaseCount + intOldCaseCount + 1];
                                for (int i = 0; i < intOldCaseCount; i++)
                                {
                                    strCurrentCaseTextArr[i] = strCurrentCaseTextArrTemp[i];
                                    strCurrentCaseXmlArr[i] = strCurrentCaseXmlArrTemp[i];
                                    strCurrentCaseCreateDateArr[i] = strCurrentCaseCreateDateArrTemp[i];
                                }
                                for (int j = 0; j < intCaseCount; j++)
                                {
                                    strCurrentCaseTextArr[j + intOldCaseCount] = strCaseTextArr[j];
                                    strCurrentCaseXmlArr[j + intOldCaseCount] = strCaseXmlArr[j];
                                    strCurrentCaseCreateDateArr[j + intOldCaseCount] = objIntesiveTendInfo.m_objTransDataArr[n].m_dtContentCreateDate.ToString();
                                }
                                //���ǩ��
                                strCurrentCaseTextArr[intCaseCount + intOldCaseCount] = "                          --" + objIntesiveTendInfo.m_objTransDataArr[n].m_strContentModifyUserName;
                                strCurrentCaseXmlArr[intCaseCount + intOldCaseCount] = strCaseXmlArr[intCaseCount - 1];
                                strCurrentCaseCreateDateArr[intCaseCount + intOldCaseCount] = objIntesiveTendInfo.m_objTransDataArr[n].m_dtContentCreateDate.ToString();

                                //���̼�¼�����
                                intCaseRowCount = intCaseRowCount + intCaseCount + 1;
                                intContent_Count = intCaseRowCount;
                            }
                            else
                            {
                                strCurrentCaseTextArr = new string[intCaseCount + 1];
                                strCurrentCaseXmlArr = new string[intCaseCount + 1];
                                strCurrentCaseCreateDateArr = new string[intCaseCount + 1];
                                for (int i = 0; i < intCaseCount; i++)
                                {
                                    strCurrentCaseTextArr[i] = strCaseTextArr[i];
                                    strCurrentCaseXmlArr[i] = strCaseXmlArr[i];
                                    strCurrentCaseCreateDateArr[i] = objIntesiveTendInfo.m_objTransDataArr[n].m_dtContentCreateDate.ToString();
                                }
                                //���ǩ��
                                strCurrentCaseTextArr[intCaseCount] = "                          --" + objIntesiveTendInfo.m_objTransDataArr[n].m_strContentModifyUserName;
                                strCurrentCaseXmlArr[intCaseCount] = strCaseXmlArr[intCaseCount - 1];
                                strCurrentCaseCreateDateArr[intCaseCount] = objIntesiveTendInfo.m_objTransDataArr[n].m_dtContentCreateDate.ToString();
                                //���̼�¼�����
                                intCaseRowCount = intCaseCount + 1;
                                intContent_Count = intCaseCount + 1;
                            }

                            strCurrentClass = objIntesiveTendInfo.m_objTransDataArr[n].m_strClass;
                        }
                    */
#endregion
                    int intMaxCount = 1;
					if(intMaxCount<intIn_Count)
						intMaxCount=intIn_Count;
					if(intMaxCount<intOut_Count)
						intMaxCount=intOut_Count;
                    if (intMaxCount < intContent_Count)
                        intMaxCount = intContent_Count;
					if(intMaxCount == 0)
						intMaxCount = 1;
					#endregion
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
					bool blnIsShowTime = false;
				
					#region Ϊ���鸳ֵ
					///�����ѭ�������ɵ����ݽ���ʾ��DataGrid��,����һ����¼����������DataGrid����ʾ�ж���
					for(int i=0;i<intMaxCount;i++)
					{
						objData = new object[25];
                        #region ���Σ�ֻ��ʾ���һ���޸ļ�¼�������޸����봴�����Ƿ�ͬһ�˼������Ƿ����޸�ʱ����
                        /*
						clsIntensiveTendRecordContent1 objCurrent = (n<intSingleTypeCount)?objIntesiveTendInfo.m_objTransDataArr[n]:null;
						clsIntensiveTendRecordContent1 objNext = (n>= intSingleTypeCount-1)?null:objIntesiveTendInfo.m_objTransDataArr[n+1];
			
						//����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
						if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strModifyUserID.Trim()==objCurrent.m_strCreateUserID.Trim())
						{
							if(i == 0)
								blnIsShowTime = true;
							TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
							if((int)tsModify.TotalHours < intCanModifyTime)
								continue;
						}
                        */
                        #endregion
                        #region ��Źؼ��ֶ�
                        if (i==0)
						{
                            if (objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate != DateTime.MinValue)
							{
								//ֻ�ڵ�һ�м�¼����������Ϣ
                                objData[0] = objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
								objData[1] = (int)enmRecordsType.IntensiveTend_FC;//��ż�¼���͵�intֵ
                                objData[2] = objIntesiveTendInfo.m_objRecordContent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                                objData[3] = objIntesiveTendInfo.m_objRecordContent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   

                                if (objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
								{
                                    objData[4] = objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate.Date.ToString("yyyy-MM-dd 00:00:00");//�����ַ���
								}
                                if (objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate.ToString("HH:mm") != m_dtmPreRecordDate.ToString("HH:mm"))
                                {
                                    objData[5] = objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
                                }
		
							}
                            //objData[22] = objIntesiveTendInfo.m_objCourseDiseasesRecordArr[0].m_dtmCreateDate.ToString();//��Ų��̼�¼���ݴ���ʱ����ַ���
                            m_dtmPreRecordDate = objIntesiveTendInfo.m_objRecordContent.m_dtmCreateDate;
						} 		
						#endregion ;

						#region ��ŵ�����Ϣ
						if(i == 0)
						{
							//����
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strTemperature;
							strXml = "<root />";
                            //if (objNext != null && objNext.m_strTemperature != objIntesiveTendInfo.m_objRecordContent.m_strTemperature)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strTemperature ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[6] = objclsDSTRichTextBoxValue;//T
				
							//����
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strPulse;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strPulse != objCurrent.m_strPulse)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strPulse,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[7] = objclsDSTRichTextBoxValue;//HR
	
							//����
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strBreath;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strBreath!= objCurrent.m_strBreath)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strBreath,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[8] = objclsDSTRichTextBoxValue;//P

							//��־
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strMind;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strMind!= objCurrent.m_strMind)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strMind,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[21] = objclsDSTRichTextBoxValue;//P
				
							//ѪѹA
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strBloodPressureA;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strBloodPressureA != objCurrent.m_strBloodPressureA)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureA,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[9] = objclsDSTRichTextBoxValue;//
				
							//ѪѹS
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strBloodPressureS;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strBloodPressureS != objCurrent.m_strBloodPressureS)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strBloodPressureS,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[10] = objclsDSTRichTextBoxValue;//
				
							//��ͫ
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strPupilLeft;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strPupilLeft != objCurrent.m_strPupilLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strPupilLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[11] = objclsDSTRichTextBoxValue;//
				
							//��ͫ
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strPupilRight;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strPupilRight != objCurrent.m_strPupilRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strPupilRight ,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[12] = objclsDSTRichTextBoxValue;
				
							//����  ��
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strEchoLeft;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strEchoLeft != objCurrent.m_strEchoLeft)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strEchoLeft,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[13] = objclsDSTRichTextBoxValue;
				
							//����  ��
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strEchoRight;
							strXml = "<root />";
                            //if(objNext != null && objNext.m_strEchoRight != objCurrent.m_strEchoRight)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strEchoRight,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
							objData[14] = objclsDSTRichTextBoxValue;//
                            //Ѫ�����Ͷ�
                            strText = objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strBloodOxygenSaturation;
                            strXml = "<root />";
                            //if (objNext != null && objNext.m_strBloodOxygenSaturation != objCurrent.m_strBloodOxygenSaturation)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //{
                            //    strXml = m_strGetDSTTextXML(objCurrent.m_strBloodOxygenSaturation, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                            //}
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[24] = objclsDSTRichTextBoxValue;
		
						}
						else
						{
							//����ֵ
						}
						#endregion ;

						#region ��Ŷ�����Ϣ
						#region ��ʳ
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
                            strXml = ((string[])objIn[i])[2];
                            //switch (((string[])objIn[i])[0])
                            //{
                            //    case "D":
                            //        if(objNext != null && objNext.m_intInD != objCurrent.m_intInD)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml = ((string[])objIn[i])[2];
                            //        }
                            //        break;
                            //    case "I":
                            //        if(objNext != null && objNext.m_intInI != objCurrent.m_intInI)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml = ((string[])objIn[i])[2];
                            //        }
                            //        break;
                            //}
							
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[16] = objclsDSTRichTextBoxValue;
						}
						else
						{
							//����ֵ
						}
						#endregion ;

						#region �ų�
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
                            strXml = ((string[])objOut[i])[2];
                            //switch (((string[])objOut[i])[0])
                            //{
                            //    case "E":
                            //        if(objNext != null && objNext.m_intOutE != objCurrent.m_intOutE)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText.ToString(),objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml =((string[])objOut[i])[2];
                            //        }
                            //        break;
                            //    case "S":
                            //        if(objNext != null && objNext.m_intOutS != objCurrent.m_intOutS)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml =((string[])objOut[i])[2];
                            //        }
                            //        break;
                            //    case "U":
                            //        if(objNext != null && objNext.m_intOutU != objCurrent.m_intOutU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml =((string[])objOut[i])[2];
                            //        }
                            //        break;
                            //    case "V":
                            //        if(objNext != null && objNext.m_intOutV != objCurrent.m_intOutV)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                            //        {
                            //            strXml = m_strGetDSTTextXML(strText,objCurrent.m_strModifyUserID,objCurrent.m_strModifyUserName);
                            //        }
                            //        else
                            //        {
                            //            strXml =((string[])objOut[i])[2];
                            //        }
                            //        break;
                            //}
							objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
							objclsDSTRichTextBoxValue.m_strText=strText;						
							objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
							objData[18] = objclsDSTRichTextBoxValue;

						}
						else
						{
							//����ֵ
						}
						#endregion ;
						
						#region ���̼�¼����
                        
                        if (arlRecordContent.Count > 0 && i != 0)
                        {
                            DateTime dtContentRecordDateTemp;
                            strText = arlRecordContent[i - 1].ToString();
                            DateTime.TryParse(strText, out dtContentRecordDateTemp);
                            if (dtContentRecordDateTemp != DateTime.MinValue)
                            {
                                dtContentRecordDate = dtContentRecordDateTemp;
                            }
                            strXml = arlRecordContentXML[i - 1].ToString();
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[19] = objclsDSTRichTextBoxValue;
                            if (dtContentRecordDate != DateTime.MinValue)
                                objData[22] = dtContentRecordDate;//���̼�¼�ļ�¼ʱ��
                        }
                        else if (i == 0)
                        {
                            strText = objIntesiveTendInfo.m_objRecordContent.m_strModifyUserName;//�����¼��ǩ����ʾ�ڲ��̼�¼��
                            strXml = "<root />";
                            objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            objclsDSTRichTextBoxValue.m_strText = strText;
                            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[19] = objclsDSTRichTextBoxValue;
                        }
						#endregion ;
						#endregion ;

						#region ���ǩ�� ����

                        //clsEmployee m_objSign;
                        //if ((objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strModifyUserName == "" ||
                        //    objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strModifyUserName == null) &&
                        //    objIntesiveTendInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strModifyUserID != "")
                        //{
                        //    m_objSign = new clsEmployee(objIntesiveTendInfo.m_objTransDataArr[n/*intSingleTypeCount-1*/].m_strModifyUserID);
                        //    objData[20] = (i == (intMaxCount - 1) ? m_objSign.m_StrFirstName : "");//ǩ��
                        //}
                        //else
                        //    objData[20] = (i == (intMaxCount - 1) ? objIntesiveTendInfo.m_objTransDataArr[n/*intSingleTypeCount-1*/].m_strModifyUserName : "");//ǩ��

						#endregion ;
                   
                        objData[23] = objIntesiveTendInfo.m_objRecordContent.m_strCreateUserID;

                        objReturnData.Add(objData);	//��Ӽ�¼

                        //while (intCaseRowCount > 0 && i == intMaxCount - 1)
                        //{
                        //    objData = new object[25];
                        //    strText = strCurrentCaseTextArr[strCurrentCaseTextArr.Length - intCaseRowCount];
                        //    strXml = strCurrentCaseXmlArr[strCurrentCaseTextArr.Length - intCaseRowCount];
                        //    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        //    objclsDSTRichTextBoxValue.m_strText = strText;
                        //    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        //    objData[19] = objclsDSTRichTextBoxValue;
                        //    objData[22] = strCurrentCaseCreateDateArr[strCurrentCaseTextArr.Length - intCaseRowCount];//��Ų��̼�¼���ݴ���ʱ����ַ���
                        //    intCaseRowCount--;
                        //    objReturnData.Add(objData);
                        //}
			
					}

				}
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
		/// <summary>
		/// ��ȡ��ӡ������
		/// </summary>
		/// <returns></returns>
		protected override infPrintRecord m_objGetPrintTool()
		{
//			return new clsIntensiveTendMainPrintTool();
			return new clsIntensiveTendMain_FC_PrintTool();
		}

		// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.IntensiveTend_FC:
					return new frmIntensiveTend_FC();
				case enmDiseaseTrackType.IntensiveTend_FCCon:
                    return new frmIntensiveTend_FContent(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "");
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
			objContent.m_dtmInPatientDate=m_objCurrentPatient.m_DtmSelectedInDate;
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
            objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = p_objDataArr[23].ToString();    
		
			return objContent;
		}


		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmIntensiveTendMain_Load(object sender, System.EventArgs e)
		{

			#region ����Ҽ��˵�
			System.Windows.Forms.MenuItem mniContentAdd=new System.Windows.Forms.MenuItem();
			mniContentAdd.Index = 10;
			mniContentAdd.Text = "��Ӳ��̼�¼����";
			mniContentAdd.Click += new System.EventHandler(mniContentAdd_Click);
			System.Windows.Forms.MenuItem mniContentModify=new System.Windows.Forms.MenuItem();
			mniContentModify.Index = 11;
			mniContentModify.Text = "�޸Ĳ��̼�¼����";
			mniContentModify.Click += new System.EventHandler(mniContentModify_Click);
			System.Windows.Forms.MenuItem mniContentDelete=new System.Windows.Forms.MenuItem();
			mniContentDelete.Index = 12;
			mniContentDelete.Text = "ɾ�����̼�¼����";
			mniContentDelete.Click += new System.EventHandler(mniContentDelete_Click);
			this.ctmRecordControl.MenuItems.Add(mniContentAdd);
			this.ctmRecordControl.MenuItems.Add(mniContentModify);
			this.ctmRecordControl.MenuItems.Add(mniContentDelete);
			
			#endregion ;
			m_dtmPreRecordDate = DateTime.MinValue;
			m_dtgRecordDetail.Focus();
			m_mniAddBlank.Visible=false;
			m_mniDeleteBlank.Visible=false;
		}
		/// <summary>
		/// ��Ӳ��̼�¼����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mniContentAdd_Click(object sender, System.EventArgs e)
		{
			long lngRes=0;
			try
			{
				//��֤
				//���ݲ���
				//�򿪴���				
                frmIntensiveTend_FContent frm = new frmIntensiveTend_FContent(true, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), "2005");
                frm.m_objBaseCurrentPatient = this.m_objBaseCurrentPatient;
                frm.Closed += new EventHandler(m_mthSubFormClosed);
				m_blnCanShowNewForm = false;
				m_FrmCurrentSub = frm;

				frm.TopMost = true;				
				//����;
				frm.Show();
//				if (frm.Show() == DialogResult.Yes)
//				{
//					TreeNode trvTemp=m_trvInPatientDate.SelectedNode;
//					m_trvInPatientDate.SelectedNode=null;
//					m_trvInPatientDate.SelectedNode=trvTemp;
//				}

			}
			catch (Exception ex)
			{
				string strMsg=ex.Message;
			}
 
		}
		private void m_mthSubFormClosed(object p_objSender,EventArgs p_objArg)
		{
			frmIntensiveTend_FContent frmAddNewForm = (frmIntensiveTend_FContent)p_objSender; 
			//��ʾ����
			
			if (frmAddNewForm.DialogResult == DialogResult.Yes)
			{
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
			}
            m_FrmCurrentSub = null;
            m_blnCanShowNewForm = true;
		}
		/// <summary>
		/// �޸Ĳ��̼�¼����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mniContentModify_Click(object sender, System.EventArgs e) 
		{
			long lngRes=0;
			try
			{
				//��֤
				if (m_dtgRecordDetail.CurrentCell.ColumnNumber!=17)
				{
					MessageBox.Show("��ѡ��һ�����̼�¼���ݣ�");
					return;
				}
				//���ݲ���
				int intSelectedRecordStartRow =m_dtgRecordDetail.CurrentCell.RowNumber;
				string strCreatedate = (string)m_dtbRecords.Rows[intSelectedRecordStartRow][22];
				if (strCreatedate.Trim().Length==0)
					return;
				//�򿪴���
                frmIntensiveTend_FContent frm = new frmIntensiveTend_FContent(false, m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strCreatedate);
                frm.m_objBaseCurrentPatient = this.m_objBaseCurrentPatient;
				frm.Closed += new EventHandler(m_mthSubFormClosed);
				m_blnCanShowNewForm = false;
				m_FrmCurrentSub = frm;

				frm.TopMost = true;	
				//����
				frm.Show();
//				if (frm.Show() == DialogResult.Yes)
//				{
//					TreeNode trvTemp=m_trvInPatientDate.SelectedNode;
//					m_trvInPatientDate.SelectedNode=null;
//					m_trvInPatientDate.SelectedNode=trvTemp;
//				}

			}
			catch (Exception ex)
			{
				string strMsg=ex.Message;
			}
 		}
		/// <summary>
		/// ɾ�����̼�¼����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mniContentDelete_Click(object sender, System.EventArgs e)
		{
			long lngRes=0;
            //��֤
            if (m_dtgRecordDetail.CurrentCell.ColumnNumber != 18)
            {
                MessageBox.Show("��ѡ��һ�����̼�¼���ݣ�");
                return;
            }
            //���ݲ���
            int intSelectedRecordStartRow = m_dtgRecordDetail.CurrentCell.RowNumber;
            string strCreatedate = (string)m_dtbRecords.Rows[intSelectedRecordStartRow][22];
            if (strCreatedate.Trim().Length == 0)
                return;
            //ȷ��
            if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ����¼���ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            //clsIntensiveTendRecordService objserv =
            //    (clsIntensiveTendRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecordService));

            try
            {
                //�򿪴���
                //ɾ��
                string strDelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strDelID = MDIParent.OperatorID;
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteRecordContent(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strCreatedate, strDelTime, strDelID);
                //����
                if (lngRes > 0)
                {
                    m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                }

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            finally
            {
                //objserv.Dispose();
            }
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
			m_mthAddNewRecord((int)enmDiseaseTrackType.IntensiveTend_FC);
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
                //this.m_picExpand.Image=imgUserclose;				
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
                //this.m_picExpand.Image=imgUseropen;				
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
		/// <summary>
		/// �����Ӵ���
		/// </summary>
		/// <param name="p_frmSubForm"></param>
        protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        {
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession,0);
        }
		/// <summary>
		/// ��Tableɾ������
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_dtmCreateRecordTime"></param>
        protected override void m_mthRemoveDataFromDataTable(int p_intRecordType,
            DateTime p_dtmCreateRecordTime)
        {
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

//		protected override void frmLoadMethod()
//		{
//			dtTempTable= new DataTable("RecordDetail");
//			m_mthInitDataTable(m_dtbRecords);
//			m_dtgRecordDetail.DataSource = dtTempTable;
//			m_mthSetRichTextBoxAttribInControl(this);
//			m_mthSetQuickKeys();
//		}
		protected override void m_mthClearPatientRecordInfo()
		{
//			base.m_mthClearPatientBaseInfo ();
			m_mthSetDataGridFirstRowFocus();
			m_dtgRecordDetail.CurrentRowIndex = 0;
			m_dtbRecords.Rows.Clear();
			//��ռ�¼����                       
			m_mthClearRecordInfo();
			dtTempTable.Rows.Clear();
			strCurrentClass="";
		}
		#region ����
				/// <summary>
		/// ˫��DataGrid�ڵĿؼ��������¼� ��д
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		protected override void m_mthTxtDoubleClick(object sender, EventArgs e)
//		{
//			if(!m_blnCheckDataGridCurrentRow())
//				return;
//			try
//			{
//				int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//				if(intSelectedRecordStartRow < 0)
//					return;
//				string strOpenDate = dtTempTable.Rows[intSelectedRecordStartRow][2].ToString();
//				int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//				m_mthModifyRecord(intRecordType,DateTime.Parse(strOpenDate));
//			}
//			catch//��׽δ֪���� Alex 2003-7-31
//			{
//				
//			}
//		}
		/// <summary>
		///  ��ȡ�û�ѡ��ļ�¼�Ŀ�ʼ��λ��
		/// </summary>
		/// <param name="p_intSelectRowIndex">��������</param>
		/// <returns></returns>
//		protected override int m_intGetRecordStartRow(int p_intSelectRowIndex)
//		{
//			//��p_intSelectRow��ʼ���Ӻ���ǰѭ��DataTable
//			//�����ǰ��¼�ĵ�һ���ֶβ�Ϊ��
//			//��������
//			for(int i1=p_intSelectRowIndex;i1>=0;i1--)
//			{
//				if(dtTempTable.Rows[i1][1].ToString() != "")
//				{
//					return i1;
//				}
//			}
//			return -1;
//		}

//		protected override bool m_blnCheckDataGridCurrentRow()
//		{
//			try
//			{
//				if(dtTempTable.Rows.Count <=0)
//					return false;
//				if(m_dtgRecordDetail.CurrentCell.RowNumber  >= dtTempTable.Rows.Count)
//				{
//					return false;
//				}
//				return true;
//			}
//			catch//��׽δ֪���� Alex 2003-7-31
//			{
//				return false;
//			}
//		}

//		protected override void mniEdit_Click(object sender, System.EventArgs e)
//		{
//			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
//			if(!m_blnCheckDataGridCurrentRow())
//				return;
//			this.Cursor=Cursors.WaitCursor;
//			try
//			{
//				int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//				if(intSelectedRecordStartRow < 0)
//					return;
//
//				string strOpenDate = dtTempTable.Rows[intSelectedRecordStartRow][2].ToString();
//				int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//				m_mthModifyRecord(intRecordType,DateTime.Parse(strOpenDate));
//			}
//			catch (Exception exp)//��׽δ֪���� Alex 2003-7-31
//			{
//				string strtemp=	exp.Message;
//			}
//			this.Cursor=Cursors.Default;
//		}

//		protected override void mniDelete_Click(object sender, System.EventArgs e)
//		{
//			enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
//
//			if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,enmFormState.NowUser)
//				== enmDBControlCheckResult.Disable)
//			{
//				clsPublicFunction.s_mthShowNotPermitMessage();
//				return;
//			}		
//			
//			if(!m_blnCheckDataGridCurrentRow())
//				return;
//			if(!m_lngCanYouDoIt())
//			{
//				clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩɾ����");
//				return;
//			}
//			int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//			if(intSelectedRecordStartRow < 0)
//				return;
//			string strCreateDate = dtTempTable.Rows[intSelectedRecordStartRow][0].ToString();
//			int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//			if (intRecordType == 100)
//				return;
//			if(!clsPublicFunction.s_blnAskForDelete())
//				return ;
//			this.Cursor=Cursors.WaitCursor;
//			m_mthDeleteRecord(intRecordType,dtTempTable.Rows[intSelectedRecordStartRow].ItemArray);
//			this.Cursor=Cursors.Default;
//		}
//		protected override bool m_lngCanYouDoIt()
//		{
//			int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//			if(intSelectedRecordStartRow < 0)
//				return false;
//
//			string strOpenDate = dtTempTable.Rows[intSelectedRecordStartRow][2].ToString();
//			int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//
//			//��ȡ��¼�Ĵ���
//			string  strFormID = m_strGetFormID(intRecordType);
//
//			return m_objApprove.lngCanYouDoIt(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID,strFormID,m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),strOpenDate,((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID);
//
//		}
//
//		protected override void mniApprove_Click(object sender, System.EventArgs e)
//		{
//			if(!m_blnCheckDataGridCurrentRow())
//				return;
//			
//			int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//			if(intSelectedRecordStartRow < 0)
//				return;
//
//			string strOpenDate = dtTempTable.Rows[intSelectedRecordStartRow][2].ToString();
//			int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//
//			if(this.GetType().Name=="frmWatchItemTrack")
//			{
//				intRecordType = 3;	
//			}
//			//��ȡ��¼�Ĵ���
//			string  strFormID = m_strGetFormID(intRecordType);
//
//			long lngEff=0;
//			lngEff = m_objApprove.lngApproveDocument(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID,strFormID,m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),strOpenDate,((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);
//			
//			#region ���ݽ������ͬ�Ĵ���
//			switch((enmApproveResult)lngEff)
//			{
//				case enmApproveResult.DB_Succeed:					
//					clsPublicFunction.ShowInformationMessageBox("��˳ɹ�!");
//					break;
//				case enmApproveResult.System_Not_Define:					
//					clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
//					break;
//				case enmApproveResult.Document_Has_Been_Finished:					
//					clsPublicFunction.ShowInformationMessageBox("���Ѿ��������󣬲������������!");
//					break;
//				case enmApproveResult.No_Purview:					
//					clsPublicFunction.ShowInformationMessageBox("���û���Ȩ���������еĸò���!");
//					break;
//				case enmApproveResult.EmployeeID_Error:					
//					clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
//					break;
//				case enmApproveResult.Not_Found_Approve_Info:					
//					clsPublicFunction.ShowInformationMessageBox("û���ҵ��õ�������˵���Ϣ!");
//					break;
//				case enmApproveResult.Is_Top_Level:					
//					clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
//					break;
//				case enmApproveResult.Document_Has_Been_Deleted:					
//					clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
//					break;
//				default:
//					break;
//			}
//			#endregion ���ݽ������ͬ�Ĵ���
//			
//		}

//		protected override  void mniUnApprove_Click(object sender, System.EventArgs e)
//		{
//			if(!m_blnCheckDataGridCurrentRow())
//				return;
//			
//			int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
//			if(intSelectedRecordStartRow < 0)
//				return;
//
//			string strOpenDate = dtTempTable.Rows[intSelectedRecordStartRow][2].ToString();
//			int intRecordType = (int)dtTempTable.Rows[intSelectedRecordStartRow][1];
//
//			//��ȡ��¼�Ĵ���
//			string  strFormID = m_strGetFormID(intRecordType);
//
//			long lngEff=0;
//			lngEff = m_objApprove.lngUntreadDocumentOneLevel(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID,strFormID,m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),strOpenDate,((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);
//			
//			#region ���ݽ������ͬ�Ĵ���
//			switch((enmApproveResult)lngEff)
//			{
//				case enmApproveResult.DB_Succeed:					
//					clsPublicFunction.ShowInformationMessageBox("����ɹ�!");
//					break;
//				case enmApproveResult.System_Not_Define:					
//					clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
//					break;
//				case enmApproveResult.Document_Has_Been_Finished:					
//					clsPublicFunction.ShowInformationMessageBox("���Ѿ��������󣬲������������!");
//					break;
//				case enmApproveResult.No_Purview:					
//					clsPublicFunction.ShowInformationMessageBox("���û���Ȩ���������еĸò���!");
//					break;
//				case enmApproveResult.EmployeeID_Error:					
//					clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
//					break;
//				case enmApproveResult.Not_Found_Approve_Info:					
//					clsPublicFunction.ShowInformationMessageBox("û���ҵ��õ�������˵���Ϣ!");
//					break;
//				case enmApproveResult.Is_Top_Level:					
//					clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
//					break;
//				case enmApproveResult.Document_Has_Been_Deleted:					
//					clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
//					break;
//				default:
//					break;
//			}
//			#endregion ���ݽ������ͬ�Ĵ���
//		}		
//
		#endregion ;



	}
}