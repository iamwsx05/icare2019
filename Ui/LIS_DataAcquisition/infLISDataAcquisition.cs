using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Windows.Forms;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// ����������ݲɼ��ӿ�
    /// </summary>
    public interface infLISDataAcquisition
    {
        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        clsLIS_Equip_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// ��������
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ��¼��־��true = ��
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="p_frmParent">��������</param>
        /// <param name="p_objDeviceConfigVO">�豸������Ϣ</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// ��ʾ�������
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// ��ʾ�ӿ���Ϣ
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
    }
        /// <summary>
        /// �������ݲɼ��ӿ� ---�ı�ģʽ ��Ӿ��2012-01-10
        /// </summary>
    public interface infLISDataAcquisition_TXT
    {
        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        clsLIS_Equip_DB_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// ��������
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ��¼��־��true = ��
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="p_frmParent">��������</param>
        /// <param name="p_objDeviceConfigVO">�豸������Ϣ</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// ���ӿڸ�����Ҫ��д�˷���
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByAuto();
        /// <summary>
        /// ���ӿڸ�����Ҫ��д�˷���
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByHandle();
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// ��ʾ�������
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// ��ʾ�ӿ���Ϣ
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;

    }
    public interface infLISDataAcquisition_DB
    {
        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        clsLIS_Equip_DB_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// ��������
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ��¼��־��true = ��
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="p_frmParent">��������</param>
        /// <param name="p_objDeviceConfigVO">�豸������Ϣ</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// ���ӿڸ�����Ҫ��д�˷���
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByAuto();
        /// <summary>
        /// ���ӿڸ�����Ҫ��д�˷���
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByHandle();
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// ��ʾ�������
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// ��ʾ�ӿ���Ϣ
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
    }


    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataShowEventHandler(clsDeviceSampleDataKey p_SampleDateKey, clsLIS_Device_Test_ResultVO[] p_objDeviceResultArr);
    /// <summary>
    /// ����ί�У���ʾ�ӿ���Ϣ��
    /// </summary>
    /// <param name="p_strInfo"></param>
    public delegate void DataAcquisitionInfoEventHandler(string p_strInfo);

    /// <summary>
    /// ��дtoString���������������������������ַ���
    /// </summary>
    public class clsDeviceSampleDataKey
    {
        public string strDeviceID;
        public string strDeviceName;
        public string strDeviceSampleID;
        public string strCheckDate;
        public int intResultBeginIndex;
        public int intResultEndIndex;
        public string strCommingDateTime;
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(strDeviceID);
            sb.Append("||");
            sb.Append(strCheckDate);
            sb.Append("||");
            sb.Append(strDeviceSampleID);
            sb.Append("||");
            sb.Append(intResultBeginIndex.ToString());
            sb.Append("||");
            sb.Append(intResultEndIndex.ToString());
            sb.Append("||");
            sb.Append(strCommingDateTime);
            return sb.ToString();
        }

    }
}
