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
    /// ���̼�¼�Ӵ���
    /// </summary>
    public partial class frmPartogramRecordContent_GX : frmDiseaseTrackBase
    {
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        private bool m_blnIsNew = true;
        private int m_intSelectHour = -1;
        private string m_strRegisterId = string.Empty;
        private DateTime m_dtmCreatedDate = DateTime.MinValue;
        private DateTime m_dtmCheckDate = DateTime.MinValue;
        private List<clsPartogram_Point> m_arlDeletePoint;
        clsPartogramDomain m_objDomain;
        clsPartogram_VO m_obj_VO;

        public frmPartogramRecordContent_GX(bool p_blnIsNew,string p_strRegisterId,string p_strGiveBirthDate)
        { 
            InitializeComponent();
            m_objDomain = new clsPartogramDomain();
            m_arlDeletePoint = new List<clsPartogram_Point>(2);
            //ָ����ʿ����վ��
            intFormType = 2;
            m_mthSetRichTextBoxAttribInControl(this);
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //����ָ��Ա��ID��
            m_objSign.m_mthBindEmployeeSign(m_cmdSign, lsvSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);

            //�½�����϶�Ϊ�޺ۼ�״̬
            chkModifyWithoutMatk.Checked = true;
            lblGiveBirthDate.Text = p_strGiveBirthDate;
            m_blnIsNew = p_blnIsNew;
            m_strRegisterId = p_strRegisterId;

        }
        //public frmPartogramRecordContent_GX(bool p_blnIsNew, int p_intCurrentHour, int[] p_intHours, string p_strRegisterId, DateTime p_dtmCreatedDate, string p_strGiveBirthDate)
        //    : this(p_blnIsNew,p_strRegisterId,p_strGiveBirthDate)
        //{
        //    m_intSelectHour = p_intCurrentHour;
        //    for (int i = 0 ; i < p_intHours.Length ; i++)
        //        m_cboHours.Items.Add(p_intHours[i]);
        //    m_cboHours.Text = p_intCurrentHour.ToString();
        //    if (!m_blnIsNew)
        //    {
        //        m_dtmCreatedDate = p_dtmCreatedDate;
        //        m_cboHours.Enabled = false;
        //    }
        //    else
        //        clsPublicFunction.m_mthSetDefaulEmployee(lsvSign);
        //}
        public frmPartogramRecordContent_GX(bool p_blnIsNew, DateTime p_dtmCheckDate,  string p_strRegisterId, DateTime p_dtmCreatedDate, string p_strGiveBirthDate)
            : this(p_blnIsNew,p_strRegisterId,p_strGiveBirthDate)
        {
            m_dtmCheckDate = p_dtmCheckDate;
            if (!m_blnIsNew)
            {
                m_dtmCreatedDate = p_dtmCreatedDate;
                m_cboHours.Enabled = false;
            }
            else
                clsPublicFunction.m_mthSetDefaulEmployee(lsvSign);
        }
        public clsPartogram_VO m_ObjGetValue
        {
            get { return m_obj_VO; }
        }
        public bool m_BlnIsNew
        {
            get { return m_blnIsNew; }
        }
        public override int m_IntFormID
        {
            get
            {
                return 0;
            }
        }
        #region method
        // <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        private void m_mthClearRecordInfo()
        {
            m_arlDeletePoint.Clear();
            m_obj_VO = null;
            //��ռ�¼����			
            m_txtProsses.m_mthClearText();
            m_numDown.Value = 0;
            m_numUtricm.Value = 0;
            m_txtFe.m_mthClearValue();
            m_txtMin1.m_mthClearValue();
            m_txtMin2.m_mthClearValue();
            m_txtDiastolicPressure.m_mthClearValue();
            m_txtUterineContraction.m_mthClearValue();
            m_txtSystolicPressure.m_mthClearValue();
            m_txtUterineContractionMin.m_mthClearValue();
            m_lsvU.Items.Clear();
            m_lsvDown.Items.Clear();

            m_blnIsNew = true;
            m_intSelectHour = -1;
            m_cboHours.Items.Clear();
            m_strRegisterId = string.Empty;
            m_dtmCreatedDate = DateTime.MinValue;

            m_dtmCheckDate = DateTime.Now;

            m_mthSetModifyControl(null, true);
            //Ĭ��ǩ��
            clsPublicFunction.m_mthSetDefaulEmployee(lsvSign);
        }
        /// <summary>
        /// ��ȡ��¼
        /// </summary>
        private void m_mthGetRecord()
        {
            if (string.IsNullOrEmpty(m_strRegisterId) || m_dtmCreatedDate == DateTime.MinValue)
                return;
            clsPartogram_VO obj_VO = null;
            long lngRes = m_objDomain.m_lngGetOneHourValues(m_strRegisterId, m_dtmCheckDate.Hour, out obj_VO);
            if (lngRes <= 0 || obj_VO == null)
                return;
            m_mthSetValuesToGui(obj_VO);
            m_mthSetModifyControl(null, false);
        }
        /// <summary>
        /// ��Ӽ�¼������
        /// </summary>
        /// <param name="p_obj_VO"></param>
        private void m_mthSetValuesToGui(clsPartogram_VO p_obj_VO)
        {
            m_strRegisterId = p_obj_VO.m_strREGISTERID_CHR;
            m_dtpCheckDate.Value = p_obj_VO.m_dtmCHECKDATE_DAT;
            m_dtmCreatedDate = p_obj_VO.m_dtmCREATEDATE_DAT;
            m_dtmCheckDate = p_obj_VO.m_dtmCHECKDATE_DAT;
            m_txtProsses.m_mthSetNewText(p_obj_VO.m_strPROCESS_VCHR, p_obj_VO.m_strPROCESS_XML_VCHR);
            m_txtDiastolicPressure.m_mthSetValue(p_obj_VO.m_intDIASTOLICPRESSURE_INT.ToString());
            m_txtSystolicPressure.m_mthSetValue(p_obj_VO.m_intSYSTOLICPRESSURE_INT.ToString());
            m_txtUterineContraction.m_mthSetValue(p_obj_VO.m_intUTERINECONTRACTION_INT.ToString());
            m_txtUterineContractionMin.m_mthSetValue(p_obj_VO.m_intUTERINECONTRACTIONMIN_INT.ToString());
            m_txtFe.m_mthSetValue(p_obj_VO.m_intFETALRHYTHM_INT.ToString());
            m_mthAddSignToListView(lsvSign, p_obj_VO.objSignerArr);
            m_intSelectHour = p_obj_VO.m_dtmCHECKDATE_DAT.Hour;
            if (p_obj_VO.m_ObjPointArr != null)
            {
                ListViewItem item = null;
                for (int i = 0; i < p_obj_VO.m_ObjPointArr.Length; i++)
                {
                    item = new ListViewItem(p_obj_VO.m_ObjPointArr[i].m_dtmCheckDate.ToString("HH:mm"));
                    item.SubItems.Add(p_obj_VO.m_ObjPointArr[i].m_fltPointValue_INT.ToString());
                    item.SubItems[1].Tag = p_obj_VO.m_ObjPointArr[i].m_dtmCheckDate;
                    item.SubItems.Add(p_obj_VO.m_ObjPointArr[i].m_intChildbearingPoint.ToString());
                    if (p_obj_VO.m_ObjPointArr[i].m_intChildbearingPoint == 1)
                        item.ForeColor = Color.Green;
                    item.Tag = p_obj_VO.m_ObjPointArr[i];
                    if (p_obj_VO.m_ObjPointArr[i].m_intPointType_INT == 0)
                    {
                        m_lsvU.Items.Add(item);
                    }
                    else
                    {
                        m_lsvDown.Items.Add(item);
                    }
                }
            }
            m_obj_VO = p_obj_VO;
            m_blnIsNew = false;
            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        private long m_lngSaveNew()
        {
            clsPartogram_VO obj_VO = m_objGetValueFromGui();

            if (obj_VO.m_ObjPointArr != null)
            {
                for (int i = 0; i < obj_VO.m_ObjPointArr.Length; i++)
                {
                    int intMinute = obj_VO.m_ObjPointArr[i].m_dtmCheckDate.Minute;
                    obj_VO.m_ObjPointArr[i].m_dtmCheckDate = obj_VO.m_ObjPointArr[i].m_dtmCheckDate.Date.AddHours(obj_VO.m_intPARTOGRAM_INT).AddMinutes(intMinute);
                }
            }
            string strRecordID = m_objBaseCurrentPatient.m_StrInPatientID.Trim() + "-" +  obj_VO.m_dtmCREATEDATE_DAT;
            if (m_lngCheckSign(obj_VO, obj_VO.m_intMarkStatus == 0, obj_VO.objSignerArr, strRecordID) == -1) return -1;
            //����ǩ�� ���ݿ��� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            //string strRecordID = m_objBaseCurrentPatient.m_StrInPatientID.Trim() + "-" + m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            //clsCheckSignersController objCheck = new clsCheckSignersController();
            //if (objCheck.m_lngSign(obj_VO, this.Name, strRecordID, clsEMRLogin.LoginInfo.m_strEmpID) == -1)
            //    return -1;

            long lngRes = m_objDomain.m_lngAddNewSub(obj_VO);
            if (lngRes > 0)
            {
                m_blnIsNew = false;
                m_obj_VO = obj_VO;
            }
            return lngRes;
        }
        /// <summary>
        /// �޸�
        /// </summary>
        /// <returns></returns>
        private long m_lngModifyOld()
        {
            clsPartogram_VO obj_VO = m_objGetValueFromGui();
            if (m_dtpCheckDate.Value.Hour != m_dtmCheckDate.Hour)
            {
                DialogResult dr = MessageBox.Show(this, "    �Ƿ񽫵����'" + m_dtmCheckDate.Hour + "'Сʱ�����ݱ��浽��" + m_dtpCheckDate.Value.Hour + "'Сʱ?\n\r��������ѡ'��',��ԭʱ�䱣��ѡ'��',ȡ�����±༭ѡ'ȡ��'", "��ܰ��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return -10;
                }
                else if (dr == DialogResult.No)
                {
                    obj_VO.m_dtmCHECKDATE_DAT = m_dtmCheckDate;
                    obj_VO.m_intPARTOGRAM_INT = m_dtmCheckDate.Hour;
                }
                if (obj_VO.m_ObjPointArr != null)
                {
                    for (int i = 0; i < obj_VO.m_ObjPointArr.Length; i++)
                    { 
                        int intMinute = obj_VO.m_ObjPointArr[i].m_dtmCheckDate.Minute;
                        obj_VO.m_ObjPointArr[i].m_dtmCheckDate = obj_VO.m_ObjPointArr[i].m_dtmCheckDate.Date.AddHours(obj_VO.m_intPARTOGRAM_INT).AddMinutes(intMinute);
                        if (obj_VO.m_ObjPointArr[i].m_intSTATUS_INT == 1)
                            obj_VO.m_ObjPointArr[i].m_intSTATUS_INT = 2;
                    }
                }
            }
            obj_VO.m_strCREATEUSERID_CHR = m_obj_VO.m_strCREATEUSERID_CHR;
            obj_VO.m_dtmCREATEDATE_DAT = m_obj_VO.m_dtmCREATEDATE_DAT;
            string strRecordID = m_objBaseCurrentPatient.m_StrInPatientID.Trim() + "-" +  obj_VO.m_dtmCREATEDATE_DAT;
            if (m_lngCheckSign(obj_VO, obj_VO.m_intMarkStatus == 0, obj_VO.objSignerArr, strRecordID) == -1) return -1;
            //����ǩ�� ���ݿ��� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            //string strRecordID = m_objBaseCurrentPatient.m_StrInPatientID.Trim() + "-" + m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            //clsCheckSignersController objCheck = new clsCheckSignersController();
            //if (objCheck.m_lngSign(obj_VO, this.Name, strRecordID, clsEMRLogin.LoginInfo.m_strEmpID) == -1)
            //    return -1;

            long lngRes = m_objDomain.m_lngModifySub(obj_VO, m_dtmCheckDate.Hour);
            m_obj_VO = obj_VO;
            return lngRes;
        }
        /// <summary>
        /// �ӽ����ȡ��¼
        /// </summary>
        /// <returns></returns>
        private clsPartogram_VO m_objGetValueFromGui()
        {
            clsPartogram_VO obj_VO = new clsPartogram_VO();
            strUserIDList = "";
            strUserNameList = "";
            m_mthGetSignArr(new Control[] { lsvSign }, ref obj_VO.objSignerArr, ref strUserIDList, ref strUserNameList);
            //����Richtextbox��modifyuserID ��modifyuserName
            m_mthSetRichTextBoxAttribInControlWithIDandName(this);
            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                obj_VO.m_intMarkStatus = 0;
            else
                obj_VO.m_intMarkStatus = 1;
            #endregion
            obj_VO.m_strMODIFYUSERID_CHR = strUserIDList;
            obj_VO.m_strMODIFYUSERNAME_VCHR = strUserNameList;
            obj_VO.m_strCREATEUSERID_CHR = clsEMRLogin.LoginInfo.m_strEmpID;
            obj_VO.m_dtmCHECKDATE_DAT = DateTime.Parse(m_dtpCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            obj_VO.m_dtmCREATEDATE_DAT = new clsPublicDomain().m_dtmGetServerTime();
            obj_VO.m_dtmMODIFYDATE_DAT = obj_VO.m_dtmCREATEDATE_DAT;
            obj_VO.m_intDIASTOLICPRESSURE_INT = (int)m_txtDiastolicPressure.m_dcmGetValue();
            obj_VO.m_intFETALRHYTHM_INT = (int)m_txtFe.m_dcmGetValue();
            obj_VO.m_intPARTOGRAM_INT = m_dtpCheckDate.Value.Hour;
            obj_VO.m_intSTATUS_INT = 1;
            obj_VO.m_intSYSTOLICPRESSURE_INT = (int)m_txtSystolicPressure.m_dcmGetValue();
            obj_VO.m_intUTERINECONTRACTION_INT = (int)m_txtUterineContraction.m_dcmGetValue();
            obj_VO.m_intUTERINECONTRACTIONMIN_INT = (int)m_txtUterineContractionMin.m_dcmGetValue();
            obj_VO.m_strPROCESS_R_VCHR = m_txtProsses.m_strGetRightText();
            obj_VO.m_strPROCESS_VCHR = m_txtProsses.Text;
            obj_VO.m_strPROCESS_XML_VCHR = m_txtProsses.m_strGetXmlText();
            obj_VO.m_strREGISTERID_CHR = m_strRegisterId;
            List<clsPartogram_Point> objPointArr = new List<clsPartogram_Point>();
            if (m_lsvU.Items.Count > 0)
            {
                for (int i = 0 ; i < m_lsvU.Items.Count ; i++)
                {
                    if (m_lsvU.Items[i].Tag is clsPartogram_Point)
                    {
                        clsPartogram_Point objPoint = (clsPartogram_Point)m_lsvU.Items[i].Tag;
                        objPoint.m_dtmMODIFYDATE_DAT = obj_VO.m_dtmCREATEDATE_DAT;
                        objPointArr.Add(objPoint);
                    }
                    else
                    {
                        float flt = -1;
                        if (!float.TryParse(m_lsvU.Items[i].SubItems[1].Text, out flt))
                            flt = -1;
                        int intTmp = -1;
                        int.TryParse(m_lsvU.Items[i].SubItems[2].Text, out intTmp);
                        clsPartogram_Point objPoint2 = m_objGetPoint(flt, ((DateTime)m_lsvU.Items[i].SubItems[1].Tag).Minute, 0);
                        objPoint2.m_intChildbearingPoint = intTmp;
                        objPointArr.Add(objPoint2);
                    }
                }
            }
            if (m_lsvDown.Items.Count > 0)
            {
                for (int i = 0 ; i < m_lsvDown.Items.Count ; i++)
                {
                    if (m_lsvDown.Items[i].Tag is clsPartogram_Point)
                    {
                        clsPartogram_Point objPoint = (clsPartogram_Point)m_lsvDown.Items[i].Tag;
                        objPoint.m_dtmMODIFYDATE_DAT = obj_VO.m_dtmCREATEDATE_DAT;
                        objPointArr.Add(objPoint);
                    }
                    else
                    {
                        float flt = -10;
                        if (!float.TryParse(m_lsvDown.Items[i].SubItems[1].Text, out flt))
                            flt = -10;
                        //int intTmp = -1;
                        //int.TryParse(m_lsvDown.Items[i].Text, out intTmp);
                        objPointArr.Add(m_objGetPoint(flt, ((DateTime)m_lsvDown.Items[i].SubItems[1].Tag).Minute, 1));
                    }
                }
            }
            if (m_arlDeletePoint.Count > 0)
            {
                objPointArr.AddRange(m_arlDeletePoint.ToArray());
            }
            obj_VO.m_ObjPointArr = objPointArr.ToArray();
            return obj_VO;
        }
        private clsPartogram_Point m_objGetPoint(float p_fltValue, int p_intMin, int p_intType)
        {
            clsPartogram_Point objPoint = new clsPartogram_Point();
            objPoint.m_dtmCheckDate = DateTime.Parse(m_dtpCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objPoint.m_dtmCREATEDATE_DAT = new clsPublicDomain().m_dtmGetServerTime();
            objPoint.m_dtmMODIFYDATE_DAT = objPoint.m_dtmCREATEDATE_DAT;
            objPoint.m_fltPointValue_INT = p_fltValue;
            objPoint.m_intPARTOGRAM_INT = objPoint.m_dtmCheckDate.Hour;
            objPoint.m_intPointID_INT = -1;
            objPoint.m_intPointMin_INT = p_intMin;
            objPoint.m_intPointType_INT = p_intType;
            objPoint.m_intSTATUS_INT = 1;
            objPoint.m_strMODIFYUSERID_CHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objPoint.m_strMODIFYUSERNAME_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objPoint.m_strREGISTERID_CHR = m_strRegisterId;
            return objPoint;
        }
        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent">������ʵ��</param>
        /// <param name="p_blnReset"></param>
        protected override void m_mthSetModifyControl(clsTrackRecordContent p_objRecordContent,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
            if (p_blnReset == true)
            {
                //����������жϣ���ʹ���ֵ��Ӳ�������������ɫ��ɰ�ɫ
                //if(MDIParent.s_bolIAnaSystem)
                //    m_mthSetRichTextModifyColor(this,SystemColors.Info);
                //else
                m_mthSetRichTextModifyColor(this, com.digitalwave.Utility.Controls.clsHRPColor.s_ClrInputFore);

                m_mthSetRichTextCanModifyLast(this, true);
            }
            else if (m_obj_VO != null)
            {
                bool blnTempCanMoify = m_blnGetCanModifyLast(m_obj_VO.m_strMODIFYUSERID_CHR, m_obj_VO.m_dtmCREATEDATE_DAT, m_obj_VO.m_intMarkStatus);
                if (blnTempCanMoify && blnIsModifyWithoutMark)//��������޺ۼ��޸��򲻺���
                    m_mthSetRichTextModifyColor(this, Color.Black);
                else//����������޺ۼ��޸������
                    m_mthSetRichTextModifyColor(this, Color.Red);


                m_mthSetRichTextCanModifyLast(this, blnTempCanMoify);
            }
            //�̳д���ʵ�ָ÷�����ͨ��do nothing
            m_mthSetModifyControlSub(p_objRecordContent, p_blnReset);
        }
        /// <summary>
        /// ���Ƿ�����޺ۼ��޸ġ�Ĭ��Ϊtrue
        /// �˱���ֻ����checkbox��������״̬
        /// </summary>
        bool m_blnIsModifyWithoutMark = true;

        protected override bool blnIsModifyWithoutMark
        {
            get { return m_blnIsModifyWithoutMark; }
        }
        /// <summary>
        /// �ۼ�����
        /// �����ڿ��޺ۼ��޸�ʱЧ�ڡ�ͨ������chkModifyWithoutMatk��
        /// �ﵽ�����Ƿ��С��޺ۼ��޸�
        /// </summary>
        /// <returns></returns>
        protected override bool m_mthModifyWithoutMark()
        {

            bool blnRes = chkModifyWithoutMatk.Checked;
            if (chkModifyWithoutMatk.Checked == false)
            {
                //��ʾǩ��
                if (MessageBox.Show("���Ҫ�����кۼ��޸ģ���ǰ�����޸ĵ����ݽ���ʧ��Ҫ������", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    m_blnIsModifyWithoutMark = false;
                    m_mthClearRecordInfo();
                    m_mthGetRecord();
                }
                else
                    blnRes = !blnRes;
            }
            else
            {
                //��ʾǩ��
                if (MessageBox.Show("���Ҫ�����޺ۼ��޸ģ���ǰ�����޸ĵ����ݽ���ʧ������Ҫ��һ��֤ÿ��ǩ���ߣ�Ҫ������", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    m_blnIsModifyWithoutMark = true;
                    m_mthClearRecordInfo();
                    m_mthGetRecord();
                }
                else
                    blnRes = !blnRes;

            }
            return blnRes;

        }
        private bool m_blnIsExistPoint(ListView p_lsvPoint, string p_strValue)
        {
            if (p_lsvPoint.Items.Count > 0)
            {
                for (int i = 0; i < p_lsvPoint.Items.Count; i++)
                {
                    if (p_lsvPoint.Items[i].Text == m_dtpCheckDate.Value.ToString("HH:mm") || p_lsvPoint.Items[i].SubItems[1].Text == p_strValue)
                    {
                        if (MessageBox.Show(this, "�Ѿ�������ͬ�ļ��ʱ����߼��ֵ����Ŀ���Ƿ������ӣ�", "��ܰ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            return false;
                        }
                        else return true;
                    }
                }
            }
            return false;
        }
        #endregion Method

        #region Event
        private void m_mniDelete_Click(object sender, EventArgs e)
        {
            m_mthDeleteItems(0);
        }

        private void m_mniDelete2_Click(object sender, EventArgs e)
        {
            m_mthDeleteItems(1);
        }
        private void m_mthDeleteItems(int p_intType)
        {
            IEnumerator objEnumerator = null;
            if (p_intType == 0 && m_lsvU.SelectedItems.Count > 0)
            {
                objEnumerator = m_lsvU.SelectedItems.GetEnumerator();
            }
            else if (p_intType == 1 && m_lsvDown.SelectedItems.Count > 0)
                objEnumerator = m_lsvDown.SelectedItems.GetEnumerator();
            string strEmpArr = string.Empty;
            if (objEnumerator != null)
            {
                while (objEnumerator.MoveNext())
                {
                    ListViewItem item = (ListViewItem)objEnumerator.Current;
                    clsPartogram_Point objPoint = item.Tag as clsPartogram_Point;
                    if (objPoint != null)
                    {
                        //Ȩ���ж�
                        string strDeptIDTemp = m_ObjCurrentArea.m_strDEPTID_CHR;
                        bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, objPoint.m_strMODIFYUSERID_CHR, clsEMRLogin.LoginEmployee, intFormType);
                        if (!blnIsAllow)
                        {
                            strEmpArr += objPoint.m_strMODIFYUSERID_CHR + " ";
                            continue;
                        }
                        else if (!m_arlDeletePoint.Contains(objPoint))
                        {
                            objPoint.m_dtmDEACTIVEDDATE_DAT = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            objPoint.m_strDEACTIVEDOPERATORID_CHR = clsEMRLogin.LoginInfo.m_strEmpID;
                            objPoint.m_strDEACTIVEDOPERATORNAME_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                            objPoint.m_intSTATUS_INT = 0;
                            m_arlDeletePoint.Add(objPoint);
                        }
                    }
                    item.Remove();
                }
            }
            if (strEmpArr != string.Empty)
            {
                Control objCtl = groupBox2;
                if (p_intType == 1)
                    objCtl = groupBox3;
                m_tipMain.Show("��һ�����������߷ֱ�Ϊ��" + strEmpArr + "������Ŀ����ɾ����", objCtl,  2000);
            }
        }
        private void m_cmdAddU_Click(object sender, EventArgs e)
        {
            if (m_blnIsExistPoint(m_lsvU, m_numUtricm.Value.ToString()))
            {
                return;
            }
            int intChildbearingPoint = (m_chkChildbearingPoint.Checked ? 1:0);
            ListViewItem item = new ListViewItem(new string[] { m_dtpCheckDate.Value.ToString("HH:mm"), m_numUtricm.Value.ToString(), intChildbearingPoint.ToString() });
            if (m_cmdAddU.Tag is clsPartogram_Point)
            {
                clsPartogram_Point objPoint = (clsPartogram_Point)m_cmdAddU.Tag;
                if (objPoint.m_intPointType_INT == 0)
                {
                    //objPoint.m_intPointMin_INT = (int)m_txtMin1.m_dcmGetValue();
                    objPoint.m_intPARTOGRAM_INT = m_dtpCheckDate.Value.Hour;
                    objPoint.m_intPointMin_INT = m_dtpCheckDate.Value.Minute;
                    objPoint.m_fltPointValue_INT = (float)m_numUtricm.Value;
                    objPoint.m_dtmMODIFYDATE_DAT = new clsPublicDomain().m_dtmGetServerTime();
                    objPoint.m_intSTATUS_INT = 2;
                    objPoint.m_strMODIFYUSERID_CHR = clsEMRLogin.LoginInfo.m_strEmpID;
                    objPoint.m_intChildbearingPoint = intChildbearingPoint;
                    item.Tag = objPoint;
                }
            }
            if (intChildbearingPoint == 1)
                item.ForeColor = Color.Green;
            item.SubItems[1].Tag = m_dtpCheckDate.Value;
            m_lsvU.Items.Add(item);
            //m_txtMin1.m_mthClearValue();
            m_chkChildbearingPoint.Checked = false;
            m_numUtricm.Value = 0;
            m_cmdAddU.Tag = null;
        }
        private void m_cmdAddDown_Click(object sender, EventArgs e)
        {
            if (m_blnIsExistPoint(m_lsvDown, m_numDown.Value.ToString()))
            {
                return;
            }
            ListViewItem item = new ListViewItem(new string[] { m_dtpCheckDate.Value.ToString("HH:mm"), m_numDown.Value.ToString() });
            if (m_cmdAddDown.Tag is clsPartogram_Point)
            {
                clsPartogram_Point objPoint = (clsPartogram_Point)m_cmdAddDown.Tag;
                if (objPoint.m_intPointType_INT == 1)
                {
                    //objPoint.m_intPointMin_INT = (int)m_txtMin2.m_dcmGetValue();
                    objPoint.m_intPARTOGRAM_INT = m_dtpCheckDate.Value.Hour;
                    objPoint.m_intPointMin_INT = m_dtpCheckDate.Value.Minute;
                    objPoint.m_fltPointValue_INT = (float)m_numDown.Value;
                    objPoint.m_dtmMODIFYDATE_DAT = new clsPublicDomain().m_dtmGetServerTime();
                    objPoint.m_intSTATUS_INT = 2;
                    objPoint.m_strMODIFYUSERID_CHR = clsEMRLogin.LoginInfo.m_strEmpID;
                    item.Tag = objPoint;
                }
            }
            item.SubItems[1].Tag = m_dtpCheckDate.Value;
            m_lsvDown.Items.Add(item);
            //m_txtMin2.m_mthClearValue();
            m_numDown.Value = 0;
            m_cmdAddDown.Tag = null;
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cmdOK_Click(object sender, EventArgs e)
        {
            long lngRes = 0;
            if (m_blnIsNew)
                lngRes = m_lngSaveNew();
            else
                lngRes = m_lngModifyOld();
            if (lngRes > 0)
            {
                MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (lngRes == -10)
            { 
                m_tipMain.Show("�Ѿ�ȡ�����棡", m_cmdOK, 2000);
            }
            else
            {
                //m_obj_VO = null;
                m_tipMain.Show("����ʧ�ܣ�", m_cmdOK, 2000);
            }
        }

        private void m_lsvU_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvU.SelectedItems.Count > 0)
            {
                ListViewItem objItem = m_lsvU.SelectedItems[0];
                clsPartogram_Point objPoint = objItem.Tag as clsPartogram_Point;
                if (objPoint != null)
                {
                    m_dtpCheckDate.Value = objPoint.m_dtmCheckDate;
                    //m_txtMin1.m_mthSetValue(objPoint.m_intPointMin_INT.ToString());
                    m_chkChildbearingPoint.Checked = (objPoint.m_intChildbearingPoint == 1);
                    m_numUtricm.Value = (decimal)objPoint.m_fltPointValue_INT;
                    m_cmdAddU.Tag = objPoint.m_objClone();
                }
                else
                {
                    m_dtpCheckDate.Value = (DateTime)objItem.SubItems[1].Tag;
                    //m_txtMin1.m_mthSetValue(objItem.Text);
                    m_numUtricm.Value = decimal.Parse(objItem.SubItems[1].Text);
                    m_cmdAddU.Tag = null;
                }
                m_lsvU.SelectedItems[0].Remove();
            }
            
        }

        private void m_lsvDown_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvDown.SelectedItems.Count > 0)
            {
                ListViewItem objItem = m_lsvU.SelectedItems[0];
                clsPartogram_Point objPoint = objItem.Tag as clsPartogram_Point;
                if (objPoint != null)
                {
                    m_dtpCheckDate.Value = objPoint.m_dtmCheckDate;
                    //m_txtMin2.m_mthSetValue(objPoint.m_intPointMin_INT.ToString());
                    m_numDown.Value = (decimal)objPoint.m_fltPointValue_INT;
                    m_cmdAddDown.Tag = objPoint.m_objClone();
                }
                else
                {
                    m_dtpCheckDate.Value = (DateTime)objItem.SubItems[1].Tag;
                    //m_txtMin2.m_mthSetValue(objItem.Text);
                    m_numDown.Value = decimal.Parse(objItem.SubItems[1].Text);
                    m_cmdAddDown.Tag = null;
                }
                m_lsvDown.SelectedItems[0].Remove();
            }
        }

        private void m_cboHours_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intTemp = -1;
            if (int.TryParse(m_cboHours.SelectedItem.ToString(), out intTemp))
                m_intSelectHour = intTemp;
        }
        private void m_dtpCheckDate_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_strRegisterId)) return;
            if (m_dtpCheckDate.Value.ToString("yyyy-MM-dd HH") != m_dtmCheckDate.ToString("yyyy-MM-dd HH") || m_dtmCheckDate == DateTime.MinValue)
            {
                clsPartogram_VO objContent = null;
                long lngRes = m_objDomain.m_lngGetOneHourValues(m_strRegisterId, m_dtpCheckDate.Value.Hour, out objContent);
                if (lngRes > 0 && objContent != null)
                {
                    if (objContent.m_dtmCHECKDATE_DAT.Date == m_dtpCheckDate.Value.Date)
                    {
                        if (MessageBox.Show(this, "��ǰ��ѡ���ʱ���" + m_dtpCheckDate.Value.Hour + "Сʱ�ļ�¼�Ѿ����ڣ��Ƿ�ˢ����ʾ" + m_dtpCheckDate.Value.Hour + "Сʱ��¼��\n\rˢ��ѡ'��',����ѡ����ʱ��ѡ'��'.", "��ܰ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            m_mthClearRecordInfo();
                            m_mthSetValuesToGui(objContent);
                            m_mthSetModifyControl(null, false);
                        }
                        else m_dtpCheckDate.Value = m_dtmCheckDate;
                    }
                }
                //else 
                //{
                    DateTime dtm = new clsPublicDomain().m_dtmGetServerTime();
                    for (int i = 0; i < m_lsvDown.Items.Count; i++)
                    {
                        if (m_lsvDown.Items[i].Tag is clsPartogram_Point)
                        {
                            clsPartogram_Point objPoint = (clsPartogram_Point)m_lsvDown.Items[i].Tag;
                            objPoint.m_dtmCheckDate = DateTime.Parse(m_dtpCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                            objPoint.m_dtmMODIFYDATE_DAT = dtm;
                            objPoint.m_intPARTOGRAM_INT = m_dtpCheckDate.Value.Hour;
                            objPoint.m_intSTATUS_INT = 2;
                            m_lsvDown.Items[i].Tag = objPoint;
                        }
                        m_lsvDown.Items[i].Text = m_dtpCheckDate.Value.ToString("HH:mm");
                    }
                    for (int i = 0; i < m_lsvU.Items.Count; i++)
                    {
                        if (m_lsvU.Items[i].Tag is clsPartogram_Point)
                        {
                            clsPartogram_Point objPoint = (clsPartogram_Point)m_lsvU.Items[i].Tag;
                            objPoint.m_dtmCheckDate = DateTime.Parse(m_dtpCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                            objPoint.m_dtmMODIFYDATE_DAT = dtm;
                            objPoint.m_intPARTOGRAM_INT = m_dtpCheckDate.Value.Hour;
                            objPoint.m_intSTATUS_INT = 2;
                            m_lsvU.Items[i].Tag = objPoint;
                        }
                        m_lsvU.Items[i].Text = m_dtpCheckDate.Value.ToString("HH:mm");
                    }
                //}
            }
        }


        #endregion Event

        private void frmPartogramRecordContent_GX_Load(object sender, EventArgs e)
        {
            if (!m_blnIsNew)
                m_mthGetRecord();
        }

    }
}