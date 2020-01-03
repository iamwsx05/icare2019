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
using com.digitalwave.Emr.Signature_gui;
namespace iCare
{
    /// <summary>
    /// һ�㻤���¼
    /// </summary>
	public class frmGeneralNurseRecord : iCare.frmDiseaseTrackBase
	{
		private System.Windows.Forms.Label lblTitle3;
		private com.digitalwave.controls.ctlRichTextBox m_txtRecordContent;
		private PinkieControls.ButtonXP cmdConfirm;
        private PinkieControls.ButtonXP cmdCancel;
        private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.MenuItem m_mniTemplature;
		private System.Windows.Forms.MenuItem m_mniPulse;
		private System.Windows.Forms.MenuItem m_mniBreath;
		private System.Windows.Forms.MenuItem m_mniPressure;
		private System.Windows.Forms.MenuItem m_mniThreeMeasure;
        private TextBox txtSign;
        private PinkieControls.ButtonXP m_cmbsign;
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;

		public frmGeneralNurseRecord()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
            //ָ����ʿ����վ��
            intFormType = 2;
			// TODO: Add any initialization after the InitializeComponent call
			m_mthSetRichTextBoxAttribInControl(this);	

            m_objSign = new clsEmrSignToolCollection();

            //����ָ��Ա��ID��
            m_objSign.m_mthBindEmployeeSign(m_cmbsign, txtSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);


			m_mthInitThreeMeasureEvent();
		}

		/// <summary>
		/// ��ʼ����ȡ�������ݵ��¼�
		/// </summary>
		private void m_mthInitThreeMeasureEvent()
		{
			for(int i = 0; i < m_mniThreeMeasure.MenuItems.Count; i++)
				m_mniThreeMeasure.MenuItems[i].Click += new EventHandler(m_mthGetNearestThreeMeasureData);
		}

		private void m_mthGetNearestThreeMeasureData(object sender,EventArgs e)
		{
			if(m_objCurrentPatient == null)
				return;

			MenuItem mniClick = (MenuItem)sender;

			string[] strResult;
			new clsThreeMeasureShareDomain().m_lngGetNearestValue(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_dtpCreateDate.Value,out strResult);

			if(strResult != null && strResult.Length == 4)
			{
				m_txtRecordContent.m_mthInsertText(strResult[mniClick.Index],m_txtRecordContent.SelectionStart);
			}

		}

		public override int m_IntFormID
		{
			get
			{
				return 4;
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
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.m_txtRecordContent = new com.digitalwave.controls.ctlRichTextBox();
            this.cmdConfirm = new PinkieControls.ButtonXP();
            this.cmdCancel = new PinkieControls.ButtonXP();
            this.m_mniThreeMeasure = new System.Windows.Forms.MenuItem();
            this.m_mniTemplature = new System.Windows.Forms.MenuItem();
            this.m_mniPulse = new System.Windows.Forms.MenuItem();
            this.m_mniBreath = new System.Windows.Forms.MenuItem();
            this.m_mniPressure = new System.Windows.Forms.MenuItem();
            this.txtSign = new System.Windows.Forms.TextBox();
            this.m_cmbsign = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(332, 84);
            this.m_trvCreateDate.Size = new System.Drawing.Size(212, 56);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(28, 120);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(112, 116);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(452, 44);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(560, 44);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(404, 44);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(512, 44);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(455, 364);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(374, 364);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(330, 362);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(671, 84);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Text = "һ�㻤���¼";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(208, 364);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(610, 9);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(31, 7);
            this.m_pnlNewBase.Size = new System.Drawing.Size(746, 60);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(744, 29);
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle3.Location = new System.Drawing.Point(28, 152);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(80, 16);
            this.lblTitle3.TabIndex = 6077;
            this.lblTitle3.Text = "��¼����:";
            // 
            // m_txtRecordContent
            // 
            this.m_txtRecordContent.AccessibleDescription = "��¼����";
            this.m_txtRecordContent.BackColor = System.Drawing.Color.White;
            this.m_txtRecordContent.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordContent.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtRecordContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtRecordContent.Location = new System.Drawing.Point(28, 176);
            this.m_txtRecordContent.m_BlnIgnoreUserInfo = false;
            this.m_txtRecordContent.m_BlnPartControl = false;
            this.m_txtRecordContent.m_BlnReadOnly = false;
            this.m_txtRecordContent.m_BlnUnderLineDST = false;
            this.m_txtRecordContent.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtRecordContent.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtRecordContent.m_IntCanModifyTime = 6;
            this.m_txtRecordContent.m_IntPartControlLength = 0;
            this.m_txtRecordContent.m_IntPartControlStartIndex = 0;
            this.m_txtRecordContent.m_StrUserID = "";
            this.m_txtRecordContent.m_StrUserName = "";
            this.m_txtRecordContent.Name = "m_txtRecordContent";
            this.m_txtRecordContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.m_txtRecordContent.Size = new System.Drawing.Size(727, 160);
            this.m_txtRecordContent.TabIndex = 100;
            this.m_txtRecordContent.Text = "";
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdConfirm.DefaultScheme = true;
            this.cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdConfirm.Hint = "";
            this.cmdConfirm.Location = new System.Drawing.Point(592, 357);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdConfirm.Size = new System.Drawing.Size(74, 32);
            this.cmdConfirm.TabIndex = 300;
            this.cmdConfirm.Text = "����(&Y)";
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCancel.DefaultScheme = true;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Hint = "";
            this.cmdCancel.Location = new System.Drawing.Point(682, 356);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCancel.Size = new System.Drawing.Size(74, 32);
            this.cmdCancel.TabIndex = 400;
            this.cmdCancel.Text = "�ر�(&C)";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // m_mniThreeMeasure
            // 
            this.m_mniThreeMeasure.Index = -1;
            this.m_mniThreeMeasure.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniTemplature,
            this.m_mniPulse,
            this.m_mniBreath,
            this.m_mniPressure});
            this.m_mniThreeMeasure.Text = "���������";
            // 
            // m_mniTemplature
            // 
            this.m_mniTemplature.Index = 0;
            this.m_mniTemplature.Text = "����";
            // 
            // m_mniPulse
            // 
            this.m_mniPulse.Index = 1;
            this.m_mniPulse.Text = "����";
            // 
            // m_mniBreath
            // 
            this.m_mniBreath.Index = 2;
            this.m_mniBreath.Text = "����";
            // 
            // m_mniPressure
            // 
            this.m_mniPressure.Index = 3;
            this.m_mniPressure.Text = "Ѫѹ";
            // 
            // txtSign
            // 
            this.txtSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSign.Enabled = false;
            this.txtSign.Location = new System.Drawing.Point(96, 362);
            this.txtSign.Name = "txtSign";
            this.txtSign.Size = new System.Drawing.Size(106, 23);
            this.txtSign.TabIndex = 10000028;
            // 
            // m_cmbsign
            // 
            this.m_cmbsign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmbsign.DefaultScheme = true;
            this.m_cmbsign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbsign.Hint = "";
            this.m_cmbsign.Location = new System.Drawing.Point(28, 356);
            this.m_cmbsign.Name = "m_cmbsign";
            this.m_cmbsign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbsign.Size = new System.Drawing.Size(64, 32);
            this.m_cmbsign.TabIndex = 5;
            this.m_cmbsign.Text = "ǩ��(&S)";
            // 
            // frmGeneralNurseRecord
            // 
            this.AccessibleDescription = "һ�㻤���¼";
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(792, 406);
            this.Controls.Add(this.txtSign);
            this.Controls.Add(this.m_cmbsign);
            this.Controls.Add(this.lblTitle3);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.m_txtRecordContent);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmGeneralNurseRecord";
            this.Text = "һ�㻤���¼";
            this.Load += new System.EventHandler(this.frmGeneralNurseRecord_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.cmdCancel, 0);
            this.Controls.SetChildIndex(this.m_txtRecordContent, 0);
            this.Controls.SetChildIndex(this.cmdConfirm, 0);
            this.Controls.SetChildIndex(this.lblTitle3, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
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
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_cmbsign, 0);
            this.Controls.SetChildIndex(this.txtSign, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// ��ȡ��ǰ�����⻤���¼��Ϣ
		/// </summary>
		/// <returns></returns>
		public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
		{
			clsGeneralNurseRecordInfo objTrackInfo = new clsGeneralNurseRecordInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;//m_objGetContentFromGUI();
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
//			objTrackInfo.m_StrTitle ="һ�㻤���¼";

			//����m_dtmRecordTime
			if(objTrackInfo.m_ObjRecordContent !=null)
			{
//				m_txtRecordTitle.Text=((clsGeneralNurseRecordRecordContent)m_objCurrentRecordContent).m_strRecordTitle;//objTrackInfo.m_StrTitle;
				m_dtpCreateDate.Value=objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
			}
			return objTrackInfo;			
		}

		/// <summary>
		/// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
		/// </summary>
		protected override void m_mthClearRecordInfo()
		{
			//��վ����¼����			
			m_txtRecordContent.m_mthClearText();
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(txtSign);
            //m_objSignTool.m_mthSetDefaulEmployee();
		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{			
			if(p_blnEnable==false)
			{
				foreach(Control control in this.Controls)
				{					
					control.Top=control.Top-105;				
				}
			
				cmdConfirm.Visible=true;
				
				this.Size=new Size(this.Size.Width, this.Size.Height-105);
				this.CenterToParent();				
			}

			this.MaximizeBox=false;
		}

		/// <summary>
		/// �Ƿ������޸������¼�ļ�¼��Ϣ��
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnableModifySub(bool p_blnEnable)
		{
			//�����¼���������,�����Ӵ������Ҫ����ʵ��
		}

		/// <summary>
		/// �����Ƿ�����޸ģ��޸����ۼ�����
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_blnReset">�Ƿ����ÿ����޸ģ��޸����ۼ�����
		///���Ϊtrue�����Լ�¼���ݣ��ѽ����������Ϊ�����ƣ�
		///������ݼ�¼���ݽ������á�
		///</param>
		protected override void m_mthSetModifyControlSub(clsTrackRecordContent p_objRecordContent,
			bool p_blnReset)
		{
			//������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
			
		}

		/// <summary>
		/// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
		/// </summary>
		/// <returns></returns>
		protected override clsTrackRecordContent m_objGetContentFromGUI()
		{
			//�������У��
            //if(m_objCurrentPatient==null || this.txtInPatientID.Text!=this.m_objCurrentPatient.m_StrHISInPatientID || txtInPatientID.Text=="")
            if (base.m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)				
				return null;
			
			//�ӽ����ȡ��ֵ
			clsGeneralNurseRecordContent objContent=new clsGeneralNurseRecordContent();
			objContent.m_dtmCreateDate=m_dtpCreateDate.Value;		
				
			objContent.m_strRecordContent_Right=m_txtRecordContent.m_strGetRightText();	
			objContent.m_strRecordContent=m_txtRecordContent.Text;
			objContent.m_strRecordContentXml=m_txtRecordContent.m_strGetXmlText();
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
            objContent.m_strCreateUserID = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR;
            objContent.m_strModifyUserID = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR;
            objContent.m_strSignName = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strLASTNAME_VCHR;
            //��ȡǩ��
            strUserIDList = "";
            strUserNameList = "";
            m_mthGetSignArr(new Control[] { txtSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
			return objContent;	
		}

		/// <summary>
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
		{
			clsGeneralNurseRecordContent objContent=(clsGeneralNurseRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��	
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.m_mthSetNewText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);
            //m_txtEmpSign.Text = objContent.m_strSignName;
            m_mthAddSignToTextBoxByEmpNo(new TextBoxBase[] { txtSign }, new string[] {objContent.m_strCreateUserID }, new bool[] { false });
		}

		protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
		{
			clsGeneralNurseRecordContent objContent=(clsGeneralNurseRecordContent)p_objContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��	
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);
		}

		/// <summary>
		/// ��ȡ�����¼�������ʵ��
		/// </summary>
		/// <returns></returns>
		protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.GeneralNurseRecord);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsGeneralNurseRecordContent objContent=(clsGeneralNurseRecordContent)p_objRecordContent;
			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
					
			m_txtRecordContent.m_mthClearText();
			m_txtRecordContent.Text=com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strRecordContent,objContent.m_strRecordContentXml);		
						
		}

		#region ��ӡ
		/// <summary>
		///  ���ô�ӡ���ݡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetPrintContent(clsTrackRecordContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ������
		}

		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		protected override void m_mthInitPrintTool()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		protected override void m_mthDisposePrintTools()
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
			//�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
		}

		/// <summary>
		/// ��ʼ��ӡ��
		/// </summary>
		protected override void m_mthStartPrint()
		{
			//ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
			if(m_blnDirectPrint)
			{
				m_pdcPrintDocument.Print();
			}
			else
			{
				PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
				ppdPrintPreview.Document = m_pdcPrintDocument;
				ppdPrintPreview.ShowDialog();
			}
		}

		/// <summary>
		/// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected override void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ������Ӵ����������ṩ����
		}

		/// <summary>
		/// ��ӡҳ
		/// </summary>
		/// <param name="p_objPrintPageArg"></param>
		protected override void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
			//��ӡ����

			//��ӡ����

			//��ӡҳβ
		}

		/// <summary>
		/// ��ӡ����ʱ�Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		protected override void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
			//���Ӵ����������ṩ����
		}
		#endregion ��ӡ
      
		// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��

			return	"һ�㻤���¼";
		}		

		private void frmGeneralNurseRecord_Load(object sender, System.EventArgs e)
		{
			this.m_dtpCreateDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
			this.m_dtpCreateDate.m_mthResetSize();

			m_txtRecordContent.Focus();
		}				

		private void cmdConfirm_Click(object sender, System.EventArgs e)
		{
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			this.Close();
		}

        public override void m_mthSetReadOnly()
        {
            m_txtRecordContent.m_BlnReadOnly = true;
            m_dtpCreateDate.Enabled = false;
            m_cmbsign.Enabled = false;
            cmdConfirm.Enabled = false;
            base.m_mthSetReadOnly();
        }
		
	}
}

