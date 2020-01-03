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
using System.Xml;
using System.IO;


namespace iCare
{
	public class frmMainICUIntensiveTend : iCare.frmRecordsBase
	{
		#region Define
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcDate;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcTime;
		private cltDataGridDSTRichTextBox m_dtcT;
		private cltDataGridDSTRichTextBox m_dtcP;
		private cltDataGridDSTRichTextBox m_dtcR;
		private cltDataGridDSTRichTextBox m_dtcBp;
		private cltDataGridDSTRichTextBox m_dtcCVP;
		private cltDataGridDSTRichTextBox m_dtcBloodSugar;
		private cltDataGridDSTRichTextBox m_dtcConsciousness;
		private cltDataGridDSTRichTextBox m_dtcPupilSizeLeft;
		private cltDataGridDSTRichTextBox m_dtcPupilSizeRight;
		private cltDataGridDSTRichTextBox m_dtcReflectLeft;
		private cltDataGridDSTRichTextBox m_dtcReflectRight;
		private cltDataGridDSTRichTextBox m_dtcDrugName;
		private cltDataGridDSTRichTextBox m_dtcDrugDosage;
		private cltDataGridDSTRichTextBox m_dtcStomachDirection;
		private cltDataGridDSTRichTextBox m_dtcStomachProperty;
		private cltDataGridDSTRichTextBox m_dtcStomachQuantity;
		private cltDataGridDSTRichTextBox m_dtcPeeDirection;
		private cltDataGridDSTRichTextBox m_dtcPeeProperty;
		private cltDataGridDSTRichTextBox m_dtcPeeQuantity;
		private cltDataGridDSTRichTextBox m_dtcDefecateProperty;
		private cltDataGridDSTRichTextBox m_dtcDefecateQuantity;
		private cltDataGridDSTRichTextBox m_dtcLeadDirection;
		private cltDataGridDSTRichTextBox m_dtcLeadProperty;
		private cltDataGridDSTRichTextBox m_dtcLeadQuantity;
		private cltDataGridDSTRichTextBox m_dtcSputumProperty;
		private cltDataGridDSTRichTextBox m_dtcSputumQuantity;
		private cltDataGridDSTRichTextBox m_dtcSkin;
		private cltDataGridDSTRichTextBox m_dtcCaseHistory;
		private System.Windows.Forms.DataGridTextBoxColumn m_dtcSign;
		private cltDataGridDSTRichTextBox m_dtcInOralType;
		private cltDataGridDSTRichTextBox m_dtcHR;
		private cltDataGridDSTRichTextBox m_dtcMuscle;
		private cltDataGridDSTRichTextBox m_dtcDrugQty;//��Һ����
		private cltDataGridDSTRichTextBox m_dtcInOralDrugName;
		private cltDataGridDSTRichTextBox m_dtcInOralProperty;
		private cltDataGridDSTRichTextBox m_dtcInOralQty;
		private cltDataGridDSTRichTextBox m_dtcInTotalQty;//��ʳ������
		private cltDataGridDSTRichTextBox m_dtcDefecateTimes;
		private System.Windows.Forms.Panel panel1;
		
		private System.ComponentModel.IContainer components = null;

		private string m_strCurrentOpenDate = "";
		private string m_strCreateUserID = "";

		#endregion

		public frmMainICUIntensiveTend()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			m_dtcDrugDosage.m_BlnGobleSet = false;
			m_dtcStomachQuantity.m_BlnGobleSet = false;
			m_dtcPeeQuantity.m_BlnGobleSet = false;
			m_dtcDefecateQuantity.m_BlnGobleSet = false;
			m_dtcLeadQuantity.m_BlnGobleSet = false;
			m_dtcDrugQty.m_BlnGobleSet = false;
			m_dtcInTotalQty.m_BlnGobleSet = false;

			// TODO: Add any initialization after the InitializeComponent call
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//���ԭ�����ַ�

			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);
		}

		/// <summary>
		/// ����Xml�Ļ���
		/// </summary>
		private MemoryStream m_objXmlMemStream;

		/// <summary>
		/// ����Xml�Ĺ���
		/// </summary>
		private XmlTextWriter m_objXmlWriter;
		
		/// ��ȡXml�����������
		/// </summary>
		private XmlParserContext m_objXmlParser;
		
		protected override Font m_FntHeaderFont
		{
			get
			{
				return new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			}
		}

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
            this.m_dtcT = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBp = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCVP = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcBloodSugar = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcConsciousness = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilSizeLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPupilSizeRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcReflectLeft = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcReflectRight = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDrugName = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDrugDosage = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcStomachDirection = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcStomachProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcStomachQuantity = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInOralType = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPeeDirection = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPeeProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcPeeQuantity = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDefecateProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDefecateQuantity = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcLeadDirection = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcLeadProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcLeadQuantity = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSputumProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSputumQuantity = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSkin = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcCaseHistory = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcSign = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_dtcHR = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcMuscle = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDrugQty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInOralDrugName = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInOralProperty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInOralQty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcInTotalQty = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.m_dtcDefecateTimes = new com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbRecords)).BeginInit();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            this.dgtsStyles.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										 this.m_dtcDate,
 																										 this.m_dtcTime,
 																										 this.m_dtcT,
 																										 this.m_dtcHR,
 																										 this.m_dtcP,
																										 this.m_dtcR,
																										 this.m_dtcBp,
																										 this.m_dtcCVP,
																										 this.m_dtcBloodSugar,
 																										 this.m_dtcConsciousness,
 																										 this.m_dtcPupilSizeLeft,
																										 this.m_dtcPupilSizeRight,
 																										 this.m_dtcReflectLeft,
 																										 this.m_dtcReflectRight,
 																										 this.m_dtcMuscle,
 																										 this.m_dtcDrugName,
 																										 this.m_dtcDrugDosage,
 																										 this.m_dtcDrugQty,
 																										 this.m_dtcStomachDirection,
 																										 this.m_dtcStomachProperty,
 																										 this.m_dtcStomachQuantity,
 																										 this.m_dtcInOralType,
 																										 this.m_dtcInOralDrugName,
 																										 this.m_dtcInOralProperty,
 																										 this.m_dtcInOralQty,
 																										 this.m_dtcInTotalQty,
 																										 this.m_dtcPeeDirection,
 																										 this.m_dtcPeeProperty,
 																										 this.m_dtcPeeQuantity,
 																										 this.m_dtcDefecateProperty,
 																										 this.m_dtcDefecateTimes,
 																										 this.m_dtcDefecateQuantity,
 																										 this.m_dtcLeadDirection,
 																										 this.m_dtcLeadProperty,
 																										 this.m_dtcLeadQuantity,
 																										 this.m_dtcSputumProperty,
 																										 this.m_dtcSputumQuantity,
 																										 this.m_dtcSkin,
 																										 this.m_dtcCaseHistory,
 																										 this.m_dtcSign});
            this.dgtsStyles.RowHeaderWidth = 15; 
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.AccessibleName = "DataGrid";
            this.m_dtgRecordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Table;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtgRecordDetail.DataSource = this.m_dtbRecords;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(15, 78);
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(788, 524);
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
            this.m_trvInPatientDate.Location = new System.Drawing.Point(-103, 103);
            this.m_trvInPatientDate.Size = new System.Drawing.Size(168, 64);
            this.m_trvInPatientDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.SystemColors.Control;
            this.lblSex.Location = new System.Drawing.Point(-14, 119);
            this.lblSex.Size = new System.Drawing.Size(52, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.SystemColors.Control;
            this.lblAge.Location = new System.Drawing.Point(-8, 103);
            this.lblAge.Size = new System.Drawing.Size(56, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(372, 66);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(505, 69);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(522, 70);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(-14, 155);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(-14, 138);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(417, 70);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(-28, 112);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(76, 104);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(-28, 112);
            this.txtInPatientID.Size = new System.Drawing.Size(76, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(525, 69);
            this.m_txtPatientName.Size = new System.Drawing.Size(100, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(420, 59);
            this.m_txtBedNO.Size = new System.Drawing.Size(56, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(396, 62);
            this.m_cboArea.Size = new System.Drawing.Size(120, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(-11, 97);
            this.m_lsvPatientName.Size = new System.Drawing.Size(100, 100);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(-11, 93);
            this.m_lsvBedNO.Size = new System.Drawing.Size(76, 104);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(379, 69);
            this.m_cboDept.Size = new System.Drawing.Size(120, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(348, 75);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(656, -36);
            this.m_cmdNewTemplate.Size = new System.Drawing.Size(84, 36);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(479, 66);
            this.m_cmdNext.Size = new System.Drawing.Size(20, 23);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(508, 52);
            this.m_cmdPre.Size = new System.Drawing.Size(8, 4);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(488, 52);
            this.m_lblForTitle.Size = new System.Drawing.Size(8, 4);
            this.m_lblForTitle.Text = "Σ��֢�໤�����ػ���¼��";
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(-40, 128);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(662, 37);
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 28);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Size = new System.Drawing.Size(795, 60);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(793, 29);
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
            // m_dtcT
            // 
            this.m_dtcT.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcT.HeaderText = "T";
            this.m_dtcT.m_BlnGobleSet = true;
            this.m_dtcT.m_BlnUnderLineDST = false;
            this.m_dtcT.MappingName = "T";
            this.m_dtcT.NullText = "";
            this.m_dtcT.Width = 40;
            // 
            // m_dtcP
            // 
            this.m_dtcP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcP.HeaderText = "P";
            this.m_dtcP.m_BlnGobleSet = true;
            this.m_dtcP.m_BlnUnderLineDST = false;
            this.m_dtcP.MappingName = "P";
            this.m_dtcP.NullText = "";
            this.m_dtcP.Width = 40;
            // 
            // m_dtcR
            // 
            this.m_dtcR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcR.HeaderText = "R";
            this.m_dtcR.m_BlnGobleSet = true;
            this.m_dtcR.m_BlnUnderLineDST = false;
            this.m_dtcR.MappingName = "R";
            this.m_dtcR.NullText = "";
            this.m_dtcR.Width = 40;
            // 
            // m_dtcBp
            // 
            this.m_dtcBp.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBp.HeaderText = "Bp";
            this.m_dtcBp.m_BlnGobleSet = true;
            this.m_dtcBp.m_BlnUnderLineDST = false;
            this.m_dtcBp.MappingName = "Bp";
            this.m_dtcBp.NullText = "";
            this.m_dtcBp.Width = 40;
            // 
            // m_dtcCVP
            // 
            this.m_dtcCVP.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCVP.HeaderText = "CVP";
            this.m_dtcCVP.m_BlnGobleSet = true;
            this.m_dtcCVP.m_BlnUnderLineDST = false;
            this.m_dtcCVP.MappingName = "CVP";
            this.m_dtcCVP.NullText = "";
            this.m_dtcCVP.Width = 40;
            // 
            // m_dtcBloodSugar
            // 
            this.m_dtcBloodSugar.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcBloodSugar.HeaderText = "Ѫ��";
            this.m_dtcBloodSugar.m_BlnGobleSet = true;
            this.m_dtcBloodSugar.m_BlnUnderLineDST = false;
            this.m_dtcBloodSugar.MappingName = "BloodSugar";
            this.m_dtcBloodSugar.NullText = "";
            this.m_dtcBloodSugar.Width = 40;
            // 
            // m_dtcConsciousness
            // 
            this.m_dtcConsciousness.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcConsciousness.HeaderText = "��ʶ";
            this.m_dtcConsciousness.m_BlnGobleSet = true;
            this.m_dtcConsciousness.m_BlnUnderLineDST = false;
            this.m_dtcConsciousness.MappingName = "Consciousness";
            this.m_dtcConsciousness.NullText = "";
            this.m_dtcConsciousness.Width = 40;
            // 
            // m_dtcPupilSizeLeft
            // 
            this.m_dtcPupilSizeLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilSizeLeft.HeaderText = "ͫ����";
            this.m_dtcPupilSizeLeft.m_BlnGobleSet = true;
            this.m_dtcPupilSizeLeft.m_BlnUnderLineDST = false;
            this.m_dtcPupilSizeLeft.MappingName = "PupilSizeLeft";
            this.m_dtcPupilSizeLeft.NullText = "";
            this.m_dtcPupilSizeLeft.Width = 40;
            // 
            // m_dtcPupilSizeRight
            // 
            this.m_dtcPupilSizeRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPupilSizeRight.HeaderText = "ͫ����";
            this.m_dtcPupilSizeRight.m_BlnGobleSet = true;
            this.m_dtcPupilSizeRight.m_BlnUnderLineDST = false;
            this.m_dtcPupilSizeRight.MappingName = "PupilSizeRight";
            this.m_dtcPupilSizeRight.NullText = "";
            this.m_dtcPupilSizeRight.Width = 40;
            // 
            // m_dtcReflectLeft
            // 
            this.m_dtcReflectLeft.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcReflectLeft.HeaderText = "����(��)";
            this.m_dtcReflectLeft.m_BlnGobleSet = true;
            this.m_dtcReflectLeft.m_BlnUnderLineDST = false;
            this.m_dtcReflectLeft.MappingName = "ReflectLeft";
            this.m_dtcReflectLeft.NullText = "";
            this.m_dtcReflectLeft.Width = 40;
            // 
            // m_dtcReflectRight
            // 
            this.m_dtcReflectRight.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcReflectRight.HeaderText = "���䣨�ң�";
            this.m_dtcReflectRight.m_BlnGobleSet = true;
            this.m_dtcReflectRight.m_BlnUnderLineDST = false;
            this.m_dtcReflectRight.MappingName = "ReflectRight";
            this.m_dtcReflectRight.NullText = "";
            this.m_dtcReflectRight.Width = 40;
            // 
            // m_dtcDrugName
            // 
            this.m_dtcDrugName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDrugName.HeaderText = "����Һҩ��";
            this.m_dtcDrugName.m_BlnGobleSet = true;
            this.m_dtcDrugName.m_BlnUnderLineDST = false;
            this.m_dtcDrugName.MappingName = "DrugName";
            this.m_dtcDrugName.NullText = "";
            this.m_dtcDrugName.Width = 70;
            // 
            // m_dtcDrugDosage
            // 
            this.m_dtcDrugDosage.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDrugDosage.HeaderText = "����Һ����";
            this.m_dtcDrugDosage.m_BlnGobleSet = true;
            this.m_dtcDrugDosage.m_BlnUnderLineDST = false;
            this.m_dtcDrugDosage.MappingName = "DrugDosage";
            this.m_dtcDrugDosage.NullText = "";
            this.m_dtcDrugDosage.Width = 70;
            // 
            // m_dtcStomachDirection
            // 
            this.m_dtcStomachDirection.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcStomachDirection.HeaderText = "θ��ͨ��";
            this.m_dtcStomachDirection.m_BlnGobleSet = true;
            this.m_dtcStomachDirection.m_BlnUnderLineDST = false;
            this.m_dtcStomachDirection.MappingName = "StomachDirection";
            this.m_dtcStomachDirection.NullText = "";
            this.m_dtcStomachDirection.Width = 70;
            // 
            // m_dtcStomachProperty
            // 
            this.m_dtcStomachProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcStomachProperty.HeaderText = "θ������";
            this.m_dtcStomachProperty.m_BlnGobleSet = true;
            this.m_dtcStomachProperty.m_BlnUnderLineDST = false;
            this.m_dtcStomachProperty.MappingName = "StomachProperty";
            this.m_dtcStomachProperty.NullText = "";
            this.m_dtcStomachProperty.Width = 70;
            // 
            // m_dtcStomachQuantity
            // 
            this.m_dtcStomachQuantity.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcStomachQuantity.HeaderText = "θ����";
            this.m_dtcStomachQuantity.m_BlnGobleSet = true;
            this.m_dtcStomachQuantity.m_BlnUnderLineDST = false;
            this.m_dtcStomachQuantity.MappingName = "StomachQuantity";
            this.m_dtcStomachQuantity.NullText = "";
            this.m_dtcStomachQuantity.Width = 70;
            // 
            // m_dtcInOralType
            // 
            this.m_dtcInOralType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInOralType.HeaderText = "�ڷ�����";
            this.m_dtcInOralType.m_BlnGobleSet = true;
            this.m_dtcInOralType.m_BlnUnderLineDST = false;
            this.m_dtcInOralType.MappingName = "InOralType";
            this.m_dtcInOralType.NullText = "";
            this.m_dtcInOralType.Width = 40;
            // 
            // m_dtcPeeDirection
            // 
            this.m_dtcPeeDirection.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPeeDirection.HeaderText = "���ͨ��";
            this.m_dtcPeeDirection.m_BlnGobleSet = true;
            this.m_dtcPeeDirection.m_BlnUnderLineDST = false;
            this.m_dtcPeeDirection.MappingName = "PeeDirection";
            this.m_dtcPeeDirection.NullText = "";
            this.m_dtcPeeDirection.Width = 70;
            // 
            // m_dtcPeeProperty
            // 
            this.m_dtcPeeProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPeeProperty.HeaderText = "�������";
            this.m_dtcPeeProperty.m_BlnGobleSet = true;
            this.m_dtcPeeProperty.m_BlnUnderLineDST = false;
            this.m_dtcPeeProperty.MappingName = "PeeProperty";
            this.m_dtcPeeProperty.NullText = "";
            this.m_dtcPeeProperty.Width = 70;
            // 
            // m_dtcPeeQuantity
            // 
            this.m_dtcPeeQuantity.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcPeeQuantity.HeaderText = "�����";
            this.m_dtcPeeQuantity.m_BlnGobleSet = true;
            this.m_dtcPeeQuantity.m_BlnUnderLineDST = false;
            this.m_dtcPeeQuantity.MappingName = "PeeQuantity";
            this.m_dtcPeeQuantity.NullText = "";
            this.m_dtcPeeQuantity.Width = 70;
            // 
            // m_dtcDefecateProperty
            // 
            this.m_dtcDefecateProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDefecateProperty.HeaderText = "�������";
            this.m_dtcDefecateProperty.m_BlnGobleSet = true;
            this.m_dtcDefecateProperty.m_BlnUnderLineDST = false;
            this.m_dtcDefecateProperty.MappingName = "DefecateProperty";
            this.m_dtcDefecateProperty.NullText = "";
            this.m_dtcDefecateProperty.Width = 70;
            // 
            // m_dtcDefecateQuantity
            // 
            this.m_dtcDefecateQuantity.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDefecateQuantity.HeaderText = "�����";
            this.m_dtcDefecateQuantity.m_BlnGobleSet = true;
            this.m_dtcDefecateQuantity.m_BlnUnderLineDST = false;
            this.m_dtcDefecateQuantity.MappingName = "DefecateQuantity";
            this.m_dtcDefecateQuantity.NullText = "";
            this.m_dtcDefecateQuantity.Width = 70;
            // 
            // m_dtcLeadDirection
            // 
            this.m_dtcLeadDirection.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcLeadDirection.HeaderText = "������ͨ��";
            this.m_dtcLeadDirection.m_BlnGobleSet = true;
            this.m_dtcLeadDirection.m_BlnUnderLineDST = false;
            this.m_dtcLeadDirection.MappingName = "LeadDirection";
            this.m_dtcLeadDirection.NullText = "";
            this.m_dtcLeadDirection.Width = 70;
            // 
            // m_dtcLeadProperty
            // 
            this.m_dtcLeadProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcLeadProperty.HeaderText = "����������";
            this.m_dtcLeadProperty.m_BlnGobleSet = true;
            this.m_dtcLeadProperty.m_BlnUnderLineDST = false;
            this.m_dtcLeadProperty.MappingName = "LeadProperty";
            this.m_dtcLeadProperty.NullText = "";
            this.m_dtcLeadProperty.Width = 70;
            // 
            // m_dtcLeadQuantity
            // 
            this.m_dtcLeadQuantity.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcLeadQuantity.HeaderText = "��������";
            this.m_dtcLeadQuantity.m_BlnGobleSet = true;
            this.m_dtcLeadQuantity.m_BlnUnderLineDST = false;
            this.m_dtcLeadQuantity.MappingName = "LeadQuantity";
            this.m_dtcLeadQuantity.NullText = "";
            this.m_dtcLeadQuantity.Width = 70;
            // 
            // m_dtcSputumProperty
            // 
            this.m_dtcSputumProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSputumProperty.HeaderText = "̵����";
            this.m_dtcSputumProperty.m_BlnGobleSet = true;
            this.m_dtcSputumProperty.m_BlnUnderLineDST = false;
            this.m_dtcSputumProperty.MappingName = "SputumProperty";
            this.m_dtcSputumProperty.NullText = "";
            this.m_dtcSputumProperty.Width = 40;
            // 
            // m_dtcSputumQuantity
            // 
            this.m_dtcSputumQuantity.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSputumQuantity.HeaderText = "̵��";
            this.m_dtcSputumQuantity.m_BlnGobleSet = true;
            this.m_dtcSputumQuantity.m_BlnUnderLineDST = false;
            this.m_dtcSputumQuantity.MappingName = "SputumQuantity";
            this.m_dtcSputumQuantity.NullText = "";
            this.m_dtcSputumQuantity.Width = 40;
            // 
            // m_dtcSkin
            // 
            this.m_dtcSkin.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcSkin.HeaderText = "Ƥ�����";
            this.m_dtcSkin.m_BlnGobleSet = true;
            this.m_dtcSkin.m_BlnUnderLineDST = false;
            this.m_dtcSkin.MappingName = "Skin";
            this.m_dtcSkin.NullText = "";
            this.m_dtcSkin.Width = 200;
            // 
            // m_dtcCaseHistory
            // 
            this.m_dtcCaseHistory.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcCaseHistory.HeaderText = "�����¼";
            this.m_dtcCaseHistory.m_BlnGobleSet = true;
            this.m_dtcCaseHistory.m_BlnUnderLineDST = false;
            this.m_dtcCaseHistory.MappingName = "CaseHistory";
            this.m_dtcCaseHistory.NullText = "";
            this.m_dtcCaseHistory.Width = 200;
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
            // m_dtcHR
            // 
            this.m_dtcHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcHR.m_BlnGobleSet = true;
            this.m_dtcHR.m_BlnUnderLineDST = false;
            this.m_dtcHR.MappingName = "HR";
            this.m_dtcHR.NullText = "HR";
            this.m_dtcHR.Width = 75;
            // 
            // m_dtcMuscle
            // 
            this.m_dtcMuscle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcMuscle.HeaderText = "����";
            this.m_dtcMuscle.m_BlnGobleSet = true;
            this.m_dtcMuscle.m_BlnUnderLineDST = false;
            this.m_dtcMuscle.MappingName = "Muscle";
            this.m_dtcMuscle.NullText = "";
            this.m_dtcMuscle.Width = 75;
            // 
            // m_dtcDrugQty
            // 
            this.m_dtcDrugQty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDrugQty.HeaderText = "����Һ����";
            this.m_dtcDrugQty.m_BlnGobleSet = true;
            this.m_dtcDrugQty.m_BlnUnderLineDST = false;
            this.m_dtcDrugQty.MappingName = "DrugQty";
            this.m_dtcDrugQty.NullText = "";
            this.m_dtcDrugQty.Width = 75;
            // 
            // m_dtcInOralDrugName
            // 
            this.m_dtcInOralDrugName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInOralDrugName.HeaderText = "�ڷ�ҩ��";
            this.m_dtcInOralDrugName.m_BlnGobleSet = true;
            this.m_dtcInOralDrugName.m_BlnUnderLineDST = false;
            this.m_dtcInOralDrugName.MappingName = "InOralDrugName";
            this.m_dtcInOralDrugName.NullText = "";
            this.m_dtcInOralDrugName.Width = 75;
            // 
            // m_dtcInOralProperty
            // 
            this.m_dtcInOralProperty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInOralProperty.HeaderText = "�ڷ�����";
            this.m_dtcInOralProperty.m_BlnGobleSet = true;
            this.m_dtcInOralProperty.m_BlnUnderLineDST = false;
            this.m_dtcInOralProperty.MappingName = "InOralProperty";
            this.m_dtcInOralProperty.NullText = "";
            this.m_dtcInOralProperty.Width = 75;
            // 
            // m_dtcInOralQty
            // 
            this.m_dtcInOralQty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInOralQty.HeaderText = "�ڷ���";
            this.m_dtcInOralQty.m_BlnGobleSet = true;
            this.m_dtcInOralQty.m_BlnUnderLineDST = false;
            this.m_dtcInOralQty.MappingName = "InOralQty";
            this.m_dtcInOralQty.NullText = "";
            this.m_dtcInOralQty.Width = 75;
            // 
            // m_dtcInTotalQty
            // 
            this.m_dtcInTotalQty.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcInTotalQty.HeaderText = "��ʳ������";
            this.m_dtcInTotalQty.m_BlnGobleSet = true;
            this.m_dtcInTotalQty.m_BlnUnderLineDST = false;
            this.m_dtcInTotalQty.MappingName = "InTotalQty";
            this.m_dtcInTotalQty.NullText = "";
            this.m_dtcInTotalQty.Width = 75;
            // 
            // m_dtcDefecateTimes
            // 
            this.m_dtcDefecateTimes.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtcDefecateTimes.HeaderText = "������";
            this.m_dtcDefecateTimes.m_BlnGobleSet = true;
            this.m_dtcDefecateTimes.m_BlnUnderLineDST = false;
            this.m_dtcDefecateTimes.MappingName = "DefecateTimes";
            this.m_dtcDefecateTimes.NullText = "";
            this.m_dtcDefecateTimes.Width = 75;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(10, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 540);
            this.panel1.TabIndex = 10000004;
            // 
            // frmMainICUIntensiveTend
            // 
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(815, 673);
            this.Controls.Add(this.panel1);
            this.Name = "frmMainICUIntensiveTend";
            this.Text = "Σ��֢�໤�����ػ���¼��";
            this.Load += new System.EventHandler(this.frmMainICUIntensiveTend_Load);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
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

		private void frmMainICUIntensiveTend_Load(object sender, System.EventArgs e)
		{
			m_dtmPreRecordDate = DateTime.MinValue;
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
		
//			//ͨ����ʽ
//			p_dtbRecordTable.Columns.Add("MachineMode",typeof(clsDSTRichTextBoxValue));
//		
//			//����������
//			p_dtbRecordTable.Columns.Add("BreathSoundLeft",typeof(clsDSTRichTextBoxValue));
//		
//			//���������ң�
//			p_dtbRecordTable.Columns.Add("BreathSoundRight",typeof(clsDSTRichTextBoxValue));
//		
			//T
			p_dtbRecordTable.Columns.Add("T",typeof(clsDSTRichTextBoxValue));

			//HR
			p_dtbRecordTable.Columns.Add("HR",typeof(clsDSTRichTextBoxValue));

			//P
			p_dtbRecordTable.Columns.Add("P",typeof(clsDSTRichTextBoxValue));

			//R
			p_dtbRecordTable.Columns.Add("R",typeof(clsDSTRichTextBoxValue));

			//Bp
			p_dtbRecordTable.Columns.Add("Bp",typeof(clsDSTRichTextBoxValue));
		
			//CVP
			p_dtbRecordTable.Columns.Add("CVP",typeof(clsDSTRichTextBoxValue));
		
			//Ѫ��
			p_dtbRecordTable.Columns.Add("BloodSugar",typeof(clsDSTRichTextBoxValue));
		
			//��ʶ
			p_dtbRecordTable.Columns.Add("Consciousness",typeof(clsDSTRichTextBoxValue));
		
			//ͫ����
			p_dtbRecordTable.Columns.Add("PupilSizeLeft",typeof(clsDSTRichTextBoxValue));
		
			//ͫ����
			p_dtbRecordTable.Columns.Add("PupilSizeRight",typeof(clsDSTRichTextBoxValue));
		
			//������
			p_dtbRecordTable.Columns.Add("ReflectLeft",typeof(clsDSTRichTextBoxValue));
		
			//������
			p_dtbRecordTable.Columns.Add("ReflectRight",typeof(clsDSTRichTextBoxValue));

			//����
			p_dtbRecordTable.Columns.Add("Muscle",typeof(clsDSTRichTextBoxValue));
		
			//����Һҩ��
			p_dtbRecordTable.Columns.Add("DrugName",typeof(clsDSTRichTextBoxValue));
		
			//����Һ����
			p_dtbRecordTable.Columns.Add("DrugDosage",typeof(clsDSTRichTextBoxValue)); 
		
			//����Һ����
			p_dtbRecordTable.Columns.Add("DrugQty",typeof(clsDSTRichTextBoxValue)); 
		
			//θ��ͨ��
			p_dtbRecordTable.Columns.Add("StomachDirection",typeof(clsDSTRichTextBoxValue));   
		
			//θ������
			p_dtbRecordTable.Columns.Add("StomachProperty",typeof(clsDSTRichTextBoxValue)); 
		
			//θ����
			p_dtbRecordTable.Columns.Add("StomachQuantity",typeof(clsDSTRichTextBoxValue)); 


			//�ڷ�����
			p_dtbRecordTable.Columns.Add("InOralType",typeof(clsDSTRichTextBoxValue)); 
			//�ڷ�ҩ��
			p_dtbRecordTable.Columns.Add("InOralDrugName",typeof(clsDSTRichTextBoxValue)); 
			//�ڷ�����			
			p_dtbRecordTable.Columns.Add("InOralProperty",typeof(clsDSTRichTextBoxValue)); 
			//�ڷ���
			p_dtbRecordTable.Columns.Add("InOralQty",typeof(clsDSTRichTextBoxValue)); 

			//��ʳ����
			p_dtbRecordTable.Columns.Add("InTotalQty",typeof(clsDSTRichTextBoxValue)); 

			//���ͨ��
			p_dtbRecordTable.Columns.Add("PeeDirection",typeof(clsDSTRichTextBoxValue));

			//�������
			p_dtbRecordTable.Columns.Add("PeeProperty",typeof(clsDSTRichTextBoxValue));

			//�����
			p_dtbRecordTable.Columns.Add("PeeQuantity",typeof(clsDSTRichTextBoxValue));

			//�������
			p_dtbRecordTable.Columns.Add("DefecateProperty",typeof(clsDSTRichTextBoxValue));
			//������
			p_dtbRecordTable.Columns.Add("DefecateTimes",typeof(clsDSTRichTextBoxValue));
			//�����
			p_dtbRecordTable.Columns.Add("DefecateQuantity",typeof(clsDSTRichTextBoxValue));

			//������ͨ��
			p_dtbRecordTable.Columns.Add("LeadDirection",typeof(clsDSTRichTextBoxValue));

			//����������
			p_dtbRecordTable.Columns.Add("LeadProperty",typeof(clsDSTRichTextBoxValue));

			//��������
			p_dtbRecordTable.Columns.Add("LeadQuantity",typeof(clsDSTRichTextBoxValue));

			//̵����
			p_dtbRecordTable.Columns.Add("SputumProperty",typeof(clsDSTRichTextBoxValue));

			//̵��
			p_dtbRecordTable.Columns.Add("SputumQuantity",typeof(clsDSTRichTextBoxValue));

			//Ƥ�����
			p_dtbRecordTable.Columns.Add("Skin",typeof(clsDSTRichTextBoxValue));

			//�����¼
			p_dtbRecordTable.Columns.Add("CaseHistory",typeof(clsDSTRichTextBoxValue));

			//���ǩ��
			p_dtbRecordTable.Columns.Add("Sign");

			p_dtbRecordTable.Columns.Add("RecordSignID");

			#endregion

			int intPerformWith=38;
			#region Set Header
			this.m_dtcDate.HeaderText = "  ��\r\n\r\n\r\n\r\n  ��";
			this.m_dtcDate.Width = 80;

			this.m_dtcTime.HeaderText = "  ʱ\r\n\r\n\r\n\r\n  ��";
			this.m_dtcTime.Width = 70;

			this.m_dtcT.HeaderText = "T\r\n\r\n��";
			this.m_dtcT.Width = intPerformWith;

			this.m_dtcHR.HeaderText = "HR\r\n\r\nbpm";
			this.m_dtcHR.Width = intPerformWith;

			this.m_dtcP.HeaderText = "P\r\n\r\nbpm";
			this.m_dtcP.Width = intPerformWith;

			this.m_dtcR.HeaderText = "R\r\n\r\nbpm";
			this.m_dtcR.Width = intPerformWith;

			this.m_dtcBp.HeaderText = "Bp\r\n\r\nmmHg";
			this.m_dtcBp.Width = intPerformWith;

			this.m_dtcCVP.HeaderText = "CVP\r\n\r\ncmH2O";
			this.m_dtcCVP.Width = intPerformWith+5;

			this.m_dtcBloodSugar.HeaderText = "Ѫ\r\n��\r\nmmol\r\n /L";
			this.m_dtcBloodSugar.Width = intPerformWith;

			this.m_dtcConsciousness.HeaderText = "��\r\n־";
			this.m_dtcConsciousness.Width = intPerformWith;

			this.m_dtcPupilSizeLeft.HeaderText = "ͫ��\r\n��С\r\n��";
			this.m_dtcPupilSizeLeft.Width = intPerformWith;

			this.m_dtcPupilSizeRight.HeaderText = "ͫ��\r\n��С\r\n��";
			this.m_dtcPupilSizeRight.Width = intPerformWith;

			this.m_dtcReflectLeft.HeaderText = "�Թ�\r\n����\r\n��";
			this.m_dtcReflectLeft.Width = intPerformWith;

			this.m_dtcReflectRight.HeaderText = "�Թ�\r\n����\r\n��";
			this.m_dtcReflectRight.Width = intPerformWith;

			this.m_dtcMuscle.HeaderText = " ��\r\n ��";
			this.m_dtcMuscle.Width = intPerformWith;

			this.m_dtcDrugName.HeaderText =   " ��  ��\r\n  (ml)\r\n\r\n����Һ��\r\n\r\n  ҩ��";
			this.m_dtcDrugName.Width = intPerformWith*2;

			this.m_dtcDrugDosage.HeaderText = " ��  ��\r\n  (ml)\r\n\r\n����Һ��\r\n\r\n  ����";
			this.m_dtcDrugDosage.Width = intPerformWith*2;

			this.m_dtcDrugQty.HeaderText =    " ��  ��\r\n  (ml)\r\n\r\n����Һ��\r\n\r\n  ����";
			this.m_dtcDrugQty.Width = intPerformWith*2;

			this.m_dtcStomachDirection.HeaderText = "����\r\n(ml)\r\n\r\nθ��\r\n\r\nͨ��";
			this.m_dtcStomachDirection.Width = intPerformWith;

			this.m_dtcStomachProperty.HeaderText =  "����\r\n(ml)\r\n\r\nθ��\r\n\r\nθҺ\r\n����";
			this.m_dtcStomachProperty.Width = intPerformWith;

			this.m_dtcStomachQuantity.HeaderText =  "����\r\n(ml)\r\n\r\nθ��\r\n\r\n ��";
			this.m_dtcStomachQuantity.Width = intPerformWith;

			this.m_dtcInOralType.HeaderText = "����\r\n(ml)\r\n\r\n�ڷ�\r\n\r\n����";
			this.m_dtcInOralType.Width = intPerformWith;

			this.m_dtcInOralDrugName.HeaderText = "����\r\n(ml)\r\n\r\n�ڷ�\r\n\r\nҩ��";
			this.m_dtcInOralDrugName.Width = intPerformWith;

			this.m_dtcInOralProperty.HeaderText = "����\r\n(ml)\r\n\r\n�ڷ�\r\n\r\n����";
			this.m_dtcInOralProperty.Width = intPerformWith;

			this.m_dtcInOralQty.HeaderText = "����\r\n(ml)\r\n\r\n�ڷ�\r\n\r\n ��";
			this.m_dtcInOralQty.Width = intPerformWith;

			this.m_dtcInTotalQty.HeaderText = "����\r\n(ml)\r\n\r\n��ʳ��\r\n\r\n����";
			this.m_dtcInTotalQty.Width = intPerformWith;

			this.m_dtcPeeDirection.HeaderText = "����\r\n(ml)\r\n\r\n���\r\n\r\nͨ��";
			this.m_dtcPeeDirection.Width = intPerformWith;

			this.m_dtcPeeProperty.HeaderText =  "����\r\n(ml)\r\n\r\n���\r\n\r\n��Һ\r\n����";
			this.m_dtcPeeProperty.Width = intPerformWith;

			this.m_dtcPeeQuantity.HeaderText =  "����\r\n(ml)\r\n\r\n���\r\n\r\n ��";
			this.m_dtcPeeQuantity.Width = intPerformWith;

			this.m_dtcDefecateProperty.HeaderText = "����\r\n(ml)\r\n\r\n���\r\n\r\n����";
			this.m_dtcDefecateProperty.Width = intPerformWith;

			this.m_dtcDefecateTimes.HeaderText = "����\r\n(ml)\r\n\r\n���\r\n\r\n����";
			this.m_dtcDefecateTimes.Width = intPerformWith;

			this.m_dtcDefecateQuantity.HeaderText = "����\r\n(ml)\r\n\r\n���\r\n\r\n ��";
			this.m_dtcDefecateQuantity.Width = intPerformWith;

			this.m_dtcLeadDirection.HeaderText = "����\r\n(ml)\r\n\r\n������\r\n\r\nͨ��";
			this.m_dtcLeadDirection.Width = intPerformWith;

			this.m_dtcLeadProperty.HeaderText =  "����\r\n(ml)\r\n\r\n������\r\n\r\n����";
			this.m_dtcLeadProperty.Width = intPerformWith;

			this.m_dtcLeadQuantity.HeaderText =  "����\r\n(ml)\r\n\r\n������\r\n\r\n ��";
			this.m_dtcLeadQuantity.Width = intPerformWith;

			this.m_dtcSputumProperty.HeaderText = " ̵\r\n(ml)\r\n\r\n����";
			this.m_dtcSputumProperty.Width = intPerformWith;

			this.m_dtcSputumQuantity.HeaderText = " ̵\r\n(ml)\r\n\r\n ��";
			this.m_dtcSputumQuantity.Width = intPerformWith;

			this.m_dtcSkin.HeaderText = "  Ƥ  ��  ��  ��";
			this.m_dtcSkin.Width = 200;

			this.m_dtcCaseHistory.HeaderText = "  ��  ��  ��  ¼";
			this.m_dtcCaseHistory.Width = 200;

			this.m_dtcSign.HeaderText = " ǩ  ��";
			this.m_dtcSign.Width = 70;

			m_mthSetControl(m_dtcDate);
			m_mthSetControl(m_dtcTime);
			m_mthSetControl(m_dtcT);			
			m_mthSetControl(m_dtcHR);
			m_mthSetControl(m_dtcP);
			m_mthSetControl(m_dtcR);
			m_mthSetControl(m_dtcBp);
			m_mthSetControl(m_dtcCVP);
			m_mthSetControl(m_dtcBloodSugar);
			m_mthSetControl(m_dtcConsciousness);
			m_mthSetControl(m_dtcPupilSizeLeft);
			m_mthSetControl(m_dtcPupilSizeRight);
			m_mthSetControl(m_dtcReflectLeft);
			m_mthSetControl(m_dtcReflectRight);
			m_mthSetControl(m_dtcMuscle);
			m_mthSetControl(m_dtcDrugName);
			m_mthSetControl(m_dtcDrugDosage);
			m_mthSetControl(m_dtcDrugQty);
			m_mthSetControl(m_dtcStomachDirection);
			m_mthSetControl(m_dtcStomachProperty);
			m_mthSetControl(m_dtcStomachQuantity);
			m_mthSetControl(m_dtcInOralType);
			m_mthSetControl(m_dtcInOralDrugName);
			m_mthSetControl(m_dtcInOralProperty);
			m_mthSetControl(m_dtcInOralQty);
			m_mthSetControl(m_dtcInTotalQty);
			m_mthSetControl(m_dtcPeeDirection);
			m_mthSetControl(m_dtcPeeProperty);
			m_mthSetControl(m_dtcPeeQuantity);
			m_mthSetControl(m_dtcDefecateProperty);
			m_mthSetControl(m_dtcDefecateQuantity);
			m_mthSetControl(m_dtcDefecateTimes);
			m_mthSetControl(m_dtcLeadDirection);
			m_mthSetControl(m_dtcLeadProperty);
			m_mthSetControl(m_dtcLeadQuantity);
			m_mthSetControl(m_dtcSputumProperty);
			m_mthSetControl(m_dtcSputumQuantity);
			m_mthSetControl(m_dtcSkin);
			m_mthSetControl(m_dtcCaseHistory);
			m_mthSetControl(m_dtcSign);

			m_dtcDate.ReadOnly=true;
			m_dtcTime.ReadOnly=true;
			m_dtcT.ReadOnly=true;
			m_dtcHR.ReadOnly=true;
			m_dtcP.ReadOnly=true;
			m_dtcR.ReadOnly=true;
			m_dtcBp.ReadOnly=true;
			m_dtcCVP.ReadOnly=true;
			m_dtcBloodSugar.ReadOnly=true;
			m_dtcConsciousness.ReadOnly=true;
			m_dtcPupilSizeLeft.ReadOnly=true;
			m_dtcPupilSizeRight.ReadOnly=true;
			m_dtcReflectLeft.ReadOnly=true;
			m_dtcReflectRight.ReadOnly=true;
			m_dtcMuscle.ReadOnly=true;
			m_dtcDrugName.ReadOnly=true;
			m_dtcDrugDosage.ReadOnly=true;
			m_dtcDrugQty.ReadOnly=true;
			m_dtcStomachDirection.ReadOnly=true;
			m_dtcStomachProperty.ReadOnly=true;
			m_dtcStomachQuantity.ReadOnly=true;
			m_dtcInOralType.ReadOnly=true;
			m_dtcInOralDrugName.ReadOnly=true;
			m_dtcInOralProperty.ReadOnly=true;
			m_dtcInOralQty.ReadOnly=true;
			m_dtcInTotalQty.ReadOnly=true;
			m_dtcPeeDirection.ReadOnly=true;
			m_dtcPeeProperty.ReadOnly=true;
			m_dtcPeeQuantity.ReadOnly=true;
			m_dtcDefecateProperty.ReadOnly=true;
			m_dtcDefecateTimes.ReadOnly=true;
			m_dtcDefecateQuantity.ReadOnly=true;
			m_dtcLeadDirection.ReadOnly=true;
			m_dtcLeadProperty.ReadOnly=true;
			m_dtcLeadQuantity.ReadOnly=true;
			m_dtcSputumProperty.ReadOnly=true;
			m_dtcSputumQuantity.ReadOnly=true;
			m_dtcSkin.ReadOnly=true;
			m_dtcCaseHistory.ReadOnly=true;
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

		//���Xml��ժ��Alex��
		private void m_mthSetInOralInfo(string p_strInOralAll,string p_strInOralXmlAll)
		{
			System.Xml.XmlDocument doc1 = new System.Xml.XmlDocument();
			doc1.LoadXml(p_strInOralAll);
			System.Xml.XmlNode root1 = doc1.FirstChild;
			doc1.LoadXml(p_strInOralXmlAll);
			System.Xml.XmlNode root2 = doc1.FirstChild;

			int intInOralLength = root1.ChildNodes.Count;
			if(intInOralLength<=0)
				return;
			
			clsDSTRichTextBoxValue objValue;
			for(int i=0;i<intInOralLength;i++)
			{
				objValue = new clsDSTRichTextBoxValue();
				objValue.m_strText = root1.ChildNodes[i].InnerText;
				objValue.m_strDSTXml = root1.ChildNodes[i].InnerXml;
				
			}
		}
		
		
		private string m_strMakeXml()
		{
			m_objXmlMemStream.SetLength(0);

			m_objXmlWriter.WriteStartDocument();	
			m_objXmlWriter.WriteStartElement("Field");

			
			m_objXmlWriter.WriteStartElement("Item");
			m_objXmlWriter.WriteAttributeString("ITEMTEXT","abc");
			m_objXmlWriter.WriteAttributeString("ITEMXML","<Field />");

			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteStartElement("Item");
			m_objXmlWriter.WriteEndElement();

			m_objXmlWriter.WriteEndElement();			
			m_objXmlWriter.WriteEndDocument();

			m_objXmlWriter.Flush();

			return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(),39*2,(int)m_objXmlMemStream.Length-39*2);
		}
		
		private void m_mthRead(string p_strXml)
		{
			XmlTextReader objReader = new XmlTextReader(p_strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;
			
			int intIndex = 0;
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.Name == "Field")
						{
							//							objInfoArr[intIndex] = new clsThreeMeasureRecordInfo();
							//							objInfoArr[intIndex].m_strInPatientID = objReader.GetAttribute("ITEMTEXT");
							
							intIndex++;
						}
						break;
				}
			}
		}

		private int m_intGetLength(string p_strContent)
		{
			System.Xml.XmlDocument doc1 = new System.Xml.XmlDocument();
			doc1.LoadXml(p_strContent);
			System.Xml.XmlNode root1  = doc1.FirstChild;
			
			return root1.ChildNodes.Count;
		}

		private int m_intGetCaseLength(string p_strCase,string p_strCaseXml)
		{
			int intCharPerLine = 10;
			
			string [] strTextArr,strXmlArr;			

			com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(p_strCase,p_strCaseXml,intCharPerLine,out strTextArr,out strXmlArr);
			return strTextArr.Length;
		}
			
		/// <summary>
		/// ͨ�����XML��ȡ��ǰ��¼������޸ļ�¼�Ķ������ҩ���ͼ���
		/// </summary>
		/// <param name="p_strDrugNameAll">ҩ��</param>
		/// <param name="p_strDrugNameXMLAll">ҩ��XML��ʽ</param>
		/// <param name="p_strDrugDosageAll">����</param>
		/// <param name="p_strDrugDosageXMLAll">����XML��ʽ</param>
		/// <returns></returns>
		private clsICUIntensiveTendInLiquidContent[] m_objGetInLiquidArr(string p_strDrugNameAll,string p_strDrugNameXMLAll,string p_strDrugDosageAll,string p_strDrugDosageXMLAll)
		{
			System.Xml.XmlDocument doc1 = new System.Xml.XmlDocument();
			doc1.LoadXml(p_strDrugNameAll);
			System.Xml.XmlNode root1  = doc1.FirstChild;
			doc1.LoadXml(p_strDrugNameXMLAll);
			System.Xml.XmlNode root2  = doc1.FirstChild;
			doc1.LoadXml(p_strDrugDosageAll);
			System.Xml.XmlNode root3  = doc1.FirstChild;
			doc1.LoadXml(p_strDrugDosageXMLAll);
			System.Xml.XmlNode root4  = doc1.FirstChild;

			int intInLiquidLength = root1.ChildNodes.Count;
			if(intInLiquidLength <= 0)
				return null;
			
			clsICUIntensiveTendInLiquidContent[] objContentArr = new clsICUIntensiveTendInLiquidContent[intInLiquidLength];
			for(int i1=0;i1<intInLiquidLength;i1++)
			{				
				objContentArr[i1] = new clsICUIntensiveTendInLiquidContent();
				objContentArr[i1].m_strDrugName = root1.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strDrugNameXML = root2.ChildNodes[i1].InnerXml;
				objContentArr[i1].m_strDrugDosage = root3.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strDrugDosageXML = root4.ChildNodes[i1].InnerXml;
			}
			return objContentArr;
		}

		/// <summary>
		/// ͨ�����XML��ȡ��ǰ��¼������޸ļ�¼�Ķ�οڷ�
		/// </summary>
		/// <param name="p_strInOralAll">�ڷ�</param>
		/// <param name="p_strInOralXML">�ڷ�XML��ʽ</param>
		/// <returns></returns>
		private clsICUIntensiveTendInOralContent[] m_objGetInOralArr(clsICUIntensiveTendContent p_objContent)
		{
			if(p_objContent==null)return null;

			System.Xml.XmlDocument doc1 = new System.Xml.XmlDocument();
			doc1.LoadXml(p_objContent.m_strInOral);
			System.Xml.XmlNode root1  = doc1.FirstChild;
			doc1.LoadXml(p_objContent.m_strInOralXML);
			System.Xml.XmlNode root2  = doc1.FirstChild;

			doc1.LoadXml(p_objContent.m_strInOralType);
			System.Xml.XmlNode root3  = doc1.FirstChild;
			doc1.LoadXml(p_objContent.m_strInOralTypeXML);
			System.Xml.XmlNode root4  = doc1.FirstChild;

			doc1.LoadXml(p_objContent.m_strInOralProperty);
			System.Xml.XmlNode root5  = doc1.FirstChild;
			doc1.LoadXml(p_objContent.m_strInOralPropertyXML);
			System.Xml.XmlNode root6  = doc1.FirstChild;

			doc1.LoadXml(p_objContent.m_strInOralQuantity);
			System.Xml.XmlNode root7  = doc1.FirstChild;
			doc1.LoadXml(p_objContent.m_strInOralQuantityXML);
			System.Xml.XmlNode root8  = doc1.FirstChild;

			
			int intInOralLength = root1.ChildNodes.Count;
			if(intInOralLength <= 0)
				return null;
			
			clsICUIntensiveTendInOralContent[] objContentArr = new clsICUIntensiveTendInOralContent[intInOralLength];
			for(int i1=0;i1<intInOralLength;i1++)
			{				
				objContentArr[i1] = new clsICUIntensiveTendInOralContent();
				objContentArr[i1].m_strInOral = root1.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strInOralXML = root2.ChildNodes[i1].InnerXml;

				objContentArr[i1].m_strInOralType = root3.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strInOralTypeXML = root4.ChildNodes[i1].InnerXml;

				objContentArr[i1].m_strInOralProperty = root5.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strInOralPropertyXML = root6.ChildNodes[i1].InnerXml;

				objContentArr[i1].m_strInOralQuantity = root7.ChildNodes[i1].InnerText;
				objContentArr[i1].m_strInOralQuantityXML = root8.ChildNodes[i1].InnerXml;
			}
			return objContentArr;
		}
	
		private int m_intGetSkinLength(string p_strSkin,string p_strSkinXml)
		{
			int intCharPerLine = 5;
			
			string [] strTextArr,strXmlArr;			

			com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(p_strSkin,p_strSkinXml,intCharPerLine,out strTextArr,out strXmlArr);
			return strTextArr.Length;
		}

		/// <summary>
		///  ��ȡ��ӵ�DataTable������
		/// </summary>
		/// <param name="p_objTransDataInfo"></param>
		/// <returns></returns>
		protected override object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			clsICUIntensiveTendDataInfo objDataInfo;
			object [][] objData;
			ArrayList objReturnData=new ArrayList();
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
	
			if(p_objTransDataInfo.m_intFlag != (int)enmRecordsType.ICUIntensiveTend)
			{
				return m_objGetPerDateSummaryRecordsValueArr(p_objTransDataInfo);
			}

			objDataInfo = (clsICUIntensiveTendDataInfo)p_objTransDataInfo;

			#region ��ȡ��ӵ�DataTable������

            int intSingleTypeCount = objDataInfo.m_objTransDataArr.Length;

            //string strDrugName = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strDrugName;
            //string strDrugNameXML = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strDrugNameXML;
            //string strDrugDosage = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strDrugDosage;
            //string strDrugDosageXML = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strDrugDosageXML;
			
            //int intDrugCount = 0;
            //clsICUIntensiveTendInLiquidContent [] objInLiquidArr = m_objGetInLiquidArr(strDrugName,strDrugNameXML,strDrugDosage,strDrugDosageXML);
            //if(objInLiquidArr!=null)
            //    intDrugCount = objInLiquidArr.Length;
			
            //clsICUIntensiveTendContent objCont=objDataInfo.m_objTransDataArr[intSingleTypeCount-1];
            //int intInOralCount = 0;
            //clsICUIntensiveTendInOralContent [] objInOralArr = m_objGetInOralArr(objCont);
            //if(objInOralArr!=null)
            //    intInOralCount = objInOralArr.Length;
			

            //string strSkin = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strSkin;
            //string strSkinXML = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strSkinXML;
            //string[] strSkinTextArr,strSkinXmlArr;
            ////com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(strSkin, strSkinXML, 13, out strSkinTextArr, out strSkinXmlArr);
            //com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strSkin, strSkinXML, 24, out strSkinTextArr, out strSkinXmlArr);
            //int intSkinCount = strSkinTextArr.Length;

            //string strCase = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strCaseHistory_Last;
            //string strCaseXML = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strCaseHistoryXML;
            //string[] strCaseTextArr,strCaseXmlArr;
            ////com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(strCase,strCaseXML,13,out strCaseTextArr,out strCaseXmlArr);
            //com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strCase, strCaseXML, 24, out strCaseTextArr, out strCaseXmlArr);
            //int intCaseCount = strCaseTextArr.Length;

            //int intMaxCount = intSingleTypeCount;
            //if(intMaxCount<intDrugCount)
            //    intMaxCount=intDrugCount;
            //if(intMaxCount<intInOralCount)
            //    intMaxCount=intInOralCount;
            //if(intMaxCount<intSkinCount)
            //    intMaxCount=intSkinCount;
            //if(intMaxCount<intCaseCount)
            //    intMaxCount=intCaseCount;

            //if(intMaxCount == 0)
            //    intMaxCount = 1;

            //objData = new object[intMaxCount][]; 
			bool blnIsShowTime = false;

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

			for(int i=0;i<intSingleTypeCount;i++)
			{
                //objData[i] = new object[45];   
			
				clsICUIntensiveTendContent objCurrent = (i<intSingleTypeCount)?objDataInfo.m_objTransDataArr[i]:null;
				clsICUIntensiveTendContent objNext = (i >= intSingleTypeCount-1)?null:objDataInfo.m_objTransDataArr[i+1];

				//����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
				if(objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                    && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
				{
					if(i == 0)
						blnIsShowTime = true;
					TimeSpan tsModify =  objNext.m_dtmModifyDate-objCurrent.m_dtmModifyDate;
					if((int)tsModify.TotalHours < intCanModifyTime)
						continue;
				}

                string strDrugName = objDataInfo.m_objTransDataArr[i].m_strDrugName;
                string strDrugNameXML = objDataInfo.m_objTransDataArr[i].m_strDrugNameXML;
                string strDrugDosage = objDataInfo.m_objTransDataArr[i].m_strDrugDosage;
                string strDrugDosageXML = objDataInfo.m_objTransDataArr[i].m_strDrugDosageXML;

                int intDrugCount = 0;
                clsICUIntensiveTendInLiquidContent[] objInLiquidArr = m_objGetInLiquidArr(strDrugName, strDrugNameXML, strDrugDosage, strDrugDosageXML);
                if (objInLiquidArr != null)
                    intDrugCount = objInLiquidArr.Length;

                clsICUIntensiveTendContent objCont = objDataInfo.m_objTransDataArr[i];
                int intInOralCount = 0;
                clsICUIntensiveTendInOralContent[] objInOralArr = m_objGetInOralArr(objCont);
                if (objInOralArr != null)
                    intInOralCount = objInOralArr.Length;


                string strSkin = objDataInfo.m_objTransDataArr[i].m_strSkin;
                string strSkinXML = objDataInfo.m_objTransDataArr[i].m_strSkinXML;
                string[] strSkinTextArr, strSkinXmlArr;
                com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strSkin, strSkinXML, 24, out strSkinTextArr, out strSkinXmlArr);
                int intSkinCount = strSkinTextArr.Length;

                string strCase = objDataInfo.m_objTransDataArr[i].m_strCaseHistory_Last;
                string strCaseXML = objDataInfo.m_objTransDataArr[i].m_strCaseHistoryXML;
                string[] strCaseTextArr, strCaseXmlArr;
                com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strCase, strCaseXML, 24, out strCaseTextArr, out strCaseXmlArr);
                int intCaseCount = strCaseTextArr.Length;

                int intMaxCount = Math.Max(Math.Max(intDrugCount, intInOralCount), Math.Max(intSkinCount, intCaseCount));
                if (intMaxCount == 0)
                    intMaxCount = 1;
                objData = new object[intMaxCount][];

                for (int j = 0; j < intMaxCount; j++)
                {
                    objData[j] = new object[45];
                    if (j == 0 || blnIsShowTime)
                    {
                        //ֻ�ڵ�һ�м�¼����������Ϣ
                        objData[j][0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
                        objData[j][1] = (int)enmRecordsType.ICUIntensiveTend;//��ż�¼���͵�intֵ
                        objData[j][2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[j][3] = objDataInfo.m_objTransDataArr[objDataInfo.m_objTransDataArr.Length - 1].m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   


                        if (objCurrent.m_dtmCreateDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[j][4] = objCurrent.m_dtmCreateDate.Date.ToString("yyyy-MM-dd 00:00:00");//�����ַ���
                        }
                        if (objCurrent.m_dtmCreateDate.ToString("HH:mm") != m_dtmPreRecordDate.ToString("HH:mm"))
                        {
                            //objData[i][5] = objCurrent.m_dtmCreateDate.ToString("HH:mm:ss");//ʱ���ַ���
                            objData[j][5] = objCurrent.m_dtmCreateDate.ToString("HH:mm");//ʱ���ַ���
                        }
                        m_dtmPreRecordDate = objCurrent.m_dtmCreateDate;
                        blnIsShowTime = false;
                    }


                    if (j == 0)
                    {
                        //��۲���Ŀ��ֵ��������Һ�����ڷ���Ƥ������Ͳ����¼��				
                        strText = objCurrent.m_strT_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strT_Last != objCurrent.m_strT_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strT_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][6] = objclsDSTRichTextBoxValue;//T

                        strText = objCurrent.m_strHR_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strHR_Last != objCurrent.m_strHR_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strHR_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][7] = objclsDSTRichTextBoxValue;//HR

                        strText = objCurrent.m_strP_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strP_Last != objCurrent.m_strP_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strP_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][8] = objclsDSTRichTextBoxValue;//P

                        strText = objCurrent.m_strR_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strR_Last != objCurrent.m_strR_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strR_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][9] = objclsDSTRichTextBoxValue;//R

                        strText = objCurrent.m_strBp_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strBp_Last != objCurrent.m_strBp_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strBp_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][10] = objclsDSTRichTextBoxValue;//Bp

                        strText = objCurrent.m_strCVP_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strCVP_Last != objCurrent.m_strCVP_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strCVP_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][11] = objclsDSTRichTextBoxValue;//CVP

                        strText = objCurrent.m_strBloodSugar_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strBloodSugar_Last != objCurrent.m_strBloodSugar_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strBloodSugar_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][12] = objclsDSTRichTextBoxValue;//Ѫ��

                        strText = objCurrent.m_strConsciousness_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strConsciousness_Last != objCurrent.m_strConsciousness_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strConsciousness_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][13] = objclsDSTRichTextBoxValue;//��ʶ

                        strText = objCurrent.m_strPupilSizeLeft_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPupilSizeLeft_Last != objCurrent.m_strPupilSizeLeft_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPupilSizeLeft_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][14] = objclsDSTRichTextBoxValue;//ͫ����

                        strText = objCurrent.m_strPupilSizeRight_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPupilSizeRight_Last != objCurrent.m_strPupilSizeRight_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPupilSizeRight_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][15] = objclsDSTRichTextBoxValue;//ͫ����

                        strText = objCurrent.m_strReflectLeft_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strReflectLeft_Last != objCurrent.m_strReflectLeft_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strReflectLeft_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][16] = objclsDSTRichTextBoxValue;//����(��)

                        strText = objCurrent.m_strReflectRight_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strReflectRight_Last != objCurrent.m_strReflectRight_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strReflectRight_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][17] = objclsDSTRichTextBoxValue;//����(��)

                        strText = objCurrent.m_strPower_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPower_Last != objCurrent.m_strPower_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPower_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][18] = objclsDSTRichTextBoxValue;//����

                        //����Һ��-ҩ��������2��Ϊһ���Կ��ǻ������룬�����ڴ˸�ֵ

                        strText = objCurrent.m_strTransfusionTotal_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strTransfusionTotal_Last != objCurrent.m_strTransfusionTotal_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strTransfusionTotal_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][21] = objclsDSTRichTextBoxValue;//����Һ������

                        if (objCurrent.m_strStomachPipe_Last == "")
                        {
                            strText = objCurrent.m_strStomachDirection_Last;
                        }
                        else
                        {
                            strText = objCurrent.m_strStomachDirection_Last + "(" + objCurrent.m_strStomachPipe_Last + ")";
                        }
                        strXml = "<root />";
                        if (objNext != null && (objNext.m_strStomachDirection_Last != objCurrent.m_strStomachDirection_Last || objNext.m_strStomachPipe_Last != objCurrent.m_strStomachPipe_Last))/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            //m_strGetDSTTextXML��ɾ���ߣ�ʵ����ֻ�Ǹ�����һ����ɾ���ߵĳ��ȣ����������Դ����Ҫ����ɾ���ߵĳ��ȣ���"abc(bcd)"����ʱ����Ϊ8
                            strXml = m_strGetDSTTextXML(objCurrent.m_strStomachDirection_Last + "(" + objCurrent.m_strStomachPipe_Last + ")", objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }

                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;//ctlRichTextBox.s_strCombineXml(new string[]{strXml,strXML1,strStomachPipeXml,strXML2});
                        objData[j][22] = objclsDSTRichTextBoxValue;//θ��ͨ��


                        strText = objCurrent.m_strStomachProperty_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strStomachProperty_Last != objCurrent.m_strStomachProperty_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strStomachProperty_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][23] = objclsDSTRichTextBoxValue;//θ������

                        strText = objCurrent.m_strStomachQuantity_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strStomachQuantity_Last != objCurrent.m_strStomachQuantity_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strStomachQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][24] = objclsDSTRichTextBoxValue;//θ����

                        //�ڷ�-���͡�ҩ�������ʡ���4��Ϊһ���Կ��ǻ������룬�����ڴ˸�ֵ
                        strText = objCurrent.m_strTakeFoodTotal_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strTakeFoodTotal_Last != objCurrent.m_strTakeFoodTotal_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strTakeFoodTotal_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][29] = objclsDSTRichTextBoxValue;//��ʳ������


                        strText = objCurrent.m_strPeeDirection_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPeeDirection_Last != objCurrent.m_strPeeDirection_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPeeDirection_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][30] = objclsDSTRichTextBoxValue;//���ͨ��

                        strText = objCurrent.m_strPeeProperty_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPeeProperty_Last != objCurrent.m_strPeeProperty_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPeeProperty_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][31] = objclsDSTRichTextBoxValue;//�������

                        strText = objCurrent.m_strPeeQuantity_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strPeeQuantity_Last != objCurrent.m_strPeeQuantity_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strPeeQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][32] = objclsDSTRichTextBoxValue;//�����


                        strText = objCurrent.m_strDefecateProperty_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strDefecateProperty_Last != objCurrent.m_strDefecateProperty_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strDefecateProperty_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][33] = objclsDSTRichTextBoxValue;//�������    

                        strText = objCurrent.m_strDefecateTimes_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strDefecateTimes_Last != objCurrent.m_strDefecateTimes_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strDefecateTimes_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][34] = objclsDSTRichTextBoxValue;//������  

                        strText = objCurrent.m_strDefecateQuantity_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strDefecateQuantity_Last != objCurrent.m_strDefecateQuantity_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strDefecateQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][35] = objclsDSTRichTextBoxValue;//�����    

                        if (objCurrent.m_strLeadPipe_Last == "")
                        {
                            strText = objCurrent.m_strLeadDirection_Last;
                        }
                        else
                        {
                            strText = objCurrent.m_strLeadDirection_Last + "(" + objCurrent.m_strLeadPipe_Last + ")";
                        }
                        strXml = "<root />";
                        if (objNext != null && (objNext.m_strLeadDirection_Last != objCurrent.m_strLeadDirection_Last || objNext.m_strLeadPipe_Last != objCurrent.m_strLeadPipe_Last))/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strLeadDirection_Last + "(" + objCurrent.m_strLeadPipe_Last + ")", objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][36] = objclsDSTRichTextBoxValue;//������ͨ��    

                        strText = objCurrent.m_strLeadProperty_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strLeadProperty_Last != objCurrent.m_strLeadProperty_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strStomachQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][37] = objclsDSTRichTextBoxValue;//����������    

                        strText = objCurrent.m_strLeadQuantity_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strLeadQuantity_Last != objCurrent.m_strLeadQuantity_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strLeadQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][38] = objclsDSTRichTextBoxValue;//��������    

                        strText = objCurrent.m_strSputumProperty_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strSputumProperty_Last != objCurrent.m_strSputumProperty_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strSputumProperty_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][39] = objclsDSTRichTextBoxValue;//̵����    

                        strText = objCurrent.m_strSputumQuantity_Last;
                        strXml = "<root />";
                        if (objNext != null && objNext.m_strSputumQuantity_Last != objCurrent.m_strSputumQuantity_Last)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                        {
                            strXml = m_strGetDSTTextXML(objCurrent.m_strSputumQuantity_Last, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                        }
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][40] = objclsDSTRichTextBoxValue;//̵��
                        #region DSL���
                        m_strCurrentOpenDate = objCurrent.m_dtmCreateDate.ToString();
                        m_strCreateUserID = objCurrent.m_strModifyUserID;
                        #endregion DSL���
                    }
                    else
                    {
                        //����ֵ
                        m_strCurrentOpenDate = "";
                        m_strCreateUserID = "";
                    }

                    if (j < intDrugCount)
                    {
                        //����Һ����ֵ
                        strText = objInLiquidArr[j].m_strDrugName;
                        strXml = objInLiquidArr[j].m_strDrugNameXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][19] = objclsDSTRichTextBoxValue;//����ҩ��

                        strText = objInLiquidArr[j].m_strDrugDosage;
                        strXml = objInLiquidArr[j].m_strDrugDosageXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][20] = objclsDSTRichTextBoxValue;//�������			

                    }
                    else
                    {
                        //����ֵ
                    }

                    if (j < intInOralCount)
                    {
                        //�ڷ����͸�ֵ
                        strText = objInOralArr[j].m_strInOralType;
                        strXml = objInOralArr[j].m_strInOralTypeXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][25] = objclsDSTRichTextBoxValue;

                        //�ڷ�ҩ����ֵ
                        strText = objInOralArr[j].m_strInOral;
                        strXml = objInOralArr[j].m_strInOralXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][26] = objclsDSTRichTextBoxValue;

                        //�ڷ����ʸ�ֵ
                        strText = objInOralArr[j].m_strInOralProperty;
                        strXml = objInOralArr[j].m_strInOralPropertyXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][27] = objclsDSTRichTextBoxValue;

                        //�ڷ�����ֵ
                        strText = objInOralArr[j].m_strInOralQuantity;
                        strXml = objInOralArr[j].m_strInOralQuantityXML;
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][28] = objclsDSTRichTextBoxValue;
                    }
                    else
                    {
                        //����ֵ
                    }

                    if (j < intSkinCount)
                    {
                        //Ƥ�������ֵ
                        strText = strSkinTextArr[j];
                        strXml = strSkinXmlArr[j];
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][41] = objclsDSTRichTextBoxValue;
                    }
                    else
                    {
                        //����ֵ
                    }

                    if (j < intCaseCount)
                    {
                        //�����¼��ֵ
                        //Ƥ�������ֵ
                        strText = strCaseTextArr[j];
                        strXml = strCaseXmlArr[j];
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[j][42] = objclsDSTRichTextBoxValue;
                    }
                    else
                    {
                        //����ֵ
                    }
                    if (j == intMaxCount - 1)
                        objData[j][43] = objDataInfo.m_objTransDataArr[0].m_strModifyUserName;//ǩ��
                    //objData[i][43] = objDataInfo.m_objTransDataArr[i].m_strModifyUserName;//ǩ��
                    //else
                    //{
                    //    if (objData[intMaxCount - 1] == null)
                    //        objData[intMaxCount - 1] = new object[45]; 
                    //    objData[intMaxCount - 1][43] = objDataInfo.m_objTransDataArr[intSingleTypeCount - 1].m_strModifyUserName;//ǩ��
                    //}
                    if (objCurrent == null)
                        objData[j][44] = "";
                    else
                        objData[j][44] = objCurrent.m_strCreateUserID;

                    objReturnData.Add(objData[j]);
                }
			}	
//			objData[intMaxCount-1][43] = objDataInfo.m_objTransDataArr[intSingleTypeCount-1].m_strModifyUserName;//ǩ��
			
			object[][] m_objRe=new object[objReturnData.Count][];

			for(int m=0;m<objReturnData.Count ;m++)
				m_objRe[m]=(object[])objReturnData[m];
			return m_objRe;
			
			#endregion
		}

		private object[][] m_objGetPerDateSummaryRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
		{
			#region  ��ȡ��ӵ�DataTable��ͳ�����ݣ�����ͳ�ƣ�
			clsICUIntensiveTendDataInfo objDataInfo;
			object [][] objData;
			string strText,strXml;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;

			objDataInfo = (clsICUIntensiveTendDataInfo)p_objTransDataInfo;
			//û��ͳ������ʱ����ʾͳ�Ƶ�����
			if(!(objDataInfo.m_objICUSummary.m_intOutDefecateQuantity_Total != 0 || objDataInfo.m_objICUSummary.m_intOutLeadQuantity_Total != 0 || objDataInfo.m_objICUSummary.m_intOutPeeQuantity_Total != 0
				|| objDataInfo.m_objICUSummary.m_intSputum != 0 || objDataInfo.m_objICUSummary.m_intSputumQuantity_Total != 0 || objDataInfo.m_objICUSummary.m_intTakeFood_Total != 0
				|| objDataInfo.m_objICUSummary.m_intTotal_In != 0 || objDataInfo.m_objICUSummary.m_intTotal_Out != 0 || objDataInfo.m_objICUSummary.m_intTransfusion_Total != 0))
			{
				return null;
			}
			//objData��������
			objData = new object[3][];
			//���һ���ǿ���
			objData[2] = new string[44];
			objData[0] = new object[44];   
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
			objclsDSTRichTextBoxValue.m_strText=strText;						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][18] = objclsDSTRichTextBoxValue;//�Թⷴ�����λ��

			strText = "����";
			strXml = "<root />";

			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][19] = objclsDSTRichTextBoxValue;//�Թⷴ���ҵ�λ��

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][20] = objclsDSTRichTextBoxValue;//����Һ��ҩ����λ��
		
			strText = objDataInfo.m_objICUSummary.m_intTransfusion_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][21] = objclsDSTRichTextBoxValue;//����Һ��������λ��
		
			strText = objDataInfo.m_objICUSummary.m_intTakeFood_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][29] = objclsDSTRichTextBoxValue;//��ʳ��������λ��
		
			strText = objDataInfo.m_objICUSummary.m_intOutPeeQuantity_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][32] = objclsDSTRichTextBoxValue;//�������λ��
		
			strText = objDataInfo.m_objICUSummary.m_intOutDefecateQuantity_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][35] = objclsDSTRichTextBoxValue;//�������λ��
		
			strText = objDataInfo.m_objICUSummary.m_intOutLeadQuantity_Total.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[0][38] = objclsDSTRichTextBoxValue;//����������λ��

			objData[1] = new object[44];

			if(m_blnIfLastSummary)
				strText = "�Ϲ�";
			else
				strText = "����";

			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][18] = objclsDSTRichTextBoxValue;//�Թⷴ�����λ��

			strText = "����";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][19] = objclsDSTRichTextBoxValue;//�Թⷴ���ҵ�λ��

			strText = "�ܼ�:";
			strXml = "<root />";
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][20] = objclsDSTRichTextBoxValue;//����Һ��ҩ����λ��

			strText = objDataInfo.m_objICUSummary.m_intTotal_In.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][21] = objclsDSTRichTextBoxValue;//����Һ��������λ��

			strText = objDataInfo.m_objICUSummary.m_intTotal_Out.ToString();
			strXml = "<root />";
			strXml = m_strGetDSTTextXML(strText,MDIParent.OperatorID,MDIParent.OperatorName);
			objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			objclsDSTRichTextBoxValue.m_strText=(strText == "0" ? "" : strText);						
			objclsDSTRichTextBoxValue.m_strDSTXml=strXml;	
			objclsDSTRichTextBoxValue.m_blnUnderDST = true;
			objData[1][32] = objclsDSTRichTextBoxValue;//�������λ��
			
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

		/// <summary>
		///  ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		protected override clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
			object[] p_objDataArr)
		{
			//���� p_intRecordType ��ȡ��Ӧ�� clsICUIntensiveTendContent
			clsICUIntensiveTendContent objContent = new clsICUIntensiveTendContent();

			int intSelectedRecordStartRow =m_dtgRecordDetail.CurrentCell.RowNumber;
			objContent.m_strCreateUserID = (m_dtbRecords.Rows[intSelectedRecordStartRow][44]).ToString();
		
			objContent.m_dtmCreateDate = DateTime.Parse((string)p_objDataArr[0]);
			objContent.m_dtmOpenDate = DateTime.Parse((string)p_objDataArr[2]);
			objContent.m_dtmModifyDate = DateTime.Parse((string)p_objDataArr[3]);
            objContent.m_strCreateUserID = (string)p_objDataArr[44];
		
			return objContent;
		}

		/// <summary>
		///  ��ȡ���̼�¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsRecordsDomain m_objGetRecordsDomain()
		{
            return new clsRecordsDomain(enmRecordsType.ICUIntensiveTend);
		}

		protected override infPrintRecord m_objGetPrintTool()
		{			
			return new clsICUIntensiveTendPrintTool();
		}

		protected override void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			PageSetupDialog psd=new PageSetupDialog();
			try
			{
				if (m_pdcPrintDocument.DefaultPageSettings == null) 
				{
					m_pdcPrintDocument.DefaultPageSettings =  new PageSettings();
				}	
				m_pdcPrintDocument.DefaultPageSettings.Landscape=true;
				m_pdcPrintDocument.DefaultPageSettings.PaperSize=new PaperSize("A3",1150,1620);
			
				psd.PageSettings = m_pdcPrintDocument.DefaultPageSettings ;
				
				if(	psd.ShowDialog() !=DialogResult.Cancel)
				{
					m_pdcPrintDocument.DefaultPageSettings.Landscape=psd.PageSettings.Landscape;
					m_pdcPrintDocument.DefaultPageSettings.PaperSize=psd.PageSettings.PaperSize;
				}
				else 
				{
					return;
				}
				
				if(m_blnDirectPrint)
				{
					m_pdcPrintDocument.Print();
				}
				else
				{
					((clsICUIntensiveTendPrintTool)objPrintTool).m_mthPrintPage(m_pdcPrintDocument.DefaultPageSettings);
				
				}
			}
			catch(Exception ex)
			{
				if(ex.Message.IndexOf("No Printers installed")>=0)
					clsPublicFunction.ShowInformationMessageBox("�Ҳ�����ӡ����");
				else MessageBox.Show( ex.Message);
			}
			
//			base.m_mthStartPrint();
			
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
			m_mthAddNewRecord((int)enmDiseaseTrackType.ICUIntensiveTend);
		}

		/// <summary>
		///  ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <returns></returns>
		protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
		{
			///����Σ���ػ���¼�����Ӧ�ı༭����
			return new frmSubICUIntensiveTend();
		}

		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
		/// </summary>
		/// <param name="p_intRecordType"></param>
		protected override void m_mthAddNewRecord(int p_intRecordType)
		{
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                MDIParent.ShowInformationMessageBox("����ѡ��һ������!");
                return;
            }
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    m_frmCurrentSub.Activate();
                    m_frmCurrentSub.WindowState = FormWindowState.Normal;
                }
                return;
            }
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);
            if (frmAddNewForm == null)
                return;
			//��ӿ���
			frmAddNewForm.m_mthSetDiseaseTrackInfoForAddNew(m_objCurrentPatient);
		
//			//��ʾ����
//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
//			{
//				m_mthSetPatientFormInfo(m_objCurrentPatient);
//			}
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
		}

		protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
		{
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
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

		/// <summary>
		/// Σ���ػ���¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
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

		protected override void m_mthGetDeletedRecord(int p_intRecordType,
			DateTime p_dtmCreateRecordTime)
		{	
			//��ȡ��Ӽ�¼�Ĵ���
			frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType); 
			frmAddNewForm.m_mthSetDeletedDiseaseTrackInfo(m_objCurrentPatient,p_dtmCreateRecordTime);
		
			m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);
//			//��ʾ����
//			if(frmAddNewForm.ShowDialog() == DialogResult.Yes)
//			{				
//				m_mthSetPatientFormInfo(m_objCurrentPatient);
//			}
		}
		public override int m_IntFormID
		{
			get
			{
				return 18;
			}
		}

	}
}

