using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.emr.BEDExplorer;
using System.Windows.Forms;
using iCareData;
using com.digitalwave.Utility.Controls;
using System.Data;
using HRP;
using com.digitalwave.Emr.Signature_gui;
using com.digitalwave.emr.AssistModuleVO;

namespace iCare
{
    /// <summary>
    /// ��ǰ���ۼ�¼---�½�
    /// </summary>
    public partial class frmBeforeOperationDiscuss_xj : iCare.frmDiseaseTrackBase
    {
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();


        public frmBeforeOperationDiscuss_xj()
        {
            InitializeComponent();

            m_mthSetRichTextBoxAttribInControl(this);
            //ָ��ҽ������վ��
            intFormType = 1;
            this.Text = "��ǰ���ۼ�¼";
            this.m_lblForTitle.Text = this.Text;

            m_objSign = new clsEmrSignToolCollection();
            //������
            m_objSign.m_mthBindEmployeeSign(m_cmdCompere, m_txtCompere, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
            //�㱨��
            m_objSign.m_mthBindEmployeeSign(m_cmdHuiBao, m_txtHuiBao, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
            //��¼��
            //m_objSign.m_mthBindEmployeeSign(m_cmdRecord, m_txtRecorder, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            //�μ���Ա
            m_objSign.m_mthBindEmployeeSign(m_cmdAttendee, m_lsvAttendeeList, 0, false, clsEMRLogin.LoginInfo.m_strEmpID);
            //�ܴ�ҽʦ
            //m_objSign.m_mthBindEmployeeSign(m_cmdCompereSign, m_txtCompereSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            ////������
            //m_objSign.m_mthBindEmployeeSign(m_cmdKeZhuRen, m_txtKeZhuRen, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

        }


        /// <summary>
        /// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsBeforeOperationDiscussInfo_xj objTrackInfo = new clsBeforeOperationDiscussInfo_xj();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;
            //����m_strTitle��m_dtmRecordTime
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "��ǰ���ۼ�¼";

            return objTrackInfo;
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //��վ����¼����	
            m_strCurrentOpenDate = "";
            m_txtInHospitalDiagnose.Text = "";
            m_txtSpeakRecord.m_mthClearText();
            m_txtYiJian.m_mthClearText();
            m_txtVerdict.m_mthClearText();
            m_lsvAttendeeList.Items.Clear();
            m_txtCompere.Clear();
            m_txtCompere.Tag = null;
           // m_txtCompereSign.Clear();
            //m_txtCompereSign.Tag = null;
            //m_txtRecorder.Clear();
            //m_txtRecorder.Tag = null;
            m_txtHuiBao.Clear();
            m_txtHuiBao.Tag = null;
            //m_txtKeZhuRen.Clear();
            //m_txtKeZhuRen.Tag = null;
            m_txtYiBao.Text = "";
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
        /// ��ʾû�иò���
        /// </summary>
        protected override void m_mthShowNoPatient()
        {
            clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
        }


        protected override void m_mthSetPatientBaseInfo(clsPatient p_objSelectedPatient)
        {
            if (p_objSelectedPatient.m_ObjPeopleInfo == null)
            {
                m_mthShowNoPatient();
                return;
            }
            //������ص��������Է���m_cboArea��ֵ�󴥷���SelectedIndexChanged�¼�
            m_blnCanTextChanged = false;

            if (p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo != null)
            {
                //				m_cboDept.ClearItem();
                //				m_cboArea.ClearItem();
                //				m_cboDept.AddItem(p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept);
                //				m_cboDept.SelectedIndex=0;
                //				clsInPatientArea objInPatientArea =new clsInPatientArea(p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID,p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName);
                //				m_cboArea.AddItem(objInPatientArea);
                //				m_cboArea.SelectedIndex=0;
                //				m_txtBedNO.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;				
                //ʹ���±� modified by tfzhang at 2005��10��17�� 16:02:29
                //���
                m_cboDept.ClearItem();

                //��ȡ����
                string str1 = p_objSelectedPatient.m_strDeptNewID;
                string str2;
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                clsEmrDept_VO objDeptNew;
                objDomain.m_lngGetSpecialDeptInfo(str1, out objDeptNew);
                str1 = objDeptNew.m_strSHORTNO_CHR.Trim();
                str2 = objDeptNew.m_strDEPTNAME_VCHR.Trim();
                string str11 = objDeptNew.m_strDEPTID_CHR.Trim();
                clsDepartment objDeptTemp = new clsDepartment(str1, str2);
                //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                objDeptTemp.m_strDeptNewID = str11;
                m_cboDept.AddItem(objDeptTemp);
                m_cboDept.SelectedIndex = 0;

                //��ȡ����
                m_cboArea.ClearItem();
                string str3 = p_objSelectedPatient.m_strAreaNewID;
                if (str3.Trim().Length != 0)//������Ϊ��
                {
                    string str4;
                    clsEmrDept_VO objAreNew;
                    objDomain.m_lngGetSpecialAreaInfo(str3, out objAreNew);
                    str3 = objAreNew.m_strSHORTNO_CHR;
                    str4 = objAreNew.m_strDEPTNAME_VCHR;
                    clsInPatientArea objInPatientArea = new clsInPatientArea(str3, str4, str3);
                    //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                    objInPatientArea.m_strAreaNewID = objAreNew.m_strDEPTID_CHR;
                    m_cboArea.AddItem(objInPatientArea);
                    m_cboArea.SelectedIndex = 0;
                }

                m_txtBedNO.Text = p_objSelectedPatient.m_strBedCode;
            }
            else
            {
                m_txtBedNO.Text = "";
            }

            m_objCurrentPatient = p_objSelectedPatient;

            txtInPatientID.Text = m_objCurrentPatient.m_StrHISInPatientID;
            m_txtPatientName.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrFirstName;
            lblSex.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrSex;
            lblAge.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrAge;
            m_blnCanTextChanged = true;
        }



        /// <summary>
        /// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
        /// </summary>
        /// <returns></returns>
        protected override clsTrackRecordContent m_objGetContentFromGUI()
        {
            //�������У��
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;

            //�ӽ����ȡ��ֵ
            clsBeforeOperationDiscussContent_xj objContent = new clsBeforeOperationDiscussContent_xj();
            objContent.m_dtmCreateDate = m_dtpCreateDate.Value;
            objContent.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpNo;
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion
            //   objContent.m_dtmDeadDate = m_dtpDeadDate.Value;
            objContent.m_dtmDiscussDate = m_dtpDiscussTime.Value;

            //objContent.m_strDiscussAddress = m_txtDiscussAddress.Text;
            objContent.m_strNiZheng = m_txtInHospitalDiagnose.Text;
            objContent.m_strNiZhengXML = m_txtInHospitalDiagnose.m_strGetXmlText();
            objContent.m_strHuiBao = m_txtVerdict.Text;
            objContent.m_strHuiBaoXML = m_txtVerdict.m_strGetXmlText();

            objContent.m_strTaoLunYiJian = m_txtSpeakRecord.Text;
            objContent.m_strTaoLunYiJianXML = m_txtSpeakRecord.m_strGetXmlText();

            objContent.m_strTaoLunXiaoJie = m_txtYiJian.Text;
            objContent.m_strTaoLunXiaoJieXML = m_txtYiJian.m_strGetXmlText();

            objContent.m_strYiBao = m_txtYiBao.Text;

            //objContent.m_strDeadReason = m_txtDeadReason.Text;
            //objContent.m_strDeadReasonXML = m_txtDeadReason.m_strGetXmlText();
            //objContent.m_strExperience = m_txtExperience.Text;
            //objContent.m_strExperienceXML = m_txtExperience.m_strGetXmlText();

            //�μ���Ա
            if (m_lsvAttendeeList.Items.Count > 0)
            {
                objContent.m_strAttendeeIDArr = new string[m_lsvAttendeeList.Items.Count];
                objContent.m_strAttendeeNameArr = new string[m_lsvAttendeeList.Items.Count];
                for (int i = 0; i < m_lsvAttendeeList.Items.Count; i++)
                {
                    objContent.m_strAttendeeIDArr[i] = ((clsEmrEmployeeBase_VO)m_lsvAttendeeList.Items[i].Tag).m_strEMPNO_CHR.Trim();
                    objContent.m_strAttendeeNameArr[i] = ((clsEmrEmployeeBase_VO)m_lsvAttendeeList.Items[i].Tag).m_strLASTNAME_VCHR.Trim() + ((clsEmrEmployeeBase_VO)m_lsvAttendeeList.Items[i].Tag).m_strTECHNICALRANK_CHR.Trim();
                }
            }
            else
            {
                clsPublicFunction.ShowInformationMessageBox("������һ���μ���Աǩ��!");
                return null;
            }

            //������
            if (m_txtCompere.Tag != null && m_txtCompere.Text.Trim() != "")
            {
                objContent.m_strCompereID = ((clsEmrEmployeeBase_VO)m_txtCompere.Tag).m_strEMPNO_CHR;

                clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
                objEmployeeSign.m_lngGetEmpByNO(objContent.m_strCompereID, out objEmpVO);
                // m_txtDoctorSign.Text = objEmpVO.ToString();
                objContent.m_strCompereName = objEmpVO.ToString();

                // objContent.m_strCompereName=m_txtCompere.Text;
            }
            else
            {
                clsPublicFunction.ShowInformationMessageBox("��������ǩ��!");
                return null;
            }

            #region
            //��¼��
            //if (m_txtRecorder.Tag != null && m_txtRecorder.Text.Trim() != "")
            //{
            //    objContent.m_strRecordID = ((clsEmrEmployeeBase_VO)m_txtRecorder.Tag).m_strEMPNO_CHR.Trim();
            //    clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            //    objEmployeeSign.m_lngGetEmpByNO(objContent.m_strRecordID, out objEmpVO);
            //    // m_txtDoctorSign.Text = objEmpVO.ToString();
            //    objContent.m_strRecordName = objEmpVO.ToString();

            //    //objContent.m_strRecorderName = m_txtRecorder.Text.Trim();
            //}
            //else
            //{
            //    clsPublicFunction.ShowInformationMessageBox("���¼��ǩ��!");
            //    return null;
            //}

            //������
            //if (m_txtKeZhuRen.Tag != null && m_txtKeZhuRen.Text.Trim() != "")
            //{
            //    objContent.m_strZhuRenID = ((clsEmrEmployeeBase_VO)m_txtKeZhuRen.Tag).m_strEMPNO_CHR;
            //    //objContent.m_strCompereSignName = m_txtCompereSign.Text.Trim();
            //    clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            //    objEmployeeSign.m_lngGetEmpByNO(objContent.m_strZhuRenID, out objEmpVO);
            //    // m_txtDoctorSign.Text = objEmpVO.ToString();
            //    objContent.m_strZhuRenName = objEmpVO.ToString();
            //}
            //else
            //{
            //    if (!m_BlnIsAddNew)
            //    {
            //        clsPublicFunction.ShowInformationMessageBox("�������ǩ��!");
            //        return null;
            //    }
            //}

            //�ܴ�ҽʦ
            //if (m_txtCompereSign.Tag != null && m_txtCompereSign.Text.Trim() != "")
            //{
            //    objContent.m_strGuanChuangID = ((clsEmrEmployeeBase_VO)m_txtCompereSign.Tag).m_strEMPNO_CHR.Trim();
            //    //objContent.m_strCompereSignName = m_txtCompereSign.Text.Trim();
            //    clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            //    objEmployeeSign.m_lngGetEmpByNO(objContent.m_strGuanChuangID, out objEmpVO);
            //    // m_txtDoctorSign.Text = objEmpVO.ToString();
            //    objContent.m_strGuanChuangName = objEmpVO.ToString();
            //}
            //else
            //{
            //    if (!m_BlnIsAddNew)
            //    {
            //        clsPublicFunction.ShowInformationMessageBox("��ܴ�ҽʦǩ��!");
            //        return null;
            //    }
            //}

            //objContent.m_strGuanChuangID = ((clsEmrEmployeeBase_VO)m_txtCompereSign.Tag).m_strEMPNO_CHR.Trim();
            //objContent.m_strGuanChuangName = m_txtCompereSign.ToString();
            #endregion 
            //��ʷ�㱨��
            if (m_txtHuiBao.Tag != null && m_txtHuiBao.Text.Trim() != "")
            {
                objContent.m_strHuiBaoID = ((clsEmrEmployeeBase_VO)m_txtHuiBao.Tag).m_strEMPNO_CHR;
                //objContent.m_strCompereSignName = m_txtCompereSign.Text.Trim();
                clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
                objEmployeeSign.m_lngGetEmpByNO(objContent.m_strHuiBaoID, out objEmpVO);
                // m_txtDoctorSign.Text = objEmpVO.ToString();
                objContent.m_strHuiBaoName = objEmpVO.ToString();
            }
            else
            {                
                    clsPublicFunction.ShowInformationMessageBox("��ʷ�㱨��ǩ��!");
                    return null;                
            }
            //objContent.m_strHuiBaoID = ((clsEmrEmployeeBase_VO)m_txtHuiBao.Tag).m_strEMPNO_CHR.Trim();
            //objContent.m_strHuiBaoName = m_txtHuiBao.ToString();

            //if (!string.IsNullOrEmpty(objContent.m_strCompereSignID)
            //    && objContent.m_strCompereSignID.Trim() != objContent.m_strCompereID.Trim())
            //{
            //    if (clsPublicFunction.ShowInformationMessageBox("��д��������������������ǩ����ͬ���Ƿ�������棿", MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        return null;
            //    }
            //}
            return objContent;
        }



        /// <summary>
        /// �������¼��ֵ��ʾ�������ϡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsBeforeOperationDiscussContent_xj objContent = (clsBeforeOperationDiscussContent_xj)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_strCurrentOpenDate = objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");

            //   m_dtpDeadDate.Value = objContent.m_dtmDeadDate;
            m_dtpDiscussTime.Value = objContent.m_dtmDiscussDate;
            //  m_txtDiscussAddress.Text = objContent.m_strDiscussAddress;

            m_txtInHospitalDiagnose.m_mthSetNewText(objContent.m_strNiZheng, objContent.m_strNiZhengXML);
            m_txtSpeakRecord.m_mthSetNewText(objContent.m_strTaoLunYiJian, objContent.m_strTaoLunYiJianXML);
            m_txtYiJian.m_mthSetNewText(objContent.m_strTaoLunXiaoJie, objContent.m_strTaoLunXiaoJieXML);
            m_txtVerdict.m_mthSetNewText(objContent.m_strHuiBao, objContent.m_strHuiBaoXML);
            m_txtYiBao.Text = objContent.m_strYiBao;
            //m_txtDeadReason.m_mthSetNewText(objContent.m_strDeadReason, objContent.m_strDeadReasonXML);
            //m_txtExperience.m_mthSetNewText(objContent.m_strExperience, objContent.m_strExperienceXML);

            #region ǩ��
            clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            if (objContent.m_strAttendeeIDArr != null)
            {
                for (int i = 0; i < objContent.m_strAttendeeIDArr.Length; i++)
                {
                    ListViewItem lviNewItem = new ListViewItem(new string[] { objContent.m_strAttendeeNameArr[i].Trim(), objContent.m_strAttendeeIDArr[i].Trim() });
                    //tag��Ϊ����
                    objEmployeeSign.m_lngGetEmpByNO(objContent.m_strAttendeeIDArr[i].Trim(), out objEmpVO);
                    lviNewItem.SubItems.Add(objEmpVO.m_strLEVEL_CHR);
                    lviNewItem.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTechnicalRank;
                    lviNewItem.Tag = objEmpVO;
                    //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                    m_lsvAttendeeList.Items.Add(lviNewItem);
                    //m_lsvAttendeeList.Items.Add(objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTechnicalRank);
                }
            }

            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strCompereID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtCompere.Tag = objEmpVO;
                //  m_txtCompere.Text = objContent..m_strCompereName;
                m_txtCompere.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            }

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strRecordID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtRecorder.Tag = objEmpVO;
            //    //  m_txtRecorder.Text = objContent.m_strRecorderName;
            //    m_txtRecorder.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            //}

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strGuanChuangID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtCompereSign.Tag = objEmpVO;
            //    //  m_txtCompereSign.Text = objContent.m_strCompereSignName;
            //    m_txtCompereSign.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            //}

            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strHuiBaoID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtHuiBao.Tag = objEmpVO;
                //  m_txtCompereSign.Text = objContent.m_strCompereSignName;
                m_txtHuiBao.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            }

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strZhuRenID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtKeZhuRen.Tag = objEmpVO;
            //    //  m_txtCompereSign.Text = objContent.m_strCompereSignName;
            //    m_txtKeZhuRen.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            //}


            #endregion ǩ��
        }



        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼���ڣ��˴���ʾCreateDate</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            if (m_objBaseCurrentPatient == null || m_objBaseCurrentPatient.m_StrInPatientID == null || m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue || p_dtmRecordDate == DateTime.MinValue)
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
                return;
            }

            clsTrackRecordContent objContent = null;
            long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
            if (lngRes > 0 && objContent != null)
            {
                m_mthSetDeletedGUIFromContent(objContent);
            }

        }

        public override int m_IntFormID
        {
            get
            {
                return 194;
            }
        }


        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsBeforeOperationDiscussContent_xj objContent = (clsBeforeOperationDiscussContent_xj)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            //m_dtpDeadDate.Value = objContent.m_dtmDeadDate;
            m_dtpDiscussTime.Value = objContent.m_dtmDiscussDate;
            //m_txtDiscussAddress.Text = objContent.m_strDiscussAddress;

            m_txtInHospitalDiagnose.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strNiZheng, objContent.m_strNiZhengXML);
            m_txtSpeakRecord.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strTaoLunYiJian, objContent.m_strTaoLunYiJianXML);
            m_txtYiJian.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strTaoLunXiaoJie, objContent.m_strTaoLunXiaoJieXML);
            //m_txtDeadReason.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strDeadReason, objContent.m_strDeadReasonXML);
            m_txtVerdict.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strHuiBao, objContent.m_strHuiBaoXML);
            //   m_txtExperience.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(objContent.m_strExperience, objContent.m_strExperienceXML);
            m_txtYiBao.Text = objContent.m_strYiBao;

        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.BeforeOperationDiscuss_xj);
        }

        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
            clsBeforeOperationDiscussContent_xj objContent = (clsBeforeOperationDiscussContent_xj)p_objRecordContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_dtpDiscussTime.Value = objContent.m_dtmDiscussDate;
            //  m_txtDiscussAddress.Text = objContent.m_strDiscussAddress;

            m_txtInHospitalDiagnose.m_mthSetNewText(objContent.m_strNiZheng, objContent.m_strNiZhengXML);
            m_txtSpeakRecord.m_mthSetNewText(objContent.m_strTaoLunYiJian, objContent.m_strTaoLunYiJianXML);
            m_txtYiJian.m_mthSetNewText(objContent.m_strTaoLunXiaoJie, objContent.m_strTaoLunXiaoJieXML);
            m_txtVerdict.m_mthSetNewText(objContent.m_strHuiBao, objContent.m_strHuiBaoXML);
            m_txtYiBao.Text = objContent.m_strYiBao;


            #region ǩ��
            clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            if (objContent.m_strAttendeeIDArr != null)
            {
                for (int i = 0; i < objContent.m_strAttendeeIDArr.Length; i++)
                {
                    ListViewItem lviNewItem = new ListViewItem(new string[] { objContent.m_strAttendeeNameArr[i].Trim(), objContent.m_strAttendeeIDArr[i] });
                    //tag��Ϊ����
                    objEmployeeSign.m_lngGetEmpByNO(objContent.m_strAttendeeIDArr[i], out objEmpVO);
                    lviNewItem.SubItems.Add(objEmpVO.m_strLEVEL_CHR);
                    lviNewItem.Tag = objEmpVO;
                    //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                    m_lsvAttendeeList.Items.Add(lviNewItem);
                }
            }

            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strCompereID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtCompere.Tag = objEmpVO;
                m_txtCompere.Text = objContent.m_strCompereName;
            }

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strRecordID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtRecorder.Tag = objEmpVO;
            //    m_txtRecorder.Text = objContent.m_strRecordName;
            //}

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strGuanChuangID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtCompereSign.Tag = objEmpVO;
            //    m_txtCompereSign.Text = objContent.m_strGuanChuangName;
            //}

            objEmployeeSign.m_lngGetEmpByNO(objContent.m_strHuiBaoID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtHuiBao.Tag = objEmpVO;
                //  m_txtCompereSign.Text = objContent.m_strCompereSignName;
                m_txtHuiBao.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            }

            //objEmployeeSign.m_lngGetEmpByNO(objContent.m_strZhuRenID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    m_txtKeZhuRen.Tag = objEmpVO;
            //    //  m_txtCompereSign.Text = objContent.m_strCompereSignName;
            //    m_txtKeZhuRen.Text = objEmpVO.m_strLASTNAME_VCHR + " " + objEmpVO.m_strTECHNICALRANK_CHR;
            //}

            #endregion ǩ��
        }


        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "��ǰ���ۼ�¼";
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected override void m_mthSelectRootNode()
        {
            if (m_objCurrentPatient != null)
            {
                clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
                {
                    m_txtInHospitalDiagnose.Text = objInPatientCaseHisoryDefaultValueArr[0].m_strPrimaryDiagnose;
                }
            }
        }


        #region ǩ��
        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:// enter
                    break;

                case 38:
                case 40:
                    break;
                case 46://Keys.Delete
                    break;
            }
        }

        #endregion ǩ��

        #region ��Ӽ��̿�ݼ�
        private void m_mthSetQuickKeys()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Lable" && strTypeName != "Button")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// ��ճ���ǰ�ؼ���������д�������,(�ɸ����ṩ�µ�ʵ��)
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_blnReadOnly"></param>
        protected override void m_mthClearAllInfo(Control p_ctlControl)
        {
            if (p_ctlControl == null)// || m_lblInHospitalTime == null)
                return;
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
            {
                if (p_ctlControl is iCare.CustomForm.ctlRichTextBox)//�Զ�����е�cltRichTextBox
                    ((iCare.CustomForm.ctlRichTextBox)p_ctlControl).Text = "";
                else
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_mthClearText();
            }
            else if (strTypeName == "ctlBorderTextBox" && p_ctlControl.Name != "txtInPatientID" && p_ctlControl.Name != "m_txtPatientName" && p_ctlControl.Name != "m_txtBedNO" && p_ctlControl.Name != "m_txtRecorder")
                ((ctlBorderTextBox)p_ctlControl).Text = "";
            else if (strTypeName == "TreeView")
            {
                if (((TreeView)p_ctlControl).Nodes.Count > 0)
                    ((TreeView)p_ctlControl).Nodes[0].Nodes.Clear();
            }
            else if (strTypeName == "ListView")
                ((ListView)p_ctlControl).Items.Clear();
            else if (strTypeName == "DateTimePicker")
                ((DateTimePicker)p_ctlControl).Value = DateTime.Now;
            //m_lblInHospitalTime.Text = "";
            if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthClearAllInfo(subcontrol);
                }
            }
        }

        //private void frmDifficultCaseDiscussCountent_XJ_Load(object sender, EventArgs e)
        //{
           
        //}

        private void frmBeforeOperationDiscuss_xj_Load(object sender, EventArgs e)
        {
            m_mthfrmLoad();
            //if(m_objCurrentPatient !=null)
            //{
            //    m_lblInHospitalTime.Text=m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��");
            //}
            this.m_dtpCreateDate.m_EnmVisibleFlag = MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.m_dtpCreateDate.m_mthResetSize();

            m_trvCreateDate.Focus();
        }


        protected override void m_mthSetPatientInHospitalDate(clsPatient p_objSelectedPatient)
        {
            //�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
            if (p_objSelectedPatient == null)
                return;

            //��¼������Ϣ
            m_objCurrentPatient = p_objSelectedPatient;
            //m_lblInHospitalTime.Text=m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��");

        }


        /// <summary>
        /// ���ݸ���
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthDataShare(clsPatient p_objSelectedPatient)
        {
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseDefaultValue = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString());
            if (objInPatientCaseDefaultValue != null && objInPatientCaseDefaultValue.Length > 0)
            {
                this.m_txtInHospitalDiagnose.Text = objInPatientCaseDefaultValue[0].m_strPrimaryDiagnose;
            }
        }


        #region ���
        private string m_strCurrentOpenDate = "";
        protected override string m_StrCurrentOpenDate
        {
            get
            {
                if (m_strCurrentOpenDate == "")
                {
                    clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                    return "";
                }
                return m_strCurrentOpenDate;

            }
        }

        protected override bool m_BlnCanApprove
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region ����
        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.CaseHistory; }
        }
        protected override string m_StrRecorder_ID
        {
            get
            {
                if (m_txtCompere.Tag != null)
                    return ((clsEmrEmployeeBase_VO)m_txtCompere.Tag).m_strEMPNO_CHR.Trim();
                return "";
            }
        }

        #endregion ����

        #region �ⲿ��ӡ.

        //System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        private void m_mthfrmLoad()
        {
            if (m_pdcPrintDocument == null)
                this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
        }
        private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);
        }

        private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }

        //		private bool m_blnHasInitPrintTool=false;
        clsBeforeOperationDiscussPrintTool_xj objPrintTool;
        private void m_mthDemoPrint_FromDataSource()
        {
            //			if(m_blnHasInitPrintTool==false)
            //			{
            objPrintTool = new clsBeforeOperationDiscussPrintTool_xj();
            objPrintTool.m_mthInitPrintTool(null);
            //				m_blnHasInitPrintTool=true;
            //			}
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                m_objBaseCurrentPatient.m_DtmSelectedHISInDate = m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate;
                if (m_objCurrentRecordContent == null)
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objBaseCurrentPatient.m_DtmSelectedInDate, DateTime.MinValue);
                else
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objBaseCurrentPatient.m_DtmSelectedInDate, m_objCurrentRecordContent.m_dtmOpenDate);
            }
            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint_this();
        }

        private void m_mthStartPrint_this()
        {
            if (m_blnDirectPrint)
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

        protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
        {
            m_mthDemoPrint_FromDataSource();
            return 1;
        }
        #endregion �ⲿ��ӡ.


        #region ��������

        protected override bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            bool blnIsOK = false;
            if (p_objSelectedValue != null)
            {
                clsTrackRecordContent m_objContent = new clsBeforeOperationDiscussContent_xj();

                long lngRes = m_objGetDiseaseTrackDomain().m_lngGetDeleteRecordContent(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objContent);
                if (lngRes <= 0 || m_objContent == null)
                {
                    switch (lngRes)
                    {
                        case (long)(iCareData.enmOperationResult.Not_permission):
                            m_mthShowNotPermitted(); break;
                        case (long)(iCareData.enmOperationResult.DB_Fail):
                            m_mthShowDBError(); break;
                    }
                    return blnIsOK;
                }
                clsBeforeOperationDiscussContent_xj p_objContent = (clsBeforeOperationDiscussContent_xj)m_objContent;

                this.m_dtpDiscussTime.Text = p_objContent.m_dtmDiscussDate.ToString("yyyy-MM-dd hh:mm:ss");
                //   this.m_txtDiscussAddress.Text = p_objContent.m_strDiscussAddress;
                this.m_txtCompere.Text = p_objContent.m_strCompereName;
                //this.m_lsvAttendeeList= p_objContent.m_strAttendeeNameArr
                this.m_lsvAttendeeList.Items.Clear();
                string[] strAttendeeName = p_objContent.m_strAttendeeNameArr;
                ListViewItem lviAttendeeName = null;
                foreach (string i in strAttendeeName)
                {
                    lviAttendeeName = new ListViewItem();
                    lviAttendeeName.Text = i.ToString();
                    this.m_lsvAttendeeList.Items.Add(lviAttendeeName);
                }
                this.m_txtInHospitalDiagnose.Text = p_objContent.m_strNiZheng;
                this.m_txtSpeakRecord.Text = p_objContent.m_strTaoLunYiJian;
                this.m_txtYiJian.Text = p_objContent.m_strTaoLunXiaoJie;
                this.m_txtVerdict.Text = p_objContent.m_strHuiBao;

                //this.m_txtDeadDiagnose.Text = p_objContent.m_strDeadDiagnose;
                //this.m_txtDeadReason.Text = p_objContent.m_strDeadReason;
                this.m_txtHuiBao.Text = p_objContent.m_strHuiBaoName;
                //this.m_txtRecorder.Text = p_objContent.m_strRecordName;
                //this.m_txtCompereSign.Text = p_objContent.m_strGuanChuangName;
                //this.m_txtKeZhuRen.Text = p_objContent.m_strZhuRenName;
                //clsEmrSignToolCollection m_objSign = new clsEmrSignToolCollection();
                m_txtYiBao.Text = p_objContent.m_strYiBao;               

                blnIsOK = true;
            }
            return blnIsOK;
        }

        //infPrintRecord objPrintTool;
        protected override void m_mthSubPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            if (p_objSelectedValue == null) return;
            objPrintTool = new clsBeforeOperationDiscussPrintTool_xj();

            if (m_objBaseCurrentPatient != null)
            {
                objPrintTool.m_mthInitPrintTool(null);
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,
                    p_objSelectedValue.m_DtmInpatientDate,
                    p_objSelectedValue.m_DtmOpenDate);
                clsPrintInfo_BeforeOperationDiscuss_xj objPrintInfo = new clsPrintInfo_BeforeOperationDiscuss_xj();


                //objPrintInfo.m_dtmHISInDate = p_objSelectedValue.m_DtmInpatientDate;  //???BEFOREOPERATION
                objPrintInfo.m_dtmInPatientDate = p_objSelectedValue.m_DtmInpatientDate;
                objPrintInfo.m_dtmOpenDate = p_objSelectedValue.m_DtmOpenDate;
                //objPrintInfo.m_strAge = p_objSelectedValue;           
                //objPrintInfo.m_strAreaName
                //objPrintInfo.m_strBedName
                //objPrintInfo.m_strDeptName=
                //objPrintInfo.m_strHISInPatientID=
                objPrintInfo.m_strInPatentID = p_objSelectedValue.m_StrInpatientId;
                //objPrintInfo.m_strPatientName =
                //objPrintInfo.m_strSex=


                clsTrackRecordContent p_objContent = new clsBeforeOperationDiscussContent_xj();
                long lngRes = m_objGetDiseaseTrackDomain().m_lngGetDeleteRecordContent(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objContent);
                clsBeforeOperationDiscussContent_xj objContent = (clsBeforeOperationDiscussContent_xj)p_objContent;
                //objPrintInfo.m_objContent = objContent;
                objPrintInfo.m_objRecordContent = objContent;
                //objPrintInfo.m_blnIsFirstPrint = false;

                objPrintTool.m_mthSetPrintContent(objPrintInfo);



                m_mthStartPrint();
                //ppdPrintPreview.Document = m_pdcPrintDocument;
                //ppdPrintPreview.ShowDialog(p_infOwner);
            }
        }

        protected override clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            clsInactiveRecordInfo_VO[] objArr = null;

            new clsInPatientCaseHistoryDomain().m_lngGetAllInactiveInfo(p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate, out objArr);
            return objArr;
        }
        public override bool m_blnIsNewSetInactiveForm
        {
            get { return true; }
        }
        #endregion ��������

       

    }
}