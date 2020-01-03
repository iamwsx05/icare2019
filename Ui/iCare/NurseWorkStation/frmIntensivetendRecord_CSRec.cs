using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.controls;
using System.Data;
using HRP;
using System.Xml; 
using com.digitalwave.Emr.Signature_gui;

namespace iCare
{
    /// <summary>
    /// ��������Σ�ػ��߻����¼--�����¼�Ӵ���
    /// </summary>
    public partial class frmIntensivetendRecord_CSRec : frmDiseaseTrackBase
    {
        private void frmIntensivetendRecord_CSRec_Load(object sender, EventArgs e)
        {
            m_txtBoxTemperature.Focus();
        }
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        private DataTable m_dtbInceptInfo = null;
        private DataTable m_dtbEductionInfo = null;
        public frmIntensivetendRecord_CSRec()
        {
            InitializeComponent();
            m_mthSetRichTextBoxAttribInControl(this);
            m_mthInitDataTable();
            m_objSign = new clsEmrSignToolCollection();
            //����ָ��Ա��ID��
            m_objSign.m_mthBindEmployeeSign(m_cmbRecordSign, lsvRecordSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);
        }
        public override int m_IntFormID
        {
            get
            {
                return 172;
            }
        }

		public override iCare.clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
		{
			clsIntensiveRecordInfo objTrackInfo = new clsIntensiveRecordInfo();

			objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;
			objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
			objTrackInfo.m_StrTitle =this.m_lblForTitle.Text;

			//����m_dtmRecordTime
			if(objTrackInfo.m_ObjRecordContent !=null)
			{
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
            this.m_txtBoxTemperature.m_mthClearText();
			this.m_txtTemperature.m_mthClearText();
			this.m_txtRespiration.m_mthClearText();
            this.m_txtSpO2.m_mthClearText();
            this.m_txtHeartRate.m_mthClearText();
            this.m_txtBloodPress.m_mthClearText();
            this.m_cboMind.Text="";
            this.m_txtPupilSizeLeft.m_mthClearText();
            this.m_txtPupilSizeRight.m_mthClearText();
            this.m_cboPupilReflectLeft.Text = "";
            this.m_cboPupilReflectRight.Text = "";
            this.m_cboFaceColor.Text = "";
            this.m_cboFontanel.Text = "";
            this.m_cboSkinColor.Text = "";
            this.m_cboSkinEdema.Text = "";
            this.m_cboSkinElasticity.Text = "";
            this.m_cboSkinPattern.Text = "";
            this.m_cboSkinEdemaPosition.Text = "";
            this.m_cboSkinEdemaProperty.Text = "";
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvRecordSign);
            m_cmdModifyPatientInfo.Visible = false;

 		}

		/// <summary>
		/// �����Ƿ����ѡ���˺ͼ�¼ʱ���б�
		/// </summary>
		/// <param name="p_blnEnable"></param>
		protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
		{
			if(p_blnEnable==false)
			{
			
				m_cmdOK.Visible=true;
				
				this.CenterToParent();	
			}	
	
			this.MaximizeBox=false;
		}

		/// <summary>
		/// �����¼���������,�����Ӵ������Ҫ����ʵ��
		/// </summary>
		/// <param name="p_blnEnable">�Ƿ������޸������¼�ļ�¼��Ϣ��</param>
		protected override void m_mthEnableModifySub(bool p_blnEnable)
		{
		
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
			//������д�淶���þ��崰�����д����
			
		}

		/// <summary>
		/// �������¼��ֵ��ʾ�������ϡ�
		/// </summary>
		/// <param name="p_objContent"></param>
		protected override void m_mthSetGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
		{
			clsIntensivetendRecordContent_CS objContent=(clsIntensivetendRecordContent_CS )p_objContent;

			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			
			this.m_mthClearRecordInfo();
            this.m_txtBoxTemperature.m_mthSetNewText(objContent.m_strBOXTEMPERATUREALL, objContent.m_strBOXTEMPERATUREXML);
			this.m_txtTemperature.m_mthSetNewText(objContent.m_strTEMPERATUREAll,objContent.m_strTEMPERATUREXML);
            this.m_txtHeartRate.m_mthSetNewText(objContent.m_strHEARTRATE, objContent.m_strHEARTRATEXML);
			this.m_txtRespiration.m_mthSetNewText(objContent.m_strRESPIRATION,objContent.m_strRESPIRATIONXML);
            this.m_txtBloodPress.m_mthSetNewText(objContent.m_strBLOODPRESSURES, objContent.m_strBLOODPRESSURESXML);
            this.m_txtSpO2.m_mthSetNewText(objContent.m_strSPO2, objContent.m_strSPO2XML);
            this.m_cboMind.Text = objContent.m_strMind;
            this.m_txtPupilSizeLeft.m_mthSetNewText(objContent.m_strPupilSizeLeft, objContent.m_strPupilSizeLeftXML);
            this.m_txtPupilSizeRight.m_mthSetNewText(objContent.m_strPupilSizeRight, objContent.m_strPupilSizeRightXML);
            this.m_cboPupilReflectLeft.Text = objContent.m_strPupilReflectLeft;
            this.m_cboPupilReflectRight.Text = objContent.m_strPupilReflectRight;
            this.m_cboFontanel.Text = objContent.m_strFontanel;
            this.m_cboFaceColor.Text = objContent.m_strFaceColor;
            this.m_cboSkinColor.Text = objContent.m_strSkinColor;
            this.m_cboSkinEdema.Text = objContent.m_strSkinEdema;
            this.m_cboSkinEdemaPosition.Text = objContent.m_strSkinEdemaPosition;
            this.m_cboSkinEdemaProperty.Text = objContent.m_strSkinEdemaProperty;
            this.m_cboSkinElasticity.Text = objContent.m_strSkinLasticity;
            this.m_cboSkinPattern.Text = objContent.m_strSkinPattern;
            m_dtbInceptInfo.Clear();
            m_dtbEductionInfo.Clear();
            object[] m_objTemp = new object[2];
            if (objContent.m_objInpectArr != null)
            {
                m_dtbInceptInfo.BeginLoadData();
                for (int i = 0; i < objContent.m_objInpectArr.Length; i++)
                {
                    m_objTemp[0] = objContent.m_objInpectArr[i].m_strINPECT_KIND;
                    m_objTemp[1] = objContent.m_objInpectArr[i].m_strINPECT_METE;
                    m_dtbInceptInfo.LoadDataRow(m_objTemp, true);
                }
                m_dtbInceptInfo.EndLoadData();
            }
            if (objContent.m_objEductionArr != null)
            {
                m_dtbEductionInfo.BeginLoadData();
                for (int i = 0; i < objContent.m_objEductionArr.Length; i++)
                {
                    m_objTemp[0] = objContent.m_objEductionArr[i].m_strEDUCTION_KIND;
                    m_objTemp[1] = objContent.m_objEductionArr[i].m_strEDUCTION_METE;
                    m_dtbEductionInfo.LoadDataRow(m_objTemp, true);
                }
                m_dtbEductionInfo.EndLoadData();
            }
            m_mthAddSignToListView(lsvRecordSign , objContent.objSignerArr);   
			this.m_dtpCreateDate.Enabled = false;
		}

		protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
		{
			clsIntensivetendRecordContent_CS objContent=(clsIntensivetendRecordContent_CS )p_objContent;

			//�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			
			this.m_mthClearRecordInfo();
            this.m_txtBoxTemperature.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strBOXTEMPERATUREALL, objContent.m_strBOXTEMPERATUREXML);
			this.m_txtTemperature.Text=ctlRichTextBox.s_strGetRightText(objContent.m_strTEMPERATUREAll ,objContent.m_strTEMPERATUREXML);
            this.m_txtHeartRate.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strHEARTRATE, objContent.m_strHEARTRATEXML);
			this.m_txtRespiration.Text=ctlRichTextBox.s_strGetRightText(objContent.m_strRESPIRATION ,objContent.m_strRESPIRATIONXML  );
            this.m_txtBloodPress.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strBLOODPRESSURES, objContent.m_strBLOODPRESSURESXML);
            this.m_txtSpO2.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strSPO2, objContent.m_strSPO2XML);
            this.m_cboMind.Text = objContent.m_strMind;
            this.m_txtPupilSizeLeft.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strPupilSizeLeft, objContent.m_strPupilSizeLeftXML);
            this.m_txtPupilSizeRight.Text = ctlRichTextBox.s_strGetRightText(objContent.m_strPupilSizeRight, objContent.m_strPupilSizeRightXML);
            this.m_cboPupilReflectLeft.Text = objContent.m_strPupilReflectLeft;
            this.m_cboPupilReflectRight.Text = objContent.m_strPupilReflectRight;

            this.m_cboFontanel.Text = objContent.m_strFontanel;
            this.m_cboFaceColor.Text = objContent.m_strFaceColor;
            this.m_cboSkinColor.Text = objContent.m_strSkinColor;
            this.m_cboSkinEdema.Text = objContent.m_strSkinEdema;
            this.m_cboSkinEdemaPosition.Text = objContent.m_strSkinEdemaPosition;
            this.m_cboSkinEdemaProperty.Text = objContent.m_strSkinEdemaProperty;
            this.m_cboSkinElasticity.Text = objContent.m_strSkinLasticity;
            this.m_cboSkinPattern.Text = objContent.m_strSkinPattern;

		}

		protected override weCare.Core.Entity.clsTrackRecordContent m_objGetContentFromGUI()
		{
			
			//�������У��
			if(m_objCurrentPatient==null)// || this.txtInPatientID.Text!=this.m_objCurrentPatient.m_StrHISInPatientID || txtInPatientID.Text=="")				
				return null;

			#region ����ͬһ�������ڵĲ����¼

			#endregion

			//�ӽ����ȡ��ֵ		
			clsIntensivetendRecordContent_CS objContent=new clsIntensivetendRecordContent_CS ();
			try
			{
				objContent.m_dtmCreateDate =DateTime.Now ;
                //����
                objContent.m_strBOXTEMPERATURE_RIGHT = this.m_txtBoxTemperature.m_strGetRightText();
                objContent.m_strBOXTEMPERATUREALL = this.m_txtBoxTemperature.Text;
                objContent.m_strBOXTEMPERATUREXML = this.m_txtBoxTemperature.m_strGetXmlText();
                //����
				objContent.m_strTEMPERATURE_RIGHT=this.m_txtTemperature.m_strGetRightText();
				objContent.m_strTEMPERATUREAll=this.m_txtTemperature.Text;
				objContent.m_strTEMPERATUREXML=this.m_txtTemperature.m_strGetXmlText();
                //����
                objContent.m_strHEARTRATE_RIGHT = this.m_txtHeartRate.m_strGetRightText();
                objContent.m_strHEARTRATE = this.m_txtHeartRate.Text;
                objContent.m_strHEARTRATEXML = this.m_txtHeartRate.m_strGetXmlText();
                //����
				objContent.m_strRESPIRATION_RIGHT=this.m_txtRespiration.m_strGetRightText();
				objContent.m_strRESPIRATION=this.m_txtRespiration.Text;
				objContent.m_strRESPIRATIONXML=this.m_txtRespiration.m_strGetXmlText();
                //Ѫѹ
                objContent.m_strBLOODPRESSURES_RIGHT = this.m_txtBloodPress.m_strGetRightText();
                objContent.m_strBLOODPRESSURES = this.m_txtBloodPress.Text;
                objContent.m_strBLOODPRESSURESXML = this.m_txtBloodPress.m_strGetXmlText();
                //spo2
                objContent.m_strSPO2_RIGHT = this.m_txtSpO2.m_strGetRightText();
                objContent.m_strSPO2 = this.m_txtSpO2.Text;
                objContent.m_strSPO2XML = this.m_txtSpO2.m_strGetXmlText();
                //��־
                objContent.m_strMind = this.m_cboMind.Text;
                //ͫ�״�С��
                objContent.m_strPupilSizeLeft_RIGHT = this.m_txtPupilSizeLeft.m_strGetRightText();
                objContent.m_strPupilSizeLeft = this.m_txtPupilSizeLeft.Text;
                objContent.m_strPupilSizeLeftXML = this.m_txtPupilSizeLeft.m_strGetXmlText();
                //ͫ�״�С��
                objContent.m_strPupilSizeRight_RIGHT = this.m_txtPupilSizeRight.m_strGetRightText();
                objContent.m_strPupilSizeRight = this.m_txtPupilSizeRight.Text;
                objContent.m_strPupilSizeRightXML = this.m_txtPupilSizeRight.m_strGetXmlText();
                //ͫ�׷�����
                objContent.m_strPupilReflectLeft = this.m_cboPupilReflectLeft.Text;
                //ͫ�׷�����
                objContent.m_strPupilReflectRight = this.m_cboPupilReflectRight.Text;
                //ض��
                objContent.m_strFontanel = this.m_cboFontanel.Text;
                //��ɫ
                objContent.m_strFaceColor = this.m_cboFaceColor.Text;
                //Ƥ����ɫ
                objContent.m_strSkinColor = this.m_cboSkinColor.Text;
                //Ƥ��Ӳ��
                objContent.m_strSkinEdema = this.m_cboSkinEdema.Text;
                //Ƥ������
                objContent.m_strSkinLasticity = this.m_cboSkinElasticity.Text;
                //Ƥ������
                objContent.m_strSkinPattern = this.m_cboSkinPattern.Text;
                //Ƥ��Ӳ�ײ�λ
                objContent.m_strSkinEdemaPosition = this.m_cboSkinEdemaPosition.Text;
                //Ƥ��Ӳ������
                objContent.m_strSkinEdemaProperty = this.m_cboSkinEdemaProperty.Text;

                objContent.m_objInpectArr = m_objGetInceptInfoArr();
                objContent.m_objEductionArr = m_objGetEductionInfoArr();

                objContent.m_strCreateUserID = MDIParent.OperatorID;
				objContent.m_dtmModifyDate = DateTime.Now;
                objContent.m_strModifyUserID = MDIParent.OperatorID;
				objContent.m_dtmRECORDDATE = m_dtpCreateDate.Value;
                objContent.m_intMarkStatus = chkModifyWithoutMatk.Checked ? 0 : 1;

                //��ȡǩ��s
                strUserIDList = "";
                strUserNameList = "";   
                m_mthGetSignArr(new Control[] { lsvRecordSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
			}
		
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return(objContent );		
		}

		protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
		{
			//��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.IntensivetendRecord_CS);					
		}

		/// <summary>
		/// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
		{
			//��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
			clsIntensivetendRecordContent_CS objContent=(clsIntensivetendRecordContent_CS)p_objRecordContent;
		}

		public override string m_strReloadFormTitle()
		{
			//���Ӵ�������ʵ��

			return	"��������Σ�ػ��߻����¼";
		}

		private void m_cmdOK_Click(object sender, System.EventArgs e)
		{
			if(m_lngSave() > 0)
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void m_cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			this.Close();
		}

		#region Jump Control
		protected override void m_mthInitJump(clsJumpControl p_objJump)
		{
			p_objJump=new clsJumpControl(this,
                new Control[]{m_txtBoxTemperature,m_txtTemperature,m_txtHeartRate,m_txtRespiration,m_txtBloodPress,m_txtSpO2,
								 m_cboMind,this.m_cboFontanel,this.m_cboFaceColor,this.m_cboSkinColor,this.m_cboSkinEdema,
                                 this.m_cboSkinElasticity,this.m_cboSkinPattern,this.m_cboSkinEdemaPosition,this.m_cboSkinEdemaProperty,
                                 m_txtPupilSizeLeft,m_txtPupilSizeRight,m_cboPupilReflectLeft,m_cboPupilReflectRight,
                    lsvRecordSign}, Keys.Enter);
		}
		#endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        #region �����������
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        private clsNurseRecordInpectInfo[] m_objGetInceptInfoArr()
        {
            int m_intRows = m_dtbInceptInfo.Rows.Count;
            if (m_intRows <= 0)
                return null;
            clsNurseRecordInpectInfo[] m_objInceptInfoArr = new clsNurseRecordInpectInfo[m_intRows];
            try
            {
                for (int i1 = 0; i1 < m_intRows; i1++)
                {
                    m_objInceptInfoArr[i1] = new clsNurseRecordInpectInfo();
                    m_objInceptInfoArr[i1].m_strFORMID = this.Name;
                    m_objInceptInfoArr[i1].m_strINPECT_KIND = m_dtbInceptInfo.Rows[i1][0].ToString();
                    m_objInceptInfoArr[i1].m_strINPECT_METE = m_dtbInceptInfo.Rows[i1][1].ToString();
                }
            }
            catch (Exception err)
            {
                clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);
            }
            return m_objInceptInfoArr;
        }
        #endregion
        #region ����ų�����
        /// <summary>
        /// ����ų�����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        private clsNurseRecordEductionInfo[] m_objGetEductionInfoArr()
        {
            int m_intRows = m_dtbEductionInfo.Rows.Count;
            if (m_intRows <= 0)
                return null;
            clsNurseRecordEductionInfo[] m_objEductionInfoArr = new clsNurseRecordEductionInfo[m_intRows];
            try
            {
                for (int i1 = 0; i1 < m_intRows; i1++)
                {
                    m_objEductionInfoArr[i1] = new clsNurseRecordEductionInfo();
                    m_objEductionInfoArr[i1].m_strFORMID = this.Name;
                    m_objEductionInfoArr[i1].m_strEDUCTION_KIND = m_dtbEductionInfo.Rows[i1][0].ToString();
                    m_objEductionInfoArr[i1].m_strEDUCTION_METE = m_dtbEductionInfo.Rows[i1][1].ToString();
                }
            }
            catch (Exception err)
            {
                clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);
            }
            return m_objEductionInfoArr;
        }
        #endregion
        private void m_mthInitDataTable()
        {
            DataColumn dtcTemp;

            #region ����
            m_dtbInceptInfo = new DataTable("InceptInfo");
            DataColumn dcInceptKind = this.m_dtbInceptInfo.Columns.Add("inceptkind");
            dcInceptKind.DefaultValue = "";
            DataColumn dcInceptMete = this.m_dtbInceptInfo.Columns.Add("inceptmete");
            dcInceptMete.DefaultValue = "";
            dataGrid1.DataSource = m_dtbInceptInfo;
            #endregion

            #region �ų�
            m_dtbEductionInfo = new DataTable("EductionInfo");
            DataColumn dcEductionKind = this.m_dtbEductionInfo.Columns.Add("eductionkind");
            dcEductionKind.DefaultValue = "";
            DataColumn dcEductionMete = this.m_dtbEductionInfo.Columns.Add("eductionmete");
            dcEductionMete.DefaultValue = "";
            dataGrid2.DataSource = m_dtbEductionInfo;
            #endregion
        }
	}
}

