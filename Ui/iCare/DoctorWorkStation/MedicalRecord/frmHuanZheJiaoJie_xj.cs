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
    ///  �����߽��Ӽ�¼��---�½�
    /// </summary>
    public partial class frmHuanZheJiaoJie_xj : iCare.frmDiseaseTrackBase
    {

        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();

        public frmHuanZheJiaoJie_xj()
        {
            InitializeComponent();

            m_mthSetRichTextBoxAttribInControl(this);
            //ָ��ҽ������վ��
            intFormType = 1;
            this.Text = "��Ѫ����ͬ����";
            this.m_lblForTitle.Text = this.Text;

            m_objSign = new clsEmrSignToolCollection();

            //����ҽʦ
            m_objSign.m_mthBindEmployeeSign(m_cmdRecord, m_txtRecorder, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            //����ǩ��
            m_objSign.m_mthBindEmployeeSign(m_cmdjiaojie, m_txtjiaojie, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            //������ʿ
            m_objSign.m_mthBindEmployeeSign(m_cmdhushi, m_txthushi, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
           
        }

        /// <summary>
        /// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public override clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            clsShuXueZhiLiaoyesInfo_xj objTrackInfo = new clsShuXueZhiLiaoyesInfo_xj();

            objTrackInfo.m_ObjRecordContent = m_objCurrentRecordContent;
            //����m_strTitle��m_dtmRecordTime
            objTrackInfo.m_DtmRecordTime = m_dtpCreateDate.Value;
            objTrackInfo.m_StrTitle = "�����߽��Ӽ�¼��";

            return objTrackInfo;
        }








    }
}