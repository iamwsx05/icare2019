using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ΢��Ѫ�Ǽ������¼¼�����
    /// </summary>
    public partial class frmMiniBooldSugarContent : iCare.frmDiseaseTrackBase
    {

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmMiniBooldSugarContent(bool blnAdd)
        {
            InitializeComponent();
            //ָ����ʿ����վ��
            intFormType = 2;
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();

            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse,��¼Ա��);
            m_objSign.m_mthBindEmployeeSign(m_cmbsign, txtSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);

            //��ʶ������
            blnAddRecord = blnAdd;

            //��ʼ��richtextbox�ؼ�
            m_mthSetRichTextBoxAttribInControl(this);

        }
        #endregion

        #region �ֶ�
        /// <summary>
        /// ���˼�¼סԺ��
        /// </summary>
        public string strRecordInPatientID;
        /// <summary>
        /// ���˼�¼��Ժʱ��
        /// </summary>
        public string strRecordInPatientDate;
        /// <summary>
        /// ���˼�¼����ʱ��
        /// </summary>
        public string strRecordCreateDate;
        /// <summary>
        /// ����ǩ����
        /// </summary>
        private clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// ��ǰ������¼
        /// </summary>
        private clsMiniBloodSugarChkValue_GX m_objRecord;

        /// <summary>
        /// ��ʶ��ǰ��¼���������������޸�
        /// true��������false���޸ģ�
        /// Ĭ������
        /// </summary>
        private bool blnAddRecord = true;

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�����Ӽ�¼��true������ӣ�false���޸ġ�
        /// </summary>
        protected override bool m_BlnIsAddNew
        {
            get
            {
                return blnAddRecord;
            }
        }
        #endregion

        #region ����
        // <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        private void m_mthClearRecordInfo()
        {
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(txtSign);
            //��ռ�¼����
            cmbMealType.Text = "";
            m_txtValue.m_mthClearText();
            m_txtDesription.Text = "";
            m_dtpCreateDate.Value = DateTime.Now;
            //Ĭ�Ͽؼ�����ۼ�
            m_mthSetModifyControl(null, true);
        }


        /// <summary>
        /// ���򿪴���ʱload����
        /// </summary>
        private void m_mthLoad()
        {
            //clsMiniBloodSugarChk_GXServ objservice =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            //������
            if (strRecordInPatientID == null || strRecordInPatientID == string.Empty)
            {
                MessageBox.Show("δָ����¼", "iCare message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecoedByInPatient(  strRecordInPatientID, DateTime.Parse(strRecordInPatientDate), DateTime.Parse(strRecordCreateDate), out m_objRecord);
                if (m_objRecord != null)
                {
                    m_mthSetGUIFromContent(m_objRecord);

                    m_mthEnableModify(false);

                    m_mthSetModifyControl(m_objRecord, false);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //objservice = null;
            }

        }
        /// <summary>
        /// ��ָ���ļ�¼����load��������
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;
            clsMiniBloodSugarChkValue_GX objContent = (clsMiniBloodSugarChkValue_GX)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
            m_dtpCreateDate.Value = objContent.m_dtmOpenDate;
            cmbMealType.Text = objContent.m_strMeatType;
            m_txtValue.Text = objContent.m_strBloodSugar;
            m_txtValue.m_mthSetNewText(objContent.m_strBloodSugar, objContent.m_strBloodSugarXML);
            m_txtDesription.Text = objContent.m_strDescription;

            #region ǩ������
            m_mthAddSignToTextBoxBase(new TextBoxBase[] { txtSign }, p_objContent.objSignerArr, new bool[] { true }, false);
            //txtSign.Clear();
            //if (objContent.objSignerArr != null)
            //{
            //    for (int i = 0; i < objContent.objSignerArr.Length; i++)
            //    {
            //        if (objContent.objSignerArr[i].controlName == "txtSign")
            //        {
            //            txtSign.Text = objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR;
            //            txtSign.Tag = objContent.objSignerArr[i].objEmployee; ;
            //        }
            //    }
            //}
            #endregion ǩ��		
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubAddNew()
        {
            long lngRes = 0;

            //clsMiniBloodSugarChk_GXServ objservice =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            try
            {

                #region ��ȡ����
                //��ȡ����
                m_objRecord.m_strInPatientID = strRecordInPatientID;
                m_objRecord.m_dtmInPatientDate = DateTime.Parse(strRecordInPatientDate);
                m_objRecord.m_dtmOpenDate = m_dtpCreateDate.Value; //��¼ʱ��ȡ�����Ͽؼ�ֵ
                m_objRecord.m_dtmCreateDate = DateTime.Now;          //����ʱ��ȡ��ǰʱ��
                m_objRecord.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;
                m_objRecord.m_strMeatType = cmbMealType.Text;
                m_objRecord.m_strBloodSugar_Right = m_txtValue.m_strGetRightText();
                m_objRecord.m_strBloodSugar = m_txtValue.Text;
                m_objRecord.m_strBloodSugarXML = m_txtValue.m_strGetXmlText();
                m_objRecord.m_strDescription = m_txtDesription.Text;

                //��ȡlsvsignǩ��
                m_objRecord.objSignerArr = new clsEmrSigns_VO[1];
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { txtSign }, ref m_objRecord.objSignerArr, ref strUserIDList, ref strUserNameList);
                //for (int i = 0; i < 1; i++)
                //{
                //    m_objRecord.objSignerArr[i] = new clsEmrSigns_VO();
                //    m_objRecord.objSignerArr[i].objEmployee = new clsEmrEmployeeBase_VO();
                //    m_objRecord.objSignerArr[i].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
                //    m_objRecord.objSignerArr[i].controlName = "txtSign";
                //    m_objRecord.objSignerArr[i].m_strFORMID_VCHR = "frmMiniBooldSugarContent";//ע���Сд
                //    m_objRecord.objSignerArr[i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
                //    //�ۼ���ʽ 0972,0324,

                //    strUserIDList = strUserIDList + m_objRecord.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim() + ",";
                //    strUserNameList = strUserNameList + m_objRecord.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim() + ",";
                //}
                m_objRecord.m_strModifyUserID = strUserIDList;
                #endregion

                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < m_objRecord.objSignerArr.Length; i++)
                {
                    if (m_objRecord.objSignerArr[i].controlName == "lsvSign" || m_objRecord.objSignerArr[i].controlName == "txtSign")
                        objSignerArr.Add(m_objRecord.objSignerArr[i].objEmployee);
                }
                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = m_objRecord.m_strInPatientID.Trim() + "-" + m_objRecord.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(m_objRecord, objSign_VO) == -1)
                    return -1;
                //�����¼
                clsPreModifyInfo objModifyInfo = null;
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewRecoed(  m_objRecord);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //objservice = null;
            }

            return lngRes;

        }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubModify()
        {
            long lngRes = 0;

            //clsMiniBloodSugarChk_GXServ objservice =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            try
            {

                #region ��ȡ����
                //��ȡ����
                m_objRecord.m_strInPatientID = strRecordInPatientID;
                m_objRecord.m_dtmInPatientDate = DateTime.Parse(strRecordInPatientDate);
                m_objRecord.m_dtmOpenDate = m_dtpCreateDate.Value; //��¼ʱ��ȡ�����Ͽؼ�ֵ
                m_objRecord.m_dtmCreateDate = DateTime.Now;          //����ʱ��ȡ��ǰʱ��
                m_objRecord.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;
                m_objRecord.m_dtmModifyDate = DateTime.Now;
                m_objRecord.m_strMeatType = cmbMealType.Text;
                m_objRecord.m_strBloodSugar_Right = m_txtValue.m_strGetRightText();
                m_objRecord.m_strBloodSugar = m_txtValue.Text;
                m_objRecord.m_strBloodSugarXML = m_txtValue.m_strGetXmlText();
                m_objRecord.m_strDescription = m_txtDesription.Text;

                //��ȡlsvsignǩ��
                m_objRecord.objSignerArr = new clsEmrSigns_VO[1];
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { txtSign }, ref m_objRecord.objSignerArr, ref strUserIDList, ref strUserNameList);
                //for (int i = 0; i < 1; i++)
                //{
                //    m_objRecord.objSignerArr[i] = new clsEmrSigns_VO();
                //    m_objRecord.objSignerArr[i].objEmployee = new clsEmrEmployeeBase_VO();
                //    m_objRecord.objSignerArr[i].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
                //    m_objRecord.objSignerArr[i].controlName = "txtSign";
                //    m_objRecord.objSignerArr[i].m_strFORMID_VCHR = "frmMiniBooldSugarContent";//ע���Сд
                //    m_objRecord.objSignerArr[i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
                //    //�ۼ���ʽ 0972,0324,

                //    strUserIDList = strUserIDList + m_objRecord.objSignerArr[i].objEmployee.m_strEMPID_CHR.Trim() + ",";
                //    strUserNameList = strUserNameList + m_objRecord.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR.Trim() + ",";
                //}
                m_objRecord.m_strModifyUserID = strUserIDList;
                #endregion

                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < m_objRecord.objSignerArr.Length; i++)
                {
                    if (m_objRecord.objSignerArr[i].controlName == "lsvSign" || m_objRecord.objSignerArr[i].controlName == "txtSign")
                        objSignerArr.Add(m_objRecord.objSignerArr[i].objEmployee);
                }
                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = m_objRecord.m_strInPatientID.Trim() + "-" + m_objRecord.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(m_objRecord, objSign_VO) == -1)
                    return -1;
                //�����¼
                clsPreModifyInfo objModifyInfo = null;
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifyRecoed(  m_objRecord);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //objservice = null;
            }

            return lngRes;
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubDelete()
        {
            //return base.m_lngSubDelete();
            return 1;
        }
        #endregion

        #region �¼�

        /// <summary>
        /// ����load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMiniBooldSugarContent_Load(object sender, EventArgs e)
        {
            if (blnAddRecord != null)
            {
                m_mthLoad();
            }
            else
            {
                m_mthClearRecordInfo();
            }
        }
        /// <summary>
        /// ȷ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
        /// <summary>
        /// ȡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


    }
}