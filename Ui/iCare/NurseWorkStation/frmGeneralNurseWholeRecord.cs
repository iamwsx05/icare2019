using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.controls; 
using com.digitalwave.Emr.Signature_gui;
using com.digitalwave.emr.DigitalSign;
namespace iCare
{
	/// <summary>
	/// һ�㻤���¼(����¼��)
	/// </summary>
	public class frmGeneralNurseWholeRecord : iCare.frmDiseaseTrackBase
	{
		private System.Windows.Forms.DataGrid m_GridRecord;
		private System.Windows.Forms.Button m_cmdCancel;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.Button m_cmdSave;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmGeneralNurseWholeRecord()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

            m_ctlAreaPatientSelection.m_mthSetSubItemVisible(true, false, false,false, false);
			m_mthIniRecords();

            m_mthAreaChanged(com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea);
		}

		private DataTable m_dtRecords;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeneralNurseWholeRecord));
            this.m_GridRecord = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.m_cmdSave = new System.Windows.Forms.Button();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.m_pnlNewBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_GridRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(616, 69);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(296, 88);
            this.lblCreateDateTitle.Visible = false;
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(376, 84);
            this.m_dtpCreateDate.Visible = false;
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(696, 144);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(804, 144);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(304, 148);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(300, 116);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(476, 148);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(648, 144);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(756, 144);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(260, 70);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(308, 58);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(360, 116);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(520, 144);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(348, 144);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(308, 66);
            this.m_cboArea.Visible = false;
            this.m_cboArea.SelectedIndexChanged += new System.EventHandler(this.m_cboArea_SelectedIndexChanged);
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(480, 79);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(308, 75);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(100, 66);
            this.m_cboDept.Visible = false;
            this.m_cboDept.SelectedIndexChanged += new System.EventHandler(this.m_cboDept_SelectedIndexChanged);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(52, 70);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(732, 74);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.Location = new System.Drawing.Point(376, 68);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(156, 87);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(340, 148);
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(448, 60);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(629, 80);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(4, 7);
            this.m_pnlNewBase.Size = new System.Drawing.Size(852, 32);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(850, 1);
            this.m_ctlPatientInfo.Visible = false;
            // 
            // m_GridRecord
            // 
            this.m_GridRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_GridRecord.DataMember = "";
            this.m_GridRecord.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GridRecord.Location = new System.Drawing.Point(4, 40);
            this.m_GridRecord.Name = "m_GridRecord";
            this.m_GridRecord.Size = new System.Drawing.Size(852, 524);
            this.m_GridRecord.TabIndex = 10000004;
            this.m_GridRecord.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.m_GridRecord;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "��  ��";
            this.dataGridTextBoxColumn1.MappingName = "BedID";
            this.dataGridTextBoxColumn1.NullText = "";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "��  ��";
            this.dataGridTextBoxColumn2.MappingName = "CName";
            this.dataGridTextBoxColumn2.NullText = "";
            this.dataGridTextBoxColumn2.ReadOnly = true;
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "�����¼";
            this.dataGridTextBoxColumn3.MappingName = "NR";
            this.dataGridTextBoxColumn3.NullText = "";
            this.dataGridTextBoxColumn3.Width = 480;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "��д����";
            this.dataGridTextBoxColumn4.MappingName = "tDate";
            this.dataGridTextBoxColumn4.NullText = "";
            this.dataGridTextBoxColumn4.Width = 140;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "סԺ��";
            this.dataGridTextBoxColumn5.MappingName = "InPatientID";
            this.dataGridTextBoxColumn5.NullText = "";
            this.dataGridTextBoxColumn5.ReadOnly = true;
            this.dataGridTextBoxColumn5.Width = 0;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "סԺ����";
            this.dataGridTextBoxColumn6.MappingName = "InDate";
            this.dataGridTextBoxColumn6.ReadOnly = true;
            this.dataGridTextBoxColumn6.Width = 0;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Location = new System.Drawing.Point(680, 572);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Size = new System.Drawing.Size(76, 28);
            this.m_cmdSave.TabIndex = 10000005;
            this.m_cmdSave.Text = "����";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Location = new System.Drawing.Point(776, 572);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(76, 28);
            this.m_cmdCancel.TabIndex = 10000005;
            this.m_cmdCancel.Text = "ȡ��";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // frmGeneralNurseWholeRecord
            // 
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(860, 617);
            this.Controls.Add(this.m_GridRecord);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.m_cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGeneralNurseWholeRecord";
            this.Text = "һ�㻤���¼(����¼��)";
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_cmdCancel, 0);
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
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_cmdSave, 0);
            this.Controls.SetChildIndex(this.m_GridRecord, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_GridRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void m_cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_mthIniRecords()
		{
            if (m_dtRecords == null)
            {
                m_dtRecords = new DataTable();
                m_dtRecords.Columns.Add("BedId", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("CName", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("NR", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("tDate", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("InPatientID", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("InDate", System.Type.GetType("System.String"));
                m_dtRecords.Columns.Add("registerid", System.Type.GetType("System.String"));
            }
            m_GridRecord.DataSource = m_dtRecords;		
		}

		private void m_cboArea_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			DataTable objPatientArr;
			long lngRes=0;
			m_dtRecords.Clear();
			lngRes=	m_objDiseaseTrackDomain.m_lngGetBedPatientInfo(((clsInPatientArea)(m_cboArea.SelectedItem)).m_strAreaNewID,out objPatientArr);
			
			//lngRes=	m_objDiseaseTrackDomain.m_lngGetBedInfo(((clsInPatientArea)(m_cboArea.SelectedItem)).m_strAreaNewID,out objPatientArr);
			//m_objDepartmentManager.m_lngGetAllBedAndPatientInArea(((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_StrAreaID,out objBedArr,out objPatientArr);
			if (lngRes>0&&objPatientArr.Rows.Count>0)
			{
				m_dtRecords.Rows.Add(new string[]{"","","",""});
				m_dtRecords.Rows.Clear();
				for (int i=0;i<objPatientArr.Rows.Count;i++)
				{
					if (objPatientArr.Rows[i]["BEDID_CHR"].ToString().Trim().Length!=0)
					m_dtRecords.Rows.Add(new string[]{objPatientArr.Rows[i]["code_chr"].ToString().Trim(),objPatientArr.Rows[i]["lastname_vchr"].ToString().Trim(),"",DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��"),objPatientArr.Rows[i]["inpatientid_chr"].ToString().Trim(),objPatientArr.Rows[i]["inpatient_dat"].ToString().Trim()});
					//if (objPatientArr[i]["BEDID_CHR"]!=null)
						//m_dtRecords.Rows.Add(new string[]{objPatientArr[i].m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID,objPatientArr[i].m_StrName,"",DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��"),objPatientArr[i].m_StrInPatientID,objPatientArr[i].m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString()});
				}
			}

		}

		/// <summary>
		/// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
		/// </summary>
		/// <returns></returns>
		private clsTrackRecordContent[] m_objGetSaveData()
		{
			//�������У��
			//if(m_objCurrentPatient==null || this.txtInPatientID.Text!=this.m_objCurrentPatient.m_StrInPatientID || txtInPatientID.Text=="")				
			//	return null;
			
			//�ӽ����ȡ��ֵ
			if(m_dtRecords==null)
				return null;
			m_cmdSave.Focus();
			m_dtRecords.AcceptChanges();
			int intCount=m_dtRecords.Rows.Count;
			clsGeneralNurseRecordContent[] objContent=new clsGeneralNurseRecordContent[intCount];
//			objContent = clsTrackRecordContent[intCount];
			
			for (int i=0;i<m_dtRecords.Rows.Count;i++)
			{
				if (m_dtRecords.Rows[i]["NR"].ToString().Trim().Length==0 || m_dtRecords.Rows[i]["CName"].ToString() == "")
				{
					objContent[i]=null;
				}
				else
				{
					objContent[i]=new clsGeneralNurseRecordContent();
					objContent[i].m_dtmCreateDate=DateTime.Parse(m_dtRecords.Rows[i]["tDate"].ToString().Trim());		
				
					objContent[i].m_strRecordContent_Right=m_dtRecords.Rows[i]["NR"].ToString().Trim();	
					objContent[i].m_strRecordContent=m_dtRecords.Rows[i]["NR"].ToString().Trim();
					objContent[i].m_strRecordContentXml=ctlRichTextBox.clsXmlTool.s_strMakeXml(
						objContent[i].m_strRecordContent,MDIParent.OperatorID,MDIParent.OperatorName,Color.Red,Color.Black,DateTime.Now.ToString(),false);
					objContent[i].m_strSignName = MDIParent.OperatorName;
				}
			}

			return ((clsTrackRecordContent[])objContent);	
		}
   
        protected long m_lngAddNewRecord()
		{

			//��ȡ������ʱ��
			string strTimeNow=new clsPublicDomain().m_strGetServerTime();
		
			//�ӽ����ȡ��¼��Ϣ
			clsTrackRecordContent[] objContent = m_objGetSaveData();     
		           
			//��������ֵ����           
			if(objContent == null)
                return -1;
		    //ȷ��֤�� �˲�����Ҫ
            clsDigitalSign.SetStaticCerts();
            clsCheckSignersController objCheck = new clsCheckSignersController();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //ѭ������
			    for (int i=0;i<objContent.Length;i++)
			    {
                    //�ͷ���Դ��Ӧ�����¼�
				    if(objContent[i]!=null)
				    {
                         Application.DoEvents(); 

					    //���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
					    objContent[i].m_dtmOpenDate=DateTime.Parse(strTimeNow);
					    objContent[i].m_dtmModifyDate=DateTime.Parse(strTimeNow);

					    objContent[i].m_bytIfConfirm=0;
					    objContent[i].m_bytStatus=0;
					    objContent[i].m_dtmCreateDate=m_dtpCreateDate.Value;
					    if(m_dtRecords.Rows[i]["InDate"].ToString().Trim()=="")
					    objContent[i].m_dtmInPatientDate=DateTime.Parse("1900-01-01 01:01:01");
					    else
						    objContent[i].m_dtmInPatientDate=DateTime.Parse(m_dtRecords.Rows[i]["InDate"].ToString().Trim());
					    objContent[i].m_strConfirmReason="";
					    objContent[i].m_strConfirmReasonXML="";
					    objContent[i].m_strCreateUserID=MDIParent.OperatorID;
					    //objContent[i].m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
					    objContent[i].m_strInPatientID=m_dtRecords.Rows[i]["InPatientID"].ToString().Trim();
                        objContent[i].m_strRegisterID = m_dtRecords.Rows[i]["registerid"].ToString();

					    //ǩ��
					    objContent[i].m_strModifyUserID = MDIParent.OperatorID;

                        //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                        clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                        objSign_VO.m_strFORMID_VCHR = this.Name;
                        objSign_VO.m_strFORMRECORDID_VCHR = objContent[i].m_strInPatientID.Trim() + "-" + objContent[i].m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"); ;
                        objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                        objSign_VO.m_strRegisterId = objContent[i].m_strRegisterID;
                        if (objCheck.CheckSigner(objContent, objSign_VO,0) == -1)
                            return -1;

					    //�����¼
					    clsPreModifyInfo objModifyInfo=null;
					    long lngRes = m_objDiseaseTrackDomain.m_lngAddNewRecord(objContent[i],out objModifyInfo);
				    }

			    } 
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
          
		
			//���ؽ��
            return 1;			
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


		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
            if (m_lngAddNewRecord() > 0)
                clsPublicFunction.ShowInformationMessageBox("����ɹ���");

		}


		/// <summary>
		/// ���Ҹı�load������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_cboDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{

				DataTable objPatientArr;
				long lngRes=0;
				m_dtRecords.Clear();
				lngRes=	m_objDiseaseTrackDomain.m_lngGetBedPatientInfo(((clsDepartment)(m_cboDept.SelectedItem)).m_strDeptNewID.Trim(),out objPatientArr);
			
				if (lngRes>0&&objPatientArr.Rows.Count>0)
				{
					m_dtRecords.Rows.Add(new string[]{"","","",""});
					m_dtRecords.Rows.Clear();
					for (int i=0;i<objPatientArr.Rows.Count;i++)
					{
						if (objPatientArr.Rows[i]["BEDID_CHR"].ToString().Trim().Length!=0)
							m_dtRecords.Rows.Add(new string[]{objPatientArr.Rows[i]["code_chr"].ToString().Trim(),objPatientArr.Rows[i]["lastname_vchr"].ToString().Trim(),"",DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��"),objPatientArr.Rows[i]["inpatientid_chr"].ToString().Trim(),objPatientArr.Rows[i]["inpatient_dat"].ToString().Trim()});
					}
				}
			
			}
			catch(Exception exp)
			{
				string strError=exp.Message;
			}
		
		}
		protected override long m_lngSubAddNew()
		{
			return  m_lngAddNewRecord();
		}
		/// <summary>
		/// �Ƿ�������¼�¼�Ĳ�����true������¼�¼��false,�޸ļ�¼
		/// </summary>
		protected override bool m_BlnIsAddNew
		{
			get
			{
				return true;
			}
		}
		/// <summary>
		/// ��¼���ô��嵱ǰ״̬�����ڴ���ر�ʱ�б�����ʾ���������Ҫ������ʾ�����ظú�����
		/// </summary>
		protected override void m_mthAddFormStatusForClosingSave()
		{
			
		}

        protected override void m_mthAreaChanged(clsEmrDept_VO p_objSelectedArea)
        {
            if (p_objSelectedArea == null || m_GridRecord == null)
            {
                return;
            }

            DataTable objPatientArr;
            long lngRes = 0;
            m_dtRecords.Clear();
            lngRes = m_objDiseaseTrackDomain.m_lngGetBedPatientInfo(p_objSelectedArea.m_strDEPTID_CHR, out objPatientArr);

            if (lngRes > 0)
            {
                int intRowsCount = objPatientArr.Rows.Count;
                if (intRowsCount <= 0)
                {
                    return;
                }

                try
                {
                    m_dtRecords.Rows.Add(new string[] { "", "", "", "" });
                    m_dtRecords.Rows.Clear();
                    m_dtRecords.BeginLoadData();
                    object[] objPatient = null;
                    DataRow drTemp = null;
                    for (int i = 0; i < intRowsCount; i++)
                    {
                        drTemp = objPatientArr.Rows[i];
                        if (drTemp["BEDID_CHR"] != DBNull.Value)
                        {
                            objPatient = new object[7];
                            objPatient[0] = drTemp["code_chr"].ToString();
                            objPatient[1] = drTemp["lastname_vchr"].ToString();
                            objPatient[2] = string.Empty;
                            objPatient[3] = DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��");
                            objPatient[4] = drTemp["emrinpatientid"].ToString();
                            objPatient[5] = drTemp["emrinpatientdate"].ToString();
                            objPatient[6] = drTemp["registerid_chr"].ToString();
                            m_dtRecords.LoadDataRow(objPatient, true);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    string strEx = Ex.Message;
                }
                finally
                {
                    m_dtRecords.EndLoadData();
                }
            }
        }
	}
}
