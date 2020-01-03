using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;
using com.digitalwave.emr.BEDExplorer;
using com.digitalwave.controls;

namespace iCare
{
   /// <summary>
   /// �ڷ��ڿ�Ѫ�ǹ۲��
   /// </summary>
    public partial class frmEMR_IntBloodSugarWatchcon : frmDiseaseTrackBase
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ����ǩ����

        /// </summary>
        private clsEmrSignToolCollection m_objSign = null;
        #endregion
        public frmEMR_IntBloodSugarWatchcon()
        {
            InitializeComponent();
            m_objSign = new clsEmrSignToolCollection();

            //����ָ��Ա��ID
            m_objSign.m_mthBindEmployeeSign(m_cmdSign, lsvSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_mthSetRichTextBoxAttribInControl(this);
            m_mthThisAddRichTextInfo(this);
        }

        private void m_cmdOK_Click(object sender, EventArgs e)
        {
            if (m_lngSave() > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }
        #region ����������
        #region ����ID
        public override int m_IntFormID
        {
            get
            {
                return 191;
            }
        }
                #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��

            return "�ڷ��ڿ�Ѫ�ǹ۲��";
        }
        #endregion

        #region ��ȡ�����¼�������ʵ��
        /// <summary>
        /// ��ȡ�����¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ�����¼�������ʵ�������Ӵ�������ʵ��

            return new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_IntBloodSugarWatch);
        }
        #endregion

        #region ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ������
        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ������
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ��

            clsEMR_intbloodsugarwatchValue objContent = (clsEMR_intbloodsugarwatchValue)p_objRecordContent;
        }
        #endregion

        #region �ӽ����ȡ��¼����



        /// <summary>
        /// �ӽ����ȡ��¼����

        /// </summary>
        /// <returns></returns>
        protected override weCare.Core.Entity.clsTrackRecordContent m_objGetContentFromGUI()
        {

            //�������У��
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;

            //�ӽ����ȡ��ֵ		
            clsEMR_intbloodsugarwatchValue objContent = new clsEMR_intbloodsugarwatchValue();
            try
            {
                string StrNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objContent.m_dtmCreateDate = Convert.ToDateTime(StrNow);
                objContent.m_strCreateUserID = clsEMRLogin.LoginInfo.m_strEmpID;
                objContent.m_dtmModifyDate = Convert.ToDateTime(StrNow);
                objContent.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpID;
                objContent.m_strRegisterID = frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;

                objContent.m_dtmRecordDate = Convert.ToDateTime(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                //objContent.m_strCHECKRESULT_RIGHT = m_txtCheckResult.m_strGetRightText();
                //objContent.m_strCHECKRESULT_VCHR = m_txtCheckResult.Text;
                //objContent.m_strCHECKRESULT_XML = m_txtCheckResult.m_strGetXmlText();

                //objContent.m_strCHECKTIME_RIGHT = m_cboCheckTime.Text;
                //objContent.m_strCHECKTIME_VCHR = m_cboCheckTime.Text;
                //objContent.m_strCHECKTIME_XML = "<root />";

                objContent.m_strNULLABDOMEN_RIGHT = m_txtnullAbdon.m_strGetRightText();
                objContent.m_strNULLABDOMEN_VCHR = m_txtnullAbdon.Text;
                objContent.m_strNULLABDOMEN_XML = m_txtnullAbdon.m_strGetXmlText();

                objContent.m_strTWOBREAKFAST_RIGTH = m_txttwoBreakfast.m_strGetRightText();
                objContent.m_strTWOBREAKFAST_VCHR = m_txttwoBreakfast.Text;
                objContent.m_strTWOBREAKFAST_XML = m_txttwoBreakfast.m_strGetXmlText();

                objContent.m_strBEFORELUNCH_RIGHT = m_txtBeforeLunch.m_strGetRightText();
                objContent.m_strBEFORELUNCH_VCHR = m_txtBeforeLunch.Text;
                objContent.m_strBEFORELUNCH_XML = m_txtBeforeLunch.m_strGetXmlText();

                objContent.m_strTWOAFTERLUNCH_RIGHT = m_txttwoLunch.m_strGetRightText();
                objContent.m_strTWOAFTERLUNCH_VCHR = m_txttwoLunch.Text;
                objContent.m_strTWOAFTERLUNCH_XML = m_txttwoLunch.m_strGetXmlText();

                objContent.m_strBEFOREDINNER_RIGHT = m_txtBeforeDinner.m_strGetRightText();
                objContent.m_strBEFOREDINNER_VCHR = m_txtBeforeDinner.Text;
                objContent.m_strBEFOREDINNER_XML = m_txtBeforeDinner.m_strGetXmlText();


                objContent.m_strTWOAFTERDINNER_RIGHT = m_txttwoDinner.m_strGetRightText();
                objContent.m_strTWOAFTERDINNER_VCHR = m_txttwoDinner.Text;
                objContent.m_strTWOAFTERDINNER_XML = m_txttwoDinner.m_strGetXmlText();

                objContent.m_strBEFORESLEEP_RIGHT = m_txtBeforeSleep.m_strGetRightText();
                objContent.m_strBEFORESLEEP_VCHR = m_txtBeforeSleep.Text;
                objContent.m_strBEFORESLEEP_XML = m_txtBeforeSleep.m_strGetXmlText();

                objContent.m_strBEIZHU_RIGHT = m_txtBeizhu.m_strGetRightText();
                objContent.m_strBEIZHU_VCHR = m_txtBeizhu.Text;
                objContent.m_strBEIZHU_XML = m_txtBeizhu.m_strGetXmlText();

                #region ��ȡǩ��
                objContent.objSignerArr = null;
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);

                objContent.m_strRecordUserID = strUserIDList;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return objContent;
        }
        #endregion

        #region ��ʾ��ɾ����¼������
        /// <summary>
        /// ��ʾ��ɾ����¼������
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsEMR_intbloodsugarwatchValue objContent = p_objContent as clsEMR_intbloodsugarwatchValue;

            if (objContent == null)
                return;

            this.m_mthClearRecordInfo();

            m_dtpCreateDate.Value = objContent.m_dtmRecordDate;

            //m_txtCheckResult.Text = objContent.m_strCHECKRESULT_RIGHT;
            //m_cboCheckTime.Text = objContent.m_strCHECKTIME_RIGHT;
            m_txtnullAbdon.Text = objContent.m_strNULLABDOMEN_RIGHT;
            m_txttwoBreakfast.Text = objContent.m_strTWOBREAKFAST_RIGTH;
            m_txtBeforeLunch.Text = objContent.m_strBEFORELUNCH_RIGHT;
            m_txttwoLunch.Text = objContent.m_strTWOAFTERLUNCH_RIGHT;
            m_txtBeforeDinner.Text = objContent.m_strBEFOREDINNER_RIGHT;
            m_txttwoDinner.Text = objContent.m_strTWOAFTERDINNER_RIGHT;
            m_txtBeforeSleep.Text = objContent.m_strBEFORESLEEP_RIGHT;
            m_txtBeizhu.Text = objContent.m_strBEIZHU_RIGHT;


            #region ǩ������
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }
            else
            {
                clsEmrEmployeeBase_VO objEMP = null;
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                long lngRes = objDomain.m_lngGetEmpByID(objContent.m_strCreateUserID, out objEMP);
                if (objEMP != null)
                {
                    ListViewItem lviNewItem = new ListViewItem(objEMP.m_strGetTechnicalRankAndName);
                    lviNewItem.SubItems.Add(objEMP.m_strEMPID_CHR);
                    lviNewItem.SubItems.Add(objEMP.m_StrHistroyLevel);
                    lviNewItem.Tag = objEMP;
                    lsvSign.Items.Add(lviNewItem);
                }
                objDomain = null;
            }
            #endregion ǩ��

            this.lsvSign.Enabled = false;
            this.m_cmdSign.Enabled = false;
            this.m_dtpCreateDate.Enabled = false;
        }
        #endregion

        #region �������¼��ֵ��ʾ��������

        /// <summary>
        /// �������¼��ֵ��ʾ��������

        /// </summary>
        /// <param name="p_objContent">VO</param>
        protected override void m_mthSetGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsEMR_intbloodsugarwatchValue objContent = p_objContent as clsEMR_intbloodsugarwatchValue;
            m_cmdModifyPatientInfo.Visible = false;
            if (objContent == null)
                return;

            this.m_mthClearRecordInfo();

            m_dtpCreateDate.Value = objContent.m_dtmRecordDate;
            //m_txtCheckResult.m_mthSetNewText(objContent.m_strCHECKRESULT_VCHR, objContent.m_strCHECKRESULT_XML);
            //m_cboCheckTime.Text = objContent.m_strCHECKTIME_RIGHT;
            m_txtnullAbdon.m_mthSetNewText(objContent.m_strNULLABDOMEN_VCHR, objContent.m_strNULLABDOMEN_XML);
            m_txttwoBreakfast.m_mthSetNewText(objContent.m_strTWOBREAKFAST_VCHR, objContent.m_strTWOBREAKFAST_XML);
            m_txtBeforeLunch.m_mthSetNewText(objContent.m_strBEFORELUNCH_VCHR, objContent.m_strBEFORELUNCH_XML);
            m_txttwoLunch.m_mthSetNewText(objContent.m_strTWOAFTERLUNCH_VCHR, objContent.m_strTWOAFTERLUNCH_XML);
            m_txtBeforeDinner.m_mthSetNewText(objContent.m_strBEFOREDINNER_VCHR, objContent.m_strBEFOREDINNER_XML);
            m_txttwoDinner.m_mthSetNewText(objContent.m_strTWOAFTERDINNER_VCHR, objContent.m_strTWOAFTERDINNER_XML);
            m_txtBeforeSleep.m_mthSetNewText(objContent.m_strBEFORESLEEP_VCHR, objContent.m_strBEFORESLEEP_XML);
            m_txtBeizhu.m_mthSetNewText(objContent.m_strBEIZHU_VCHR, objContent.m_strBEIZHU_XML);

            #region ǩ������
            if (objContent.objSignerArr != null)
            {
                m_mthAddSignToListView(lsvSign, objContent.objSignerArr);
            }
            else
            {
                lsvSign.Items.Clear();
                clsEmrEmployeeBase_VO objEMP = null;
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                long lngRes = objDomain.m_lngGetEmpByID(objContent.m_strCreateUserID, out objEMP);
                if (objEMP != null)
                {
                    ListViewItem lviNewItem = new ListViewItem(objEMP.m_strGetTechnicalRankAndName);
                    lviNewItem.SubItems.Add(objEMP.m_strEMPID_CHR);
                    lviNewItem.SubItems.Add(objEMP.m_StrHistroyLevel);
                    lviNewItem.Tag = objEMP;
                    lsvSign.Items.Add(lviNewItem);
                }
                objDomain = null;
            }
            #endregion ǩ��

            this.lsvSign.Enabled = false;
            this.m_cmdSign.Enabled = false;
        }
        #endregion

        #region �����Ƿ����ѡ���˺ͼ�¼ʱ���б�

        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б�

        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {
            if (p_blnEnable == false)
            {

                m_cmdOK.Visible = true;

                this.CenterToParent();
            }

            this.MaximizeBox = false;
        }
        #endregion

        #region �����¼���������

        /// <summary>
        /// �����¼���������,�����Ӵ������Ҫ����ʵ��

        /// </summary>
        /// <param name="p_blnEnable">�Ƿ������޸������¼�ļ�¼��Ϣ</param>
        protected override void m_mthEnableModifySub(bool p_blnEnable)
        {
            m_dtpCreateDate.Enabled = true;
        }
        #endregion

        #region �����Ƿ�����޸�(��ʵ��)
        /// <summary>
        /// �����Ƿ�����޸�(�޸����ۼ�)
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset">�Ƿ����ÿ����޸�(�޸����ۼ�)
        ///���Ϊtrue,���Լ�¼����,�ѽ����������Ϊ������

        ///������ݼ�¼���ݽ�������
        ///</param>
        protected override void m_mthSetModifyControlSub(clsTrackRecordContent p_objRecordContent,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д����

        }
        #endregion

        #region ��ȡ��¼��������Ϣ����

        /// <summary>
        /// ��ȡ��¼��������Ϣ����

        /// </summary>
        /// <returns></returns>
        public override iCare.clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsIntensiveRecordInfo objTrackInfo = new clsIntensiveRecordInfo();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = this.m_lblForTitle.Text;

            //����m_dtmRecordTime
            if (objTrackInfo.m_ObjRecordContent != null)
            {
                m_dtpCreateDate.Value = objTrackInfo.m_ObjRecordContent.m_dtmCreateDate;
            }
            return objTrackInfo;
        }
        #endregion

        #region ��������¼��Ϣ
        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ������

        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            MDIParent.m_mthSetDefaulEmployee(lsvSign);
            m_dtpCreateDate.Value = DateTime.Now;

            //m_txtCheckResult.m_mthClearText();
            //m_cboCheckTime.Text = string.Empty;
            //m_cboCheckTime.SelectedIndex = -1;
            m_txtnullAbdon.m_mthClearText();
            m_txttwoBreakfast.m_mthClearText();
            m_txtBeforeLunch.m_mthClearText();
            m_txttwoLunch.m_mthClearText();
            m_txtBeforeDinner.m_mthClearText();
            m_txttwoDinner.m_mthClearText();
            m_txtBeforeSleep.m_mthClearText();
            m_txtBeizhu.m_mthClearText();
        }
        #endregion

        #region Jump Control
        /// <summary>
        /// ������ת����
        /// </summary>
        /// <param name="p_objJump"></param>
        protected override void m_mthInitJump(clsJumpControl p_objJump)
        {
            p_objJump = new clsJumpControl(this,
                new Control[] { m_txtnullAbdon, m_txttwoBreakfast, m_txtBeforeLunch, m_txttwoLunch, m_txtBeforeDinner, m_txttwoDinner, m_txtBeforeSleep, m_txtBeizhu }, Keys.Enter);
        }
        #endregion

        #region ��ȡ��¼
        /// <summary>
        /// ��ȡ��¼
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public override clsTrackRecordContent m_objGetRecordContent(clsPatient p_objSelectedPatient, string p_strOpenDate)
        {
            clsTrackRecordContent objContent;
            //��ȡ��¼
            m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrRegisterId, p_strOpenDate, out objContent);
            return objContent;
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        #endregion

        private void frmEMR_IntBloodSugarWatchcon_Load(object sender, EventArgs e)
        {
            m_cmdModifyPatientInfo.Visible = false;
        }
    }
}