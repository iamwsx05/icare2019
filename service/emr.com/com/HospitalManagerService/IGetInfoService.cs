using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.emr.HospitalManagerService
{
    /// <summary>
    /// ͨ���˽ӿڿ��Ի�ȡ���ҡ������������������Լ����˵���Ϣ
    /// </summary>
    public interface IHospitalManagerService
    {
        /// ����Ա��ID��ȡ��������
        long m_lngGetDeptInfo(string p_strEmployeeID, out DataTable p_dtbValue);
        /// ���ݿ���ID��ȡ����
        long m_lngGetAreaInfo(string p_strDeptID, out DataTable p_dtbValue);
        /// ���ݲ���ID��ȡ����
        long m_lngGetRoomInfo(string pStrAreaID, out DataTable pDtbValue);
        /// ���ݲ�����������ID��ȡ����
        long m_lngGetBedInfo(string p_strID, bool p_blnIsRoom, out DataTable p_dtbValue);
        /// ���ݲ�����������ID��ȡ�����б�
        long m_lngGetPatientInfo(string p_strID, bool p_blnIsRoom, out DataTable p_dtbValue);

    }
}
