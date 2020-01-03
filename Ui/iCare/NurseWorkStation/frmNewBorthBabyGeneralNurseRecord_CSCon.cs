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
    public partial class frmNewBorthBabyGeneralNurseRecord_CSCon : iCare.frmDiseaseTrackBase
    {
        private string m_strRegisterid = string.Empty;
        private bool blnNewRecord;
        private string strRecordInPatientID;
        private string strRecordInPatientDate;
        private string strRecordCreateDate;
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        public frmNewBorthBabyGeneralNurseRecord_CSCon()
        {
            InitializeComponent();
        }
        public frmNewBorthBabyGeneralNurseRecord_CSCon(bool blnNew, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
            : this()
        {
            blnNewRecord = blnNew;
            strRecordInPatientID = p_strInPatientID;
            strRecordInPatientDate = p_strInPatientDate;
            strRecordCreateDate = p_strCreateDate;
            m_mthSetRichTextBoxAttribInControl(this);
            m_objSign = new clsEmrSignToolCollection();
            m_objSign.m_mthBindEmployeeSign(m_cmbsign, lsvSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);
        }
        public string m_strSetRegisterId
        {
            set { m_strRegisterid = value; }
        }
        public override int m_IntFormID
        {
            get
            {
                return 104;
            }
        }

        #region ����
        protected override iCare.clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ�����¼�������ʵ�������Ӵ�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.GeneralNurseRecord_CS);
        }
        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            //��ռ�¼����			
            m_txtRecordContent.m_mthClearText();
            m_dtpCreateDate.Value = DateTime.Now;
            lsvSign.Items.Clear();
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);
            m_cmdModifyPatientInfo.Visible = false;
            m_mthSetModifyControl(null, true);
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
                clsGeneralNurseRecordContent_CSDetail objContent = new clsGeneralNurseRecordContent_CSDetail();
                objContent.m_strINPATIENTID = strRecordInPatientID;
                objContent.m_dtmINPATIENTDATE = DateTime.Parse(strRecordInPatientDate);
                objContent.m_dtmOPENDATE = DateTime.Parse(strTimeNow);
                objContent.m_dtmMODIFYDATE = DateTime.Parse(strTimeNow);
                objContent.m_dtmRECORDDATE = DateTime.Parse(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                objContent.m_dtmCREATERECORDDATE = DateTime.Parse(strTimeNow);
                objContent.m_strRECORDCONTENTAll = m_txtRecordContent.Text;
                objContent.m_strRECORDCONTENT_RIGHT = m_txtRecordContent.m_strGetRightText();
                objContent.m_strRECORDCONTENTXML = m_txtRecordContent.m_strGetXmlText();
                objContent.m_intClass = m_intGetClass(m_dtpCreateDate.Value);

                if (objContent.m_strRECORDCONTENT_RIGHT == null || objContent.m_strRECORDCONTENT_RIGHT == string.Empty)
                {
                    MDIParent.ShowInformationMessageBox("����д�����¼����");
                    return 0;
                }
                //��ȡǩ��
                objContent.m_strMODIFYRECORDUSERID = clsEMRLogin.LoginEmployee.m_strEMPID_CHR;
                objContent.m_strCREATERECORDUSERID = clsEMRLogin.LoginEmployee.m_strEMPID_CHR;

                if (lsvSign.Items.Count > 0)
                {
                    objContent.m_strRecordSignNameArr = new string[lsvSign.Items.Count];
                    objContent.m_strRecordSignIDArr = new string[lsvSign.Items.Count];
                    for (int i = 0; i < lsvSign.Items.Count; i++)
                    {
                        objContent.m_strRecordSignNameArr[i] = lsvSign.Items[i].SubItems[0].Text.Split(' ').Length > 1 ? lsvSign.Items[i].SubItems[0].Text.Split(' ')[1] : lsvSign.Items[i].SubItems[0].Text;
                        objContent.m_strRecordSignIDArr[i] = lsvSign.Items[i].SubItems[1].Text;
                    }
                }
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);

                //clsNewBorthBabyGeneralNurseRecord_CSService objserv =
                //    (clsNewBorthBabyGeneralNurseRecord_CSService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNewBorthBabyGeneralNurseRecord_CSService));

                #region ��ǩ��ʱ��֤����ǩ����
                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strINPATIENTID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_strRegisterid;
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

                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsNewBorthBabyGeneralNurseRecord_CSService_m_lngAddNewDetail(objContent);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, false);
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
                clsGeneralNurseRecordContent_CSDetail objContent = new clsGeneralNurseRecordContent_CSDetail();
                objContent.m_strINPATIENTID = strRecordInPatientID;
                objContent.m_dtmINPATIENTDATE = DateTime.Parse(strRecordInPatientDate);
                objContent.m_dtmMODIFYDATE = DateTime.Parse(strNow);
                objContent.m_dtmRECORDDATE = m_dtpCreateDate.Value;
                objContent.m_dtmCREATERECORDDATE = DateTime.Parse(strRecordCreateDate);
                objContent.m_strRECORDCONTENTAll = m_txtRecordContent.Text;
                objContent.m_strRECORDCONTENT_RIGHT = m_txtRecordContent.m_strGetRightText();
                objContent.m_strRECORDCONTENTXML = m_txtRecordContent.m_strGetXmlText();
                objContent.m_dtmOPENDATE = DateTime.Parse(strNow);

                if (objContent.m_strRECORDCONTENT_RIGHT == null || objContent.m_strRECORDCONTENT_RIGHT == string.Empty)
                {
                    MDIParent.ShowInformationMessageBox("����д����۲졢�����ʩ��Ч��������");
                    return 0;
                }

                //��ȡǩ��
                if (lsvSign.Items.Count > 0)
                {
                    objContent.m_strRecordSignNameArr = new string[lsvSign.Items.Count];
                    objContent.m_strRecordSignIDArr = new string[lsvSign.Items.Count];
                    for (int i = 0; i < lsvSign.Items.Count; i++)
                    {
                        objContent.m_strRecordSignNameArr[i] = lsvSign.Items[i].SubItems[0].Text;
                        objContent.m_strRecordSignIDArr[i] = lsvSign.Items[i].SubItems[1].Text;
                    }
                }
                //��ȡǩ��
                strUserIDList = "";
                strUserNameList = "";
                m_mthGetSignArr(new Control[] { lsvSign }, ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
                /*
                objContent.m_strMODIFYRECORDUSERID = ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR;
                objContent.objSignerArr = new clsEmrSigns_VO[1];
                objContent.objSignerArr[0] = new clsEmrSigns_VO();
                objContent.objSignerArr[0].objEmployee = new clsEmrEmployeeBase_VO();
                objContent.objSignerArr[0].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
                objContent.objSignerArr[0].controlName = "txtSign";
                objContent.objSignerArr[0].m_strFORMID_VCHR = "frmGeneralNurseRecord_CSCon";//ע���Сд
                objContent.objSignerArr[0].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
                */
                //clsNewBorthBabyGeneralNurseRecord_CSService objserv =
                //    (clsNewBorthBabyGeneralNurseRecord_CSService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNewBorthBabyGeneralNurseRecord_CSService));

                #region ��ǩ��ʱ��֤����ǩ����
                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strINPATIENTID.Trim() + "-" + objContent.m_dtmINPATIENTDATE.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_strRegisterid;
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                    return -1;

                #endregion

                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsNewBorthBabyGeneralNurseRecord_CSService_m_lngModifyDetail(objContent);
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
                clsGeneralNurseRecordContent_CSDetail objDetail = null;
                //clsNewBorthBabyGeneralNurseRecord_CSService objserv =
                //    (clsNewBorthBabyGeneralNurseRecord_CSService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNewBorthBabyGeneralNurseRecord_CSService));

                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsNewBorthBabyGeneralNurseRecord_CSService_m_lngGetRecordContent(strRecordInPatientID, strRecordInPatientDate, strRecordCreateDate, out objDetail);
                //objserv.Dispose();
                if (objDetail == null)
                    return;
                //��ֵ����
                m_txtRecordContent.m_mthSetNewText(objDetail.m_strRECORDCONTENTAll, objDetail.m_strRECORDCONTENTXML);
                m_dtpCreateDate.Value = objDetail.m_dtmRECORDDATE;
                m_mthAddSignToListView(lsvSign, objDetail.objSignerArr);
                m_mthSetModifyControl(objDetail.m_strCREATERECORDUSERID, false);
                this.m_dtpCreateDate.Enabled = false;
                m_cmdModifyPatientInfo.Visible = false;
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
        private void frmNewBorthBabyGeneralNurseRecord_CSCon_Load(object sender, System.EventArgs e)
        {

            if (blnNewRecord)
            {
                //��ʼ��
                m_mthClearRecordInfo();
                //���϶˿ռ���
                m_txtRecordContent.m_mthInsertText("    ", 0);
            }
            else
                m_GetDataFromDB();
        }

        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset"></param>
        protected void m_mthSetModifyControl(string p_strModifyUserID,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
            if (p_strModifyUserID != null)
            {
                m_mthSetRichTextModifyColor(this, Color.Red);
                m_mthSetRichTextCanModifyLast(this, m_blnGetCanModifyLast(p_strModifyUserID));
            }
        }

        private bool m_blnGetCanModifyLast(string p_strModifyUserID)
        {
            if (p_strModifyUserID != null && p_strModifyUserID.Trim() == clsEMRLogin.LoginEmployee.m_strEMPID_CHR)
                return true;
            else
                return false;
        }

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

            clsGeneralNurseRecordContent_CSDetail objContent;

            //clsNewBorthBabyGeneralNurseRecord_CSService objITRServ =
            //    (clsNewBorthBabyGeneralNurseRecord_CSService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNewBorthBabyGeneralNurseRecord_CSService));

            //��ȡ��¼
            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsNewBorthBabyGeneralNurseRecord_CSService_m_lngGetDelRecordContentWithInpatient(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate, out objContent);
            //objITRServ.Dispose();
            if (lngRes <= 0 || objContent == null)
                return;


            //���õ�ǰ��¼����¼ʱ�� 
            m_objCurrentPatient = p_objSelectedPatient;
            txtInPatientID.Text = this.m_objCurrentPatient.m_StrHISInPatientID;

            m_mthSetDeletedGUIFromContent(objContent);

        }

        protected override void m_mthSetDeletedGUIFromContent(weCare.Core.Entity.clsTrackRecordContent p_objContent)
        {
            clsGeneralNurseRecordContent_CSDetail objContent = (clsGeneralNurseRecordContent_CSDetail)p_objContent;
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��			

            this.m_mthClearRecordInfo();
            //��ֵ����
            m_txtRecordContent.m_mthSetNewText(objContent.m_strRECORDCONTENTAll, objContent.m_strRECORDCONTENTXML);
            m_dtpCreateDate.Value = objContent.m_dtmCREATERECORDDATE;
            //m_mthAddSignToTextBoxByEmpNo(new TextBoxBase[] { txtSign, }, new string[] {objContent.m_strCREATERECORDUSERID }, new bool[] { false });
            if (objContent.m_strRecordSignIDArr != null)
            {
                for (int i = 0; i < objContent.m_strRecordSignIDArr.Length; i++)
                {
                    ListViewItem lviNewItem = new ListViewItem(new string[] { objContent.m_strRecordSignNameArr[i], objContent.m_strRecordSignIDArr[i] });
                    lsvSign.Items.Add(lviNewItem);
                }
            }
            this.m_dtpCreateDate.Enabled = false;
            m_cmdModifyPatientInfo.Visible = false;
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
    }
}

