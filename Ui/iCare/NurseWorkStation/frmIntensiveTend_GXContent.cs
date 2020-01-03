using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using iCare.iCareBaseForm;
using com.digitalwave.Emr.Signature_gui;
namespace iCare
{
    /// <summary>
    /// Σ�ػ��߻����¼(����)>>�����¼
    /// </summary>
    public class frmIntensiveTend_GXContent : frmDiseaseTrackBase
    {
        private System.Windows.Forms.Label lblRecordContentTitle;
        private PinkieControls.ButtonXP m_cmdOK;
        private PinkieControls.ButtonXP m_cmdCancel;
        private com.digitalwave.controls.ctlRichTextBox m_txtRecordContent;
        private clsEmployeeSignTool m_objSignTool;
        private bool blnNewRecord;
        private string strRecordInPatientID;
        private string strRecordInPatientDate;
        private string strRecordCreateDate;
        //		protected clsTemplatesetInvoke m_objTempTool;
        private System.Windows.Forms.ToolTip m_ttpTextInfo;
        private clsCommonUseToolCollection m_objCUTC;
        private TextBox txtSign;
        private PinkieControls.ButtonXP m_cmbsign;
        //		private frmTemplatesetDialog m_frmSaveTemplateset;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;

        public frmIntensiveTend_GXContent()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();
        }

        public frmIntensiveTend_GXContent(bool blnNew, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate) : this()
        {
            //			m_objBorderTool=new clsBorderTool(Color.White);
            //m_objSignTool = new clsEmployeeSignTool(m_lsvEmployee);
            //m_objSignTool.m_mthAddControl(m_txtSign);
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();

            //����ָ��Ա��ID��
            m_objSign.m_mthBindEmployeeSign(m_cmbsign, txtSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);

            //m_objCUTC = new clsCommonUseToolCollection(this);
            //m_objCUTC.m_mthBindEmployeeSign(m_cmdSign,m_txtSign,2);
            //��ʼ������ֵ
            blnNewRecord = blnNew;
            strRecordInPatientID = p_strInPatientID;
            strRecordInPatientDate = p_strInPatientDate;
            strRecordCreateDate = p_strCreateDate;
            this.ClientSize = new System.Drawing.Size(744, 424);
            m_mthSetRichTextBoxAttribInControl(this);
            //�Ҽ��˵�
            //			m_objTempTool = new clsTemplatesetInvoke();
            //			m_frmSaveTemplateset=new frmTemplatesetDialog(this);
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntensiveTend_GXContent));
            this.lblRecordContentTitle = new System.Windows.Forms.Label();
            this.m_cmdOK = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.m_txtRecordContent = new com.digitalwave.controls.ctlRichTextBox();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.txtSign = new System.Windows.Forms.TextBox();
            this.m_cmbsign = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.LineColor = System.Drawing.Color.Black;
            this.m_trvCreateDate.Location = new System.Drawing.Point(37, 128);
            this.m_trvCreateDate.Size = new System.Drawing.Size(248, 101);
            this.m_trvCreateDate.Visible = false;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.Location = new System.Drawing.Point(8, 16);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.Location = new System.Drawing.Point(80, 13);
            this.m_dtpCreateDate.Size = new System.Drawing.Size(224, 22);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(411, 174);
            this.m_dtpGetDataTime.Size = new System.Drawing.Size(247, 22);
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.Location = new System.Drawing.Point(289, 174);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(528, 48);
            this.lblSex.Size = new System.Drawing.Size(56, 21);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(656, 48);
            this.lblAge.Size = new System.Drawing.Size(60, 21);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(275, 18);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(261, 55);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(476, 18);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(472, 48);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(600, 48);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(37, 91);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(327, 78);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(135, 119);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(327, 50);
            this.txtInPatientID.Size = new System.Drawing.Size(135, 23);
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(527, 14);
            this.m_txtPatientName.Size = new System.Drawing.Size(136, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(327, 14);
            this.m_txtBedNO.Size = new System.Drawing.Size(135, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(93, 87);
            this.m_cboArea.Size = new System.Drawing.Size(168, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(527, 41);
            this.m_lsvPatientName.Size = new System.Drawing.Size(136, 119);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(327, 38);
            this.m_lsvBedNO.Size = new System.Drawing.Size(135, 119);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(93, 46);
            this.m_cboDept.Size = new System.Drawing.Size(168, 23);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(37, 55);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(821, 55);
            this.m_cmdNewTemplate.Size = new System.Drawing.Size(98, 36);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.Location = new System.Drawing.Point(229, 14);
            this.m_cmdNext.Size = new System.Drawing.Size(28, 24);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Location = new System.Drawing.Point(182, 14);
            this.m_cmdPre.Size = new System.Drawing.Size(28, 24);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(317, 18);
            this.m_lblForTitle.Size = new System.Drawing.Size(19, 27);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(342, -36);
            this.m_tipMain.SetToolTip(this.m_cmdModifyPatientInfo, "����鿴���޸Ļ�����ϸ��Ϣ(��ݼ�Alt+P)");
            // 
            // lblRecordContentTitle
            // 
            this.lblRecordContentTitle.AutoSize = true;
            this.lblRecordContentTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecordContentTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecordContentTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRecordContentTitle.Location = new System.Drawing.Point(9, 41);
            this.lblRecordContentTitle.Name = "lblRecordContentTitle";
            this.lblRecordContentTitle.Size = new System.Drawing.Size(70, 14);
            this.lblRecordContentTitle.TabIndex = 10000033;
            this.lblRecordContentTitle.Text = "�����¼:";
            // 
            // m_cmdOK
            // 
            this.m_cmdOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdOK.DefaultScheme = true;
            this.m_cmdOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdOK.Hint = "";
            this.m_cmdOK.Location = new System.Drawing.Point(536, 376);
            this.m_cmdOK.Name = "m_cmdOK";
            this.m_cmdOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdOK.Size = new System.Drawing.Size(75, 36);
            this.m_cmdOK.TabIndex = 10000034;
            this.m_cmdOK.Text = "ȷ��";
            this.m_cmdOK.Click += new System.EventHandler(this.m_cmdOK_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(624, 376);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(74, 36);
            this.m_cmdCancel.TabIndex = 10000035;
            this.m_cmdCancel.Text = "ȡ��";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // m_txtRecordContent
            // 
            this.m_txtRecordContent.AccessibleDescription = "�����¼";
            this.m_txtRecordContent.BackColor = System.Drawing.Color.White;
            this.m_txtRecordContent.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtRecordContent.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtRecordContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtRecordContent.Location = new System.Drawing.Point(8, 72);
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
            this.m_txtRecordContent.Size = new System.Drawing.Size(712, 295);
            this.m_txtRecordContent.TabIndex = 10;
            this.m_txtRecordContent.Text = "";
            // 
            // m_ttpTextInfo
            // 
            this.m_ttpTextInfo.AutomaticDelay = 200;
            // 
            // txtSign
            // 
            this.txtSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSign.Enabled = false;
            this.txtSign.Location = new System.Drawing.Point(80, 389);
            this.txtSign.Name = "txtSign";
            this.txtSign.Size = new System.Drawing.Size(106, 23);
            this.txtSign.TabIndex = 10000037;
            // 
            // m_cmbsign
            // 
            this.m_cmbsign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmbsign.DefaultScheme = true;
            this.m_cmbsign.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbsign.Hint = "";
            this.m_cmbsign.Location = new System.Drawing.Point(12, 380);
            this.m_cmbsign.Name = "m_cmbsign";
            this.m_cmbsign.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbsign.Size = new System.Drawing.Size(64, 32);
            this.m_cmbsign.TabIndex = 10000036;
            this.m_cmbsign.Text = "ǩ��";
            // 
            // frmIntensiveTend_GXContent
            // 
            this.AccessibleDescription = "Σ�ػ��߻����¼--�����¼";
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(736, 437);
            this.Controls.Add(this.txtSign);
            this.Controls.Add(this.m_cmbsign);
            this.Controls.Add(this.lblRecordContentTitle);
            this.Controls.Add(this.m_cmdOK);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_txtRecordContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIntensiveTend_GXContent";
            this.Text = "Σ�ػ��߻����¼--�����¼";
            this.Load += new System.EventHandler(this.frmIntensiveTend_GXContent_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_txtRecordContent, 0);
            this.Controls.SetChildIndex(this.m_cmdCancel, 0);
            this.Controls.SetChildIndex(this.m_cmdOK, 0);
            this.Controls.SetChildIndex(this.lblRecordContentTitle, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
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
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_cmbsign, 0);
            this.Controls.SetChildIndex(this.txtSign, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public override int m_IntFormID
        {
            get
            {
                return 105;
            }
        }

        #region ����
        // <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        private void m_mthClearRecordInfo()
        {
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(txtSign);
            //��ռ�¼����			
            m_txtRecordContent.m_mthClearText();
            //m_objSignTool.m_mthSetDefaulEmployee();
            m_dtpCreateDate.Value = DateTime.Now;
            //			m_mthSetModifyControl(null,true);
        }
        protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.IntensiveTendRecord_GX);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private new long m_lngSave()
        {
            long lngRes = 0;
            try
            {
                //��ȡ������ʱ��
                string strTimeNow = new clsPublicDomain().m_strGetServerTime();
                //�ӽ����ȡ��ֵ		
                clsIntensiveTendRecordDetail_GX objContent = new clsIntensiveTendRecordDetail_GX();
                objContent.m_strInPatientID = strRecordInPatientID;
                objContent.m_dtmInPatientDate = DateTime.Parse(strRecordInPatientDate);
                objContent.m_dtmOpenDate = DateTime.Parse(strTimeNow);
                objContent.m_dtmModifyDate = DateTime.Parse(strTimeNow);
                objContent.m_dtmDETAILRECORDDATE = Convert.ToDateTime(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                objContent.m_dtmCreateDate = DateTime.Parse(strTimeNow);
                objContent.m_strDETAILCONTENT = m_txtRecordContent.Text;
                objContent.m_strDETAILCONTENTXML = m_txtRecordContent.m_strGetXmlText();
                objContent.m_intSTAT_STATUS = m_intGetClass(m_dtpCreateDate.Value);

                objContent.m_strModifyUserID = MDIParent.OperatorID;
                objContent.m_strCreateUserID = MDIParent.OperatorID;

                if (m_txtRecordContent.m_strGetRightText() == null || m_txtRecordContent.m_strGetRightText() == string.Empty)
                {
                    MDIParent.ShowInformationMessageBox("����д�����¼����");
                    return 0;
                }

                ////ǩ��
                //if(m_txtSign.Tag != null)
                //{

                //    objContent.m_strDETAILSIGNID=((clsEmployee)m_txtSign.Tag).m_StrEmployeeID;
                //    objContent.m_strDETAILSIGNNAME=m_txtSign.Text;
                //}
                objContent.m_strDETAILSIGNID = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR;
                objContent.m_strDETAILSIGNNAME = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strLASTNAME_VCHR;
                //��ȡǩ��
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { txtSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
                //objContent.objSignerArr = new clsEmrSigns_VO[1];
                //objContent.objSignerArr[0] = new clsEmrSigns_VO();
                //objContent.objSignerArr[0].objEmployee = new clsEmrEmployeeBase_VO();
                //objContent.objSignerArr[0].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
                //objContent.objSignerArr[0].controlName = "txtSign";
                //objContent.objSignerArr[0].m_strFORMID_VCHR = "frmGeneralNurseRecord_GXRec";//ע���Сд
                //objContent.objSignerArr[0].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;

                #region ��ǩ��ʱ��֤����ǩ���� ������
                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;

                if (objContent.objSignerArr != null)
                {
                    ArrayList objSignerArr = new ArrayList();
                    for (int i = 0; i < objContent.objSignerArr.Length; i++)
                    {
                        if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                            objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                    }
                    clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                    if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                        return -1;

                }
                else
                {
                    objContent.m_strModifyUserID = MDIParent.OperatorID;
                    clsCheckSignersController objCheck = new clsCheckSignersController();
                    if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                        return -1;
                }
                #endregion

                //clsIntensiveTendRecord_GXService objserv =
                //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));


                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewDetail(objContent);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        /// <summary>
        /// �޸�
        /// </summary>
        /// <returns></returns>
        private long m_lngModify()
        {
            long lngRes = 0;
            try
            {
                //�ӽ����ȡ��ֵ
                string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                clsIntensiveTendRecordDetail_GX objContent = new clsIntensiveTendRecordDetail_GX();
                objContent.m_strInPatientID = strRecordInPatientID;
                objContent.m_dtmInPatientDate = DateTime.Parse(strRecordInPatientDate);
                objContent.m_dtmModifyDate = DateTime.Parse(strNow);
                objContent.m_dtmDETAILRECORDDATE = Convert.ToDateTime(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                objContent.m_dtmOpenDate = DateTime.Parse(strRecordCreateDate);
                objContent.m_strDETAILCONTENT = m_txtRecordContent.Text;
                objContent.m_strDETAILCONTENTXML = m_txtRecordContent.m_strGetXmlText();

                if (m_txtRecordContent.m_strGetRightText() == null || m_txtRecordContent.m_strGetRightText() == string.Empty)
                {
                    MDIParent.ShowInformationMessageBox("����д�����¼����");
                    return 0;
                }
                //ǩ��

                objContent.m_strModifyUserID = MDIParent.OperatorID;
                //objContent.m_strDETAILSIGNID = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR;
                //objContent.m_strDETAILSIGNNAME = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strLASTNAME_VCHR;
                //��ȡǩ��
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { txtSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
                //objContent.objSignerArr = new clsEmrSigns_VO[1];
                //objContent.objSignerArr[0] = new clsEmrSigns_VO();
                //objContent.objSignerArr[0].objEmployee = new clsEmrEmployeeBase_VO();
                //objContent.objSignerArr[0].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
                //objContent.objSignerArr[0].controlName = "txtSign";
                //objContent.objSignerArr[0].m_strFORMID_VCHR = "frmGeneralNurseRecord_GXRec";//ע���Сд
                //objContent.objSignerArr[0].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;

                #region ��ǩ��ʱ��֤����ǩ���� ������

                //if (objContent.objSignerArr != null)
                //{
                //    ArrayList objSignerArr = new ArrayList();
                //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
                //    {
                //        if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                //            objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                //    }
                //    //����ǩ�� 
                //    //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                //    string strRecordID = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"); ;

                //    clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                //    if (objCheck.CheckSigner(objContent, this.Name, strRecordID) == -1)
                //        return -1;

                //}
                //else
                //{
                //objContent.m_strModifyUserID = MDIParent.OperatorID;
                //����ǩ�� ���ݿ��� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                    return -1;
                //}
                #endregion

                //clsIntensiveTendRecord_GXService objserv =
                //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));


                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifyDetail(objContent);
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        private void m_GetDataFromDB()
        {
            long lngRes = 0;
            try
            {
                string[] DataArr;

                //clsIntensiveTendRecord_GXService objserv =
                //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecordContent(strRecordInPatientID, strRecordInPatientDate, strRecordCreateDate, out DataArr);
                if (DataArr == null) return;
                //��ֵ����
                m_txtRecordContent.m_mthSetNewText(DataArr[3], DataArr[4]);
                m_dtpCreateDate.Value = DateTime.Parse(DataArr[5]);
                //MDIParent.m_mthChangeFormText(this, m_enmFormEditStatus);
                //���ݹ��Ż�ȡǩ����Ϣ
                m_mthAddSignToTextBoxByEmpNo(new TextBoxBase[] { txtSign }, new string[] { DataArr[1] }, new bool[] { false });
                //m_mthSetModifyControl(DataArr[1],false);
                this.m_dtpCreateDate.Enabled = false;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }

        #endregion ;
        /// <summary>
        /// ȷ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdOK_Click(object sender, System.EventArgs e)
        {
            if (blnNewRecord)
            {
                if (this.m_lngSave() > 0)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

            }
            else
            {
                if (m_lngModify() > 0)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }

        }
        /// <summary>
        /// ȡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }
        /*
                #region ctlRichTextBox��˫���ߡ�������������
                private readonly DateTime m_dtmEmptyDate = DateTime.MinValue;
                protected clsBorderTool m_objBorderTool;
                /// <summary>
                /// ����˫����
                /// </summary>
                protected void m_mthSetRichTextBoxDoubleStrike()
                {
                    if(m_txtFocusedRichTextBox!=null)
                        m_txtFocusedRichTextBox.m_mthSelectionDoubleStrikeThough(true);			
                }

                /// <summary>
                /// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
                /// </summary>
                /// <param name="p_objRichTextBox"></param>
                protected void m_mthSetRichTextBoxAttrib(com.digitalwave.controls.ctlRichTextBox p_objRichTextBox)
                {
                    m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	p_objRichTextBox });
                    //�����Ҽ��˵�			
                    p_objRichTextBox.GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);


                    //������������			
                    p_objRichTextBox.m_StrUserID = MDIParent.OperatorID;
                    p_objRichTextBox.m_StrUserName = MDIParent.strOperatorName;
                    p_objRichTextBox.m_ClrOldPartInsertText = Color.Black;
                    p_objRichTextBox.m_ClrDST = Color.Red;
                    p_objRichTextBox.ForeColor=SystemColors.WindowText;
                }

                protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
                {
                    if(p_ctlControl.GetType().Name=="ctlRichTextBox")
                    {
                        m_mthSetRichTextBoxAttrib((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
                    }

                    if(p_ctlControl.HasChildren && p_ctlControl.GetType().Name !="DataGrid" )
                    {									
                        foreach(Control subcontrol in p_ctlControl.Controls)
                        {										
                            m_mthSetRichTextBoxAttribInControl(subcontrol);						
                        } 	
                    }	
                }
                private void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
                {
                    m_mthSetRichTextBoxDoubleStrike();
                }
                private com.digitalwave.controls.ctlRichTextBox m_txtFocusedRichTextBox=null;//��ŵ�ǰ��ý����RichTextBox
                private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
                {
                    m_txtFocusedRichTextBox=((com.digitalwave.controls.ctlRichTextBox)(sender));
                }
        */
        /// <summary>
        /// ���ô����пؼ������ı�����ɫ
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_clrColor"></param>
        private void m_mthSetRichTextModifyColor(Control p_ctlControl, System.Drawing.Color p_clrColor)
        {
            #region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;

            if (p_ctlControl.HasChildren && strTypeName != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextModifyColor(subcontrol, p_clrColor);
                }
            }
            #endregion
        }


        private void m_mthSetRichTextCanModifyLast(Control p_ctlControl, bool p_blnCanModifyLast)
        {
            #region ���ÿؼ������ı����Ƿ�����޸�,Jacky-2003-3-24	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
            {
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
            }

            if (p_ctlControl.HasChildren && strTypeName != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextCanModifyLast(subcontrol, p_blnCanModifyLast);
                }
            }
            #endregion
        }
        /*	
                #endregion ctlRichTextBox��˫���ߡ�������������
                #region ͨ���Ҽ��˵�ʵ�� 
                /// <summary>
                /// 
                /// </summary>
                /// <param name="p_txtControl"></param>
                protected void m_mthAddRichTemplate(RichTextBox p_txtControl)
                {
                    m_objTempTool.m_mthAddTextBox(this,p_txtControl,m_strGetCurFormName(),p_txtControl.Name);
                }
                /// <summary>
                /// 
                /// </summary>
                /// <param name="p_ctlContainer"></param>
                protected virtual void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
                {
                    foreach(Control ctlChild in p_ctlContainer.Controls)
                    {
                        if(ctlChild.Name == "" && ctlChild.GetType().FullName != "System.Windows.Forms.TabPage")
                            continue;
                        switch(ctlChild.GetType().FullName)
                        {
                            case "com.digitalwave.controls.ctlRichTextBox":
                                m_mthAddRichTemplate((RichTextBox)ctlChild);
                                m_mthAddRichTextInfo((com.digitalwave.controls.ctlRichTextBox)ctlChild);
                                break;
                            case "System.Windows.Forms.RichTextBox":
                            case "iCare.CustomForm.ctlRichTextBox":
                                m_mthAddRichTemplate((RichTextBox)ctlChild);
                                break;
                            default:
                                m_mthAddRichTemplateInContainer(ctlChild);
                                break;
                        }				
                    }
                }
                /// <summary>
                /// 
                /// </summary>
                /// <param name="p_ctlTextBox"></param>
                protected void m_mthAddRichTextInfo(com.digitalwave.controls.ctlRichTextBox p_ctlTextBox)
                {
                    p_ctlTextBox.m_evtMouseEnterDeleteText += new EventHandler(m_mthHandleMouseEnterDeleteText);
                    p_ctlTextBox.m_evtMouseEnterInsertText += new EventHandler(m_mthHandleMouseEnterInsertText);
                    p_ctlTextBox.MouseLeave += new EventHandler(m_mthHandleMouseLeaveControl);
                }

                /// <summary>
                /// ��ȡ��ǰ����,�Զ�������⴦��
                /// </summary>
                /// <returns></returns>
                private string m_strGetCurFormName()
                {
                    string strFormName = this.Name;
                    return strFormName;
                }
                /// <summary>
                /// ɾ����Ϣ
                /// </summary>
                /// <param name="p_objSender"></param>
                /// <param name="p_objArg"></param>
                private void m_mthHandleMouseEnterDeleteText(object p_objSender,EventArgs p_objArg)
                {
                    com.digitalwave.controls.clsDoubleStrikeThoughEventArg objArg = (com.digitalwave.controls.clsDoubleStrikeThoughEventArg)p_objArg;

                    string strInfo = "�û����� : "+
                        objArg.m_strUserName+"\r\nɾ��ʱ�� : ";

                    if(objArg.m_dtmDeleteTime != m_dtmEmptyDate && objArg.m_dtmDeleteTime != DateTime.MinValue)
                    {
                        strInfo += objArg.m_dtmDeleteTime.ToLongDateString()+" "+objArg.m_dtmDeleteTime.ToLongTimeString();				
                    }	
                    else
                    {
                        strInfo += "----��--��--�� --:--:--";
                    }

                    m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
                }
                /// <summary>
                /// ������Ϣ
                /// </summary>
                /// <param name="p_objSender"></param>
                /// <param name="p_objArg"></param>
                private void m_mthHandleMouseEnterInsertText(object p_objSender,EventArgs p_objArg)
                {
                    com.digitalwave.controls.clsInsertEventArg objArg = (com.digitalwave.controls.clsInsertEventArg)p_objArg;

                    if(objArg.m_intUserSeq == 1)
                    {
                        return;
                    }

                    string strInfo = "�û����� : "+
                        objArg.m_strUserName+"\r\n���ʱ�� : ";

                    if(objArg.m_dtmInsertTime != m_dtmEmptyDate && objArg.m_dtmInsertTime != DateTime.MinValue)
                    {
                        strInfo += objArg.m_dtmInsertTime.ToLongDateString()+" "+objArg.m_dtmInsertTime.ToLongTimeString();				
                    }	
                    else
                    {
                        strInfo += "----��--��--�� --:--:--";
                    }

                    m_ttpTextInfo.SetToolTip((Control)p_objSender,strInfo);
                }
                /// <summary>
                /// ͨ���Ҽ� �뿪�¼�
                /// </summary>
                /// <param name="p_objSender"></param>
                /// <param name="p_objArg"></param>
                private void m_mthHandleMouseLeaveControl(object p_objSender,EventArgs p_objArg)

                {
                    m_ttpTextInfo.RemoveAll();
                }
                #endregion*/
        private void frmIntensiveTend_GXContent_Load(object sender, System.EventArgs e)
        {

            //if (blnNewRecord)
            //{
            //    //��ʼ��
            //    m_mthClearRecordInfo();
            //    //���϶˿ռ���
            //    m_txtRecordContent.m_mthInsertText("    ", 0);
            //}
            //else
            //    m_GetDataFromDB();
            //�Ҽ��˵�
            //			m_mthAddRichTemplateInContainer(this);
        }

        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset"></param>
        protected void m_mthSetModifyControl(string p_strCreateUserID,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
            if (p_strCreateUserID != null)
            {
                m_mthSetRichTextModifyColor(this, Color.Red);
                m_mthSetRichTextCanModifyLast(this, m_blnGetCanModifyLast(p_strCreateUserID));
            }
        }

        private bool m_blnGetCanModifyLast(string p_strCreateUserID)
        {
            if (p_strCreateUserID != null && p_strCreateUserID.Trim() == MDIParent.OperatorID.Trim())
                return true;
            else
                return false;
        }

        //		public void m_mthNewCommonUseWithThis()
        //		{
        //			if(this.ActiveControl == null)
        //			{
        //				clsPublicFunction.ShowInformationMessageBox("����ѡ��Ҫ���ɳ���ֵ�������",this);
        //				return;
        //			}
        //			this.m_frmSaveTemplateset.m_CtlCurrent = this.ActiveControl;
        //			this.m_frmSaveTemplateset.mthShowSaveDialog(this.Name,this,true);
        //		}

        #region OverRide Function

        protected override bool m_BlnCanTextChanged
        {
            get
            {
                return true;
            }
        }

        protected override enmFormState m_EnmCurrentFormState
        {
            get
            {
                return enmFormState.NowUser;
            }
        }


        protected override long m_lngSubAddNew()
        {
            return (long)enmOperationResult.DB_Succeed;
        }

        protected override bool m_BlnIsAddNew
        {
            get
            {
                return false;
            }
        }

        protected override long m_lngSubModify()
        {
            return (long)enmOperationResult.DB_Succeed;
        }
        protected override long m_lngSubDelete()
        {
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        protected override void m_mthSetSelectedDeletedRecord(clsPatient p_objSelectedPatient,
            string p_strOpenDate)
        {
            //������
            if (p_objSelectedPatient == null || p_strOpenDate == null || p_strOpenDate == "")
                return;

            clsIntensiveTendRecordDetail_GX objContent;
            //clsIntensiveTendRecord_GXService objITRServ =
            //    (clsIntensiveTendRecord_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsIntensiveTendRecord_GXService));

            //��ȡ��¼
            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDelRecordContentWithInpatient(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate, out objContent);
            //objITRServ.Dispose();
            if (lngRes <= 0 || objContent == null)
                return;


            //���õ�ǰ��¼����¼ʱ�� 
            m_objCurrentPatient = p_objSelectedPatient;
            //txtInPatientID.Text=this.m_objCurrentPatient.m_StrHISInPatientID;

            m_mthSetDeletedGUIFromContent(objContent);

        }

        protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsIntensiveTendRecordDetail_GX objContent = (clsIntensiveTendRecordDetail_GX)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			

            this.m_mthClearRecordInfo();
            m_txtRecordContent.m_mthSetNewText(objContent.m_strDETAILCONTENT, objContent.m_strDETAILCONTENTXML);
            m_dtpCreateDate.Value = objContent.m_dtmDETAILRECORDDATE;
            //if(objContent.m_strDETAILSIGNID != null && objContent.m_strDETAILSIGNID != "")
            //{
            //    clsEmployee objEmployee=new clsEmployee(objContent.m_strDETAILSIGNID);
            //    m_txtSign.Tag=objEmployee;
            //}
            //m_txtSign.Text=objContent.m_strDETAILSIGNNAME;

            //this.m_txtSign.Enabled = false;
            //���ݹ��Ż�ȡǩ����Ϣ
            //���ڼ��ݿ��ǣ�����ʹ�� tfzhang 2006-03-12
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            clsEmrEmployeeBase_VO objSign = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strDETAILSIGNID, out objSign);
            if (objSign != null)
            {
                txtSign.Text = objSign.m_strLASTNAME_VCHR;
                txtSign.Tag = objSign;
            }
            this.txtSign.Enabled = false;
            this.m_dtpCreateDate.Enabled = false;
        }

        /// <summary>
        /// ��ȡ���
        /// ����-����ʱ��8:00-14:30,14:31-18:00,18:01-����2:00,����2:01-7:59
        /// </summary>
        /// <param name="dtmRecordDate"></param>
        /// <returns></returns>
        private int m_intGetClass(DateTime dtmRecordDate)
        {
            string strDate = dtmRecordDate.Year.ToString("0000") + dtmRecordDate.Month.ToString("00") + dtmRecordDate.Day.ToString("00");
            string strYesterday = dtmRecordDate.Year.ToString() + dtmRecordDate.Month.ToString() + dtmRecordDate.AddDays(-1).Day.ToString();
            DateTime dtClass = DateTime.Parse(dtmRecordDate.ToString("yyyy-MM-dd HH:mm:00"));
            DateTime dtDt0 = dtmRecordDate.Date;
            DateTime dt1 = dtDt0.AddHours(2).AddMinutes(1);
            DateTime dt2 = dtDt0.AddHours(8);
            DateTime dt3 = dtDt0.AddHours(14).AddMinutes(31);
            DateTime dt4 = dtDt0.AddHours(18).AddMinutes(1);
            DateTime dt5 = dtDt0.AddHours(26).AddMinutes(1);

            if (dtmRecordDate >= dt1 && dtmRecordDate < dt2)
                return Convert.ToInt32(strDate + "0");
            else if (dtmRecordDate >= dt2 && dtmRecordDate < dt3)
                return Convert.ToInt32(strDate + "1");
            else if (dtmRecordDate >= dt3 && dtmRecordDate < dt4)
                return Convert.ToInt32(strDate + "2");
            else if (dtmRecordDate >= dt4 && dtmRecordDate < dt5)
                return Convert.ToInt32(strDate + "3");
            else if (dtmRecordDate < dt1)
                return Convert.ToInt32(strYesterday + "3");
            return 0;
        }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            base.m_mthPerformSessionChanged(p_objSelectedSession, p_intIndex);
            if (blnNewRecord)
            {
                //��ʼ��
                m_mthClearRecordInfo();
                //���϶˿ռ���
                m_txtRecordContent.m_mthInsertText("    ", 0);
            }
            else
                m_GetDataFromDB();
            m_mthAddFormStatusForClosingSave();
        }
    }
}
